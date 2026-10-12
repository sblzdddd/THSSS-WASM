import { keyNameFromEvent } from "./directory-keyboard.mjs";
import { mountManagedData } from "./eagler-host.mjs";
import { prepareRuntime, launchRuntime } from "./main.js";
import { installRuntimeFileSystem } from "./runtime-files.mjs";

const protocol = "eagler-touhou/1";
const game = "thsss";
const saveRoot = "/savesthsss";
const query = new URLSearchParams(location.search);
const epoch = Number(query.get("runtimeEpoch"));
const parentWindow = window.parent;

installRuntimeFileSystem(window);

const owners = new Map();
const down = new Set();
const pointers = new Map();
let phase = "booting";
let starting = false;
let runtime = null;
let firstFrameSent = false;
let runtimeInfoSent = false;
let touchState = null;
let frameStats = { frames: 0, gaps: [] };
let lastFrameAt = 0;
let healthTimer = 0;

function hosted() {
  return window.parent !== window && Number.isSafeInteger(epoch) && epoch > 0;
}

function post(message) {
  if (!hosted()) return;
  parentWindow.postMessage(message, location.origin);
}

function emit(event, fields) {
  post(Object.assign({ protocol, game, epoch, event }, fields || {}));
}

function reply(message, ok, extra) {
  if (!message || message.request == null) return;
  post(Object.assign({
    protocol,
    game,
    epoch,
    request: message.request,
    ok,
  }, extra || {}));
}

function setKey(name, pressed, source) {
  if (!name || !source) return;
  let group = owners.get(name);
  if (!group) {
    group = new Set();
    owners.set(name, group);
  }
  if (pressed) group.add(source);
  else group.delete(source);
  const want = group.size > 0;
  if (want === down.has(name)) return;
  if (want) down.add(name);
  else down.delete(name);
  if (!runtime || !runtime.host) return;
  try {
    runtime.host.OnKey(name, want ? 1 : 0);
  } catch (err) {
    emit("error", { message: String(err && err.message || err) });
  }
}

function releaseSource(source) {
  for (const [name, group] of owners) {
    if (group.has(source)) setKey(name, false, source);
  }
}

function releaseAll() {
  for (const name of Array.from(owners.keys())) {
    const group = owners.get(name);
    if (!group) continue;
    for (const source of Array.from(group)) setKey(name, false, source);
  }
}

function normalizeUserPath(path) {
  const text = String(path || "").replace(/\\/g, "/").replace(/^\/+/, "");
  if (text === "Score.dat" || text === "Setting.INI" || text === "Log.txt") return text;
  const replay = text.match(/^(?:replay|Replay)\/([^/]+)$/);
  if (!replay) return null;
  if (/^thSSS_(?:\d{2}|ud[0-9a-f]{4})\.rpy$/i.test(replay[1]) || /^AutoSave\.rpy$/i.test(replay[1])) {
    return "Replay/" + replay[1];
  }
  return null;
}

function listedPath(stored) {
  const replay = String(stored).match(/^Replay\/([^/]+)$/);
  return replay ? "replay/" + replay[1] : stored;
}

function decodedSize(value) {
  if (typeof value !== "string" || !value) return 0;
  const pad = value.endsWith("==") ? 2 : value.endsWith("=") ? 1 : 0;
  return Math.max(0, Math.floor((value.length * 3) / 4) - pad);
}

function base64ToBytes(value) {
  const binary = atob(value || "");
  const out = new Uint8Array(binary.length);
  for (let i = 0; i < binary.length; i++) out[i] = binary.charCodeAt(i);
  return out;
}

function bytesOf(value) {
  if (value instanceof Uint8Array) return value;
  if (Array.isArray(value)) return Uint8Array.from(value);
  return null;
}

function toByteArray(bytes) {
  const out = new Array(bytes.length);
  for (let i = 0; i < bytes.length; i++) out[i] = bytes[i];
  return out;
}

