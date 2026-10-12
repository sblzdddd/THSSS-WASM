import { dotnet } from "./_framework/dotnet.js";
import { createGl } from "./gl.js";
import { createAudio } from "./audio.js";

const statusEl = document.getElementById("status");
const canvas = document.getElementById("game");
let lastStatus = "";

function setStatus(text) {
  lastStatus = text || "";
  if (statusEl) statusEl.textContent = lastStatus;
  if (text) console.log(text);
}

const dbName = "thsss-saves";
const storeName = "files";

function openDb() {
  return new Promise((resolve, reject) => {
    const request = indexedDB.open(dbName, 1);
    request.onupgradeneeded = () => {
      if (!request.result.objectStoreNames.contains(storeName)) {
        request.result.createObjectStore(storeName);
      }
    };
    request.onsuccess = () => resolve(request.result);
    request.onerror = () => reject(request.error);
  });
}

async function readAllSaves() {
  const db = await openDb();
  return new Promise((resolve, reject) => {
    const tx = db.transaction(storeName, "readonly");
    const store = tx.objectStore(storeName);
    const request = store.getAllKeys();
    const values = store.getAll();
    const bag = {};
    request.onsuccess = () => {
      const keys = request.result || [];
      values.onsuccess = () => {
        const items = values.result || [];
        for (let i = 0; i < keys.length; i++) bag[keys[i]] = items[i];
        resolve(bag);
      };
    };
    tx.onerror = () => reject(tx.error);
  });
}

async function writeSave(path, b64) {
  const db = await openDb();
  return new Promise((resolve, reject) => {
    const tx = db.transaction(storeName, "readwrite");
    tx.objectStore(storeName).put(b64, path);
    tx.oncomplete = () => resolve();
    tx.onerror = () => reject(tx.error);
  });
}

async function removeSave(path) {
  const db = await openDb();
  return new Promise((resolve, reject) => {
    const tx = db.transaction(storeName, "readwrite");
    tx.objectStore(storeName).delete(path);
    tx.oncomplete = () => resolve();
    tx.onerror = () => reject(tx.error);
  });
}

function bytesToBase64(bytes) {
  let binary = "";
  const chunk = 0x8000;
  for (let i = 0; i < bytes.length; i += chunk) {
    binary += String.fromCharCode.apply(null, bytes.subarray(i, i + chunk));
  }
  return btoa(binary);
}

const keyMap = {
  ArrowLeft: "Left",
  ArrowRight: "Right",
  ArrowUp: "Up",
  ArrowDown: "Down",
  ShiftLeft: "ShiftKey",
  ShiftRight: "ShiftKey",
  ControlLeft: "ControlKey",
  ControlRight: "ControlKey",
  AltLeft: "Menu",
  Enter: "Return",
  Escape: "Escape",
  Space: "Space",
  KeyZ: "Z",
  KeyX: "X",
  KeyC: "C",
  KeyP: "P",
  KeyR: "R",
  Minus: "OemMinus",
  Equal: "Oemplus",
  F8: "F8"
};

const held = new Set();

function bootKeys(onKey) {
  const send = (ev, down) => {
    const name = keyMap[ev.code];
    if (!name) return;
    ev.preventDefault();
    if (down) {
      if (held.has(name)) return;
      held.add(name);
    } else {
      held.delete(name);
    }
    onKey(name, down ? 1 : 0);
  };
  window.addEventListener("keydown", (ev) => send(ev, true));
  window.addEventListener("keyup", (ev) => send(ev, false));
  canvas.addEventListener("pointerdown", () => canvas.focus());
  canvas.focus();
}

async function loadShader(path) {
  const response = await fetch(new URL(path, document.baseURI));
  if (!response.ok) {
    throw new Error("Missing shader " + path);
  }
  return response.text();
}

