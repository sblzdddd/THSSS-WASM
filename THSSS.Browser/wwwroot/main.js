import { dotnet } from "./_framework/dotnet.js";
import { createGl } from "./gl.js";
import { createAudio } from "./audio.js";

const statusEl = document.getElementById("status");
const canvas = document.getElementById("game");

function setStatus(text) {
  statusEl.textContent = text || "";
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

try {
  const glApi = createGl(canvas);
  const audioApi = createAudio();
  const unlockAudio = () => audioApi.resume();
  canvas.addEventListener("pointerdown", unlockAudio);
  window.addEventListener("keydown", unlockAudio);
  const saves = await readAllSaves();
  const { setModuleImports, getAssemblyExports, runMain } = await dotnet.create();
  setModuleImports("thsss.js", {
    setStatus,
    log: (text) => console.log(text),
    readSaves: () => JSON.stringify(saves),
    writeSave: (path, b64) => {
      saves[path] = b64;
      writeSave(path, b64).catch((err) => console.error(err));
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
  setStatus("Starting the game…");
  await runMain();
  const bootStatus = statusEl.textContent || "";
  window.__thsssBoot = bootStatus;
  if (/Exception|Error|at Thsss|at Shooting|at SlimDX/i.test(bootStatus)) {
    throw new Error(bootStatus);
  }
  const exports = await getAssemblyExports("THSSS.Browser.dll");
  const host = exports.Thsss.Browser.GameHost;
  if (!host || !host.Frame) {
    throw new Error("GameHost export missing. Keys: " + Object.keys(exports || {}).join(","));
  }
  window.__thsssHost = true;
  bootKeys((name, down) => host.OnKey(name, down));
  const fpsToggle = document.getElementById("unlock-fps");
  if (host.FpsUnlocked && fpsToggle) fpsToggle.checked = host.FpsUnlocked() !== 0;
  if (fpsToggle && host.SetFpsUnlocked) {
    fpsToggle.addEventListener("change", () => host.SetFpsUnlocked(fpsToggle.checked ? 1 : 0));
  }
  let last = performance.now();
  let acc = 0;
  const step = 1000 / 60;
  let failed = false;
  const frame = (now) => {
    if (failed) return;
    const delta = Math.min(100, now - last);
    last = now;
    try {
      if (fpsToggle && fpsToggle.checked && host.Simulate && host.Draw) {
        acc += delta;
        if (acc >= step) {
          host.Simulate();
          acc -= step;
          if (acc > step) acc = step;
        }
        host.Draw(acc / step);
      } else {
        acc += delta;
        if (acc >= step) {
          host.Frame();
          acc -= step;
          if (acc > step * 2) acc = 0;
        }
      }
    } catch (err) {
      failed = true;
      window.__thsssFrameError = String(err && err.stack || err);
      setStatus(window.__thsssFrameError);
      return;
    }
    requestAnimationFrame(frame);
  };
  setStatus("Click the canvas, then use arrows, Z, X, Shift, and Esc.");
  requestAnimationFrame(frame);
} catch (err) {
  window.__thsssBootError = String(err && err.stack || err);
  setStatus(window.__thsssBootError);
  console.error(err);
}