async function handleFile(message) {
  const saves = runtime && runtime.saves;
  if (!saves) {
    reply(message, false, { error: "Storage is not ready" });
    return;
  }
  if (message.command === "list") {
    const files = [];
    for (const stored of Object.keys(saves)) {
      const canonical = normalizeUserPath(stored);
      if (!canonical) continue;
      files.push({ path: listedPath(canonical), size: decodedSize(saves[stored]) });
    }
    reply(message, true, { files });
    return;
  }
  if (message.command === "sync") {
    if (runtime.flushSaves) await runtime.flushSaves();
    reply(message, true, {});
    return;
  }
  const path = normalizeUserPath(message.path);
  if (!path) {
    reply(message, false, { error: "Path is not a user file", errno: 1 });
    return;
  }
  if (message.command === "read") {
    const value = saves[path];
    if (typeof value !== "string") {
      reply(message, false, { error: "Missing file", errno: 44 });
      return;
    }
    reply(message, true, { bytes: toByteArray(base64ToBytes(value)) });
    return;
  }
  if (message.command === "write") {
    const bytes = bytesOf(message.bytes);
    if (!bytes || !runtime.writeUserFile) {
      reply(message, false, { error: "Missing bytes" });
      return;
    }
    await runtime.writeUserFile(path, bytes);
    reply(message, true, {});
    return;
  }
  if (message.command === "remove") {
    if (runtime.removeUserFile) await runtime.removeUserFile(path);
    else delete saves[path];
    reply(message, true, {});
    return;
  }
  reply(message, false, { error: "Unsupported file command" });
}

function pulse(name, source) {
  setKey(name, true, source);
  setKey(name, false, source);
}

function applyTouch(message) {
  const next = message || {};
  const previous = touchState;
  setKey("Z", !!next.fireEnabled, "touch-fire");
  setKey("ShiftKey", !!next.focusEnabled, "touch-focus");
  if (previous && next.bombSerial !== previous.bombSerial) pulse("X", "touch-bomb");
  if (previous && next.escapeSerial !== previous.escapeSerial) pulse("Escape", "touch-esc");
  const dead = 0.35;
  const x = Number(next.joystickX) || 0;
  const y = Number(next.joystickY) || 0;
  setKey("Left", x <= -dead, "joy");
  setKey("Right", x >= dead, "joy");
  setKey("Up", y <= -dead, "joy");
  setKey("Down", y >= dead, "joy");
  touchState = {
    fireEnabled: !!next.fireEnabled,
    focusEnabled: !!next.focusEnabled,
    bombSerial: next.bombSerial,
    escapeSerial: next.escapeSerial,
  };
}

function applyDirectTouch(message) {
  if (!message) return;
  if (message.type === "up") pointers.delete(message.id);
  else if (message.type === "down" || message.type === "move") {
    pointers.set(message.id, { x: Number(message.x), y: Number(message.y) });
  }
  let point = null;
  for (const value of pointers.values()) point = value;
  if (!point) {
    setKey("Left", false, "direct");
    setKey("Right", false, "direct");
    setKey("Up", false, "direct");
    setKey("Down", false, "direct");
    return;
  }
  const dx = point.x - 320;
  const dy = point.y - 240;
  const dead = 24;
  setKey("Left", dx <= -dead, "direct");
  setKey("Right", dx >= dead, "direct");
  setKey("Up", dy <= -dead, "direct");
  setKey("Down", dy >= dead, "direct");
}

function rejectInapplicable(message) {
  reply(message, false, { error: "Command is not available for this title" });
}

async function handle(message) {
  if (!message || message.protocol !== protocol || message.game !== game) return;
  if (message.epoch !== epoch) return;
  const command = message.command;
  if (command === "configure") {
    if (phase === "launched" || starting) {
      reply(message, false, { error: "Cannot configure a running game" });
      return;
    }
    const options = message.options || {};
    const music = String(message.music || "none");
    if (music !== "none" && music !== "ogg") {
      reply(message, false, { error: "Music mode is not available" });
      return;
    }
    if (window.Module) window.Module.touhouMusicMode = music === "ogg" ? "ogg" : "none";
    if (runtime) runtime.options = options;
    reply(message, true, {});
    return;
  }
  if (command === "resources") {
    reply(message, true, {});
    return;
  }
  if (command === "launch") {
    if (phase === "launched" || starting) {
      reply(message, true, {});
      return;
    }
    try {
      await startGame();
      reply(message, true, {});
    } catch (err) {
      if (phase !== "launched") starting = false;
      reply(message, false, { error: String(err && err.message || err) });
      emit("error", { message: String(err && err.message || err) });
    }
    return;
  }
  if (command === "list" || command === "read" || command === "write" || command === "remove" || command === "sync") {
    await handleFile(message);
    return;
  }
  if (command === "keyboard") {
    const name = keyNameFromEvent(message);
    if (name) setKey(name, !!message.down, "key");
    reply(message, true, {});
    return;
  }
  if (command === "keyboard-clear") {
    releaseSource("key");
    reply(message, true, {});
    return;
  }
  if (command === "touch-controls") {
    applyTouch(message);
    reply(message, true, {});
    return;
  }
  if (command === "touch-cancel") {
    applyTouch({
      fireEnabled: false,
      focusEnabled: false,
      joystickX: 0,
      joystickY: 0,
      bombSerial: touchState ? touchState.bombSerial : 0,
      escapeSerial: touchState ? touchState.escapeSerial : 0,
    });
    pointers.clear();
    applyDirectTouch({ type: "up" });
    reply(message, true, {});
    return;
  }
  if (command === "direct-touch") {
    applyDirectTouch(message);
    reply(message, true, {});
    return;
  }
  if (command === "thprac-mouse" || command === "network-cancel" || command === "retry-music") {
    rejectInapplicable(message);
  }
}