export async function prepareRuntime(options = {}) {
  const shaderSources = {};
  for (const name of ["Filter", "Blur", "Warp"]) {
    shaderSources[name + ".vert"] = await loadShader("shaders/" + name + ".vert");
    shaderSources[name + ".frag"] = await loadShader("shaders/" + name + ".frag");
  }
  const glApi = createGl(canvas, shaderSources);
  const audioApi = createAudio();
  const unlockAudio = () => audioApi.resume();
  canvas.addEventListener("pointerdown", unlockAudio);
  window.addEventListener("keydown", unlockAudio);
  const saves = await readAllSaves();
  let pending = Promise.resolve();
  const remember = (path, value) => {
    pending = pending.then(() => (value == null ? removeSave(path) : writeSave(path, value))).catch((err) => console.error(err));
  };
  const { setModuleImports, getAssemblyExports, runMain } = await dotnet.create();
  setModuleImports("thsss.js", {
    setStatus,
    log: (text) => console.log(text),
    readSaves: () => JSON.stringify(saves),
    writeSave: (path, b64) => {
      saves[path] = b64;
      remember(path, b64);
    },
    takeArchive: () => {
      const bytes = globalThis.__thsssArchive;
      globalThis.__thsssArchive = null;
      return bytes && bytes.byteLength ? bytes : new Uint8Array(0);
    },
    pageBase: () => document.baseURI,
    clear: glApi.clear,
    createTexture: glApi.createTexture,
    createRenderTarget: glApi.createRenderTarget,
    updateTexture: glApi.updateTexture,
    deleteTexture: glApi.deleteTexture,
    bindTarget: glApi.bindTarget,
    setViewport: glApi.setViewport,
    setBlend: glApi.setBlend,
    setDepth: glApi.setDepth,
    setCull: glApi.setCull,
    drawSprites: glApi.drawSprites,
    drawEffect: glApi.drawEffect,
    drawFixed: glApi.drawFixed,
    beginImage: glApi.beginImage,
    imageState: glApi.imageState,
    finishImage: glApi.finishImage,
    imageWidth: glApi.imageWidth,
    imageHeight: glApi.imageHeight,
    rasterText: glApi.rasterText,
    rasterTextBox: glApi.rasterTextBox,
    paintText: glApi.paintText,
    audioPrepare: audioApi.prepare,
    audioVoice: audioApi.createVoice,
    audioDestroy: audioApi.destroyVoice,
    audioSubmit: audioApi.submit,
    audioStart: audioApi.start,
    audioStop: audioApi.stop,
    audioVolume: audioApi.setVolume,
    audioPan: audioApi.setPan,
    audioFilter: audioApi.setFilter,
    audioSamples: audioApi.samplesPlayed,
    audioChannels: audioApi.channels
  });
  return {
    saves,
    options: {},
    host: null,
    captureKeys: options.captureKeys !== false,
    audioApi,
    getAssemblyExports,
    runMain,
    writeUserFile(path, bytes) {
      const b64 = bytesToBase64(bytes);
      saves[path] = b64;
      return writeSave(path, b64);
    },
    removeUserFile(path) {
      delete saves[path];
      return removeSave(path);
    },
    flushSaves() {
      return pending;
    }
  };
}

export async function launchRuntime(runtime, hooks = {}) {
  setStatus("Starting the game…");
  await runtime.runMain();
  window.__thsssBoot = lastStatus;
  if (/Exception|Error|at Thsss|at Shooting|at SlimDX/i.test(lastStatus)) {
    throw new Error(lastStatus);
  }
  const exports = await runtime.getAssemblyExports("THSSS.Browser.dll");
  const host = exports.Thsss.Browser.GameHost;
  if (!host || !host.Frame) {
    throw new Error("GameHost export missing. Keys: " + Object.keys(exports || {}).join(","));
  }
  runtime.host = host;
  window.__thsssHost = true;
  if (runtime.captureKeys) bootKeys((name, down) => host.OnKey(name, down));
  const optionSet = !!(runtime.options && Object.prototype.hasOwnProperty.call(runtime.options, "limitPresentationTo60"));
  const unlimited = optionSet && runtime.options.limitPresentationTo60 === false && host.Simulate && host.Draw;
  if ((runtime.captureKeys === false || optionSet) && host.SetFpsUnlocked) host.SetFpsUnlocked(unlimited ? 1 : 0);
  const fpsToggle = document.getElementById("unlock-fps");
  if (!unlimited && host.FpsUnlocked && fpsToggle) fpsToggle.checked = host.FpsUnlocked() !== 0;
  if (fpsToggle && host.SetFpsUnlocked) {
    fpsToggle.addEventListener("change", () => host.SetFpsUnlocked(fpsToggle.checked ? 1 : 0));
  }
  let last = performance.now();
  let acc = 0;
  const step = 1000 / 60;
  let failed = false;
  let running = true;
  const frame = (now) => {
    if (!running || failed) return;
    if (document.hidden) {
      last = now;
      requestAnimationFrame(frame);
      return;
    }
    const delta = Math.min(100, now - last);
    last = now;
    let presented = false;
    try {
      const unlocked = unlimited || !!(fpsToggle && fpsToggle.checked);
      const fast = unlocked && host.Simulate && host.Draw;
      if (fast) {
        acc += delta;
        if (acc >= step) {
          host.Simulate();
          acc -= step;
          if (acc > step) acc = step;
        }
        host.Draw(acc / step);
        presented = true;
      } else {
        acc += delta;
        if (acc >= step) {
          host.Frame();
          acc -= step;
          if (acc > step * 2) acc = 0;
          presented = true;
        }
      }
    } catch (err) {
      failed = true;
      window.__thsssFrameError = String(err && err.stack || err);
      setStatus(window.__thsssFrameError);
      return;
    }
    if (presented && hooks.onPresented) hooks.onPresented();
    requestAnimationFrame(frame);
  };
  if (runtime.captureKeys) setStatus("Click the canvas, then use arrows, Z, X, Shift, and Esc.");
  requestAnimationFrame(frame);
  return {
    stop() {
      running = false;
    }
  };
}

if (!document.querySelector('meta[name="eagler-data-provider"]')) {
  prepareRuntime({ captureKeys: true }).then((runtime) => launchRuntime(runtime)).catch((err) => {
    window.__thsssBootError = String(err && err.stack || err);
    setStatus(window.__thsssBootError);
    console.error(err);
  });
}
