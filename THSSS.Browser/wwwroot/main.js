import { dotnet } from "./_framework/dotnet.js";
import { createGl } from "./gl.js";
import { createAudio } from "./audio.js";
import { putRuntimeFile } from "./runtime-files.mjs";

const canvas = document.getElementById("game");
let lastStatus = "";

let exitRequested = false;
let exitHook = null;
let activeRuntime = null;

function notifyExit() {
  if (exitRequested) return;
  exitRequested = true;
  const flush = activeRuntime && activeRuntime.flushSaves ? activeRuntime.flushSaves() : Promise.resolve();
  Promise.resolve(flush).then(() => (exitHook ? exitHook() : setStatus("游戏已退出"))).catch((err) => {
    console.error(err);
    setStatus(String(err && err.message || err));
  });
}

function setStatus(text) {
  lastStatus = text || "";
  if (text) console.log(text);
}

let wasmLoad = null;
let wasmLoaded = 0;
let wasmTotal = 0;
let wasmCurrent = "";
const wasmInflight = new Set();

function paintWasmLoad() {
  if (!wasmLoad) {
    wasmLoad = document.createElement("div");
    wasmLoad.id = "wasm-load";
    wasmLoad.setAttribute("role", "status");
    wasmLoad.style.cssText = "position:fixed;inset:0;z-index:4;display:flex;align-items:center;justify-content:center;box-sizing:border-box;margin:0;padding:24px;background:#000;color:#f2f2f2;font:16px/1.7 sans-serif;text-align:center;white-space:pre-wrap;pointer-events:none;";
    document.body.appendChild(wasmLoad);
  }
  const left = Math.max(0, wasmTotal - wasmLoaded);
  const current = wasmCurrent ? "正在加载 " + wasmCurrent : wasmTotal > 0 && wasmLoaded >= wasmTotal ? "正在启动" : "正在加载";
  wasmLoad.textContent = "已加载 " + wasmLoaded + " / " + wasmTotal + "\n还剩 " + left + "\n" + current;
}

function hideWasmLoad() {
  if (!wasmLoad) return;
  wasmLoad.remove();
  wasmLoad = null;
}

function beginWasm(name) {
  if (wasmInflight.has(name)) {
    wasmCurrent = name;
    paintWasmLoad();
    return;
  }
  if (wasmLoaded + wasmInflight.size >= wasmTotal) wasmTotal += 1;
  wasmInflight.add(name);
  wasmCurrent = name;
  paintWasmLoad();
}

function finishWasm(name) {
  if (!wasmInflight.delete(name)) return;
  wasmLoaded += 1;
  wasmCurrent = wasmInflight.values().next().value || "";
  paintWasmLoad();
}

async function wasmAssemblyNames() {
  const path = new URL(import.meta.url).pathname.endsWith(".mjs") ? "blazor.boot.json" : "_framework/blazor.boot.json";
  const response = await fetch(new URL(path, import.meta.url));
  if (!response.ok) return [];
  const boot = await response.json();
  const resources = boot.resources || {};
  return [
    ...Object.keys(resources.assembly || {}),
    ...Object.keys(resources.coreAssembly || {}),
    ...Object.keys(resources.wasmNative || {}),
  ].filter((name) => name.endsWith(".wasm"));
}

function wasmResourceLoader(type, name, defaultUri, integrity) {
  if ((type !== "assembly" && type !== "dotnetwasm") || !name || !String(name).endsWith(".wasm")) return undefined;
  beginWasm(String(name));
  const init = { cache: "no-cache" };
  if (integrity) init.integrity = integrity;
  return fetch(defaultUri, init).then((response) => {
    if (!response.ok) throw new Error("HTTP " + response.status + ": " + name);
    finishWasm(String(name));
    return response;
  });
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
  wasmLoaded = 0;
  wasmCurrent = "";
  wasmInflight.clear();
  wasmTotal = (await wasmAssemblyNames()).length;
  paintWasmLoad();
  const { setModuleImports, getAssemblyExports, runMain } = await dotnet.withResourceLoader(wasmResourceLoader).create();
  setModuleImports("thsss.js", {
    setStatus,
    notifyExit,
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
    putAudioFile: (path, bytes) => {
      putRuntimeFile(path, bytes instanceof Uint8Array ? bytes : new Uint8Array(bytes || []));
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
  activeRuntime = runtime;
  exitHook = hooks.onExit || null;
  exitRequested = false;
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
    if (!exitRequested) hideWasmLoad();
    if (exitRequested || !running || failed) return;
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