function onMessage(event) {
  if (!hosted() || event.source !== parentWindow || event.origin !== location.origin) return;
  handle(event.data || {}).catch((err) => emit("error", { message: String(err && err.message || err) }));
}

function publishHealth() {
  if (phase !== "launched") return;
  const gaps = frameStats.gaps.splice(0);
  const maxGap = gaps.length ? Math.max.apply(null, gaps) : 0;
  emit("frame-health", { fps: frameStats.frames, maxGapMs: maxGap });
  frameStats.frames = 0;
  const audio = window.__thsssAudio || {};
  emit("audio-health", {
    backend: audio.backend || "script",
    underruns: audio.underruns || 0,
    minQueuedMs: audio.minQueuedMs == null ? null : audio.minQueuedMs,
  });
}

async function startGame() {
  if (phase === "launched" || starting) return;
  starting = true;
  await launchRuntime(runtime, {
    onPresented() {
      const now = performance.now();
      if (lastFrameAt) frameStats.gaps.push(now - lastFrameAt);
      lastFrameAt = now;
      frameStats.frames += 1;
      if (!firstFrameSent) {
        firstFrameSent = true;
        emit("first-frame", {});
      }
      if (!runtimeInfoSent) {
        runtimeInfoSent = true;
        emit("runtime-info", { renderer: "webgl2", architecture: "dotnet-wasm" });
      }
    },
  });
  phase = "launched";
  healthTimer = window.setInterval(publishHealth, 1000);
}

async function loadManagedArchive() {
  const deadline = performance.now() + 5000;
  while (performance.now() < deadline) {
    if (typeof parentWindow.__eaglerPrepareManagedRuntimeDataV1 === "function") {
      const mounted = await mountManagedData(parentWindow, query, game);
      if (!mounted || !mounted.buffer || mounted.buffer.byteLength <= 0) {
        throw new Error("Managed game data is empty");
      }
      globalThis.__thsssArchive = new Uint8Array(mounted.buffer);
      return;
    }
    await new Promise((resolve) => setTimeout(resolve, 30));
  }
  throw new Error("Managed game data is unavailable");
}

async function boot() {
  if (!hosted()) {
    const prepared = await prepareRuntime({ captureKeys: true });
    await launchRuntime(prepared);
    return;
  }
  window.addEventListener("message", onMessage);
  window.__eaglerDirectInputBridge = {
    schema: "eagler-touhou/direct-input/1",
    protocol,
    game,
    epoch,
    origin: location.origin,
    submit(message) {
      if (!message || message.epoch !== epoch || message.protocol !== protocol || message.game !== game) return false;
      handle(message).catch((err) => emit("error", { message: String(err && err.message || err) }));
      return true;
    },
  };
  try {
    runtime = await prepareRuntime({ captureKeys: false });
    await loadManagedArchive();
    phase = "ready";
    parent.postMessage({ protocol, game, epoch, event: "ready", saveRoot }, location.origin);
  } catch (err) {
    emit("error", { message: String(err && err.message || err) });
  }
}

document.addEventListener("visibilitychange", () => {
  if (document.hidden) releaseAll();
});
window.addEventListener("pagehide", () => {
  releaseAll();
  if (runtime && runtime.flushSaves) runtime.flushSaves();
});

boot();
