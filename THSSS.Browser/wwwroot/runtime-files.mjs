const files = new Map();

function keyOf(path) {
  const text = String(path || "").replace(/\\/g, "/");
  return text.startsWith("/") ? text : "/" + text;
}

export function putRuntimeFile(path, data) {
  const bytes = data instanceof Uint8Array ? data : new Uint8Array(data || []);
  const copy = new Uint8Array(bytes.byteLength);
  copy.set(bytes);
  files.set(keyOf(path), copy);
  if (typeof globalThis !== "undefined") globalThis.__thsssFiles = files;
  return copy;
}

export function readRuntimeFile(path) {
  return files.get(keyOf(path)) || null;
}

export function installRuntimeFileSystem(target) {
  const fs = {
    mkdirTree() {},
    writeFile(path, data) {
      putRuntimeFile(path, data);
    },
  };
  target.FS = fs;
  target.Module = target.Module || {};
  target.Module.FS = fs;
  if (!target.Module.touhouMusicMode) target.Module.touhouMusicMode = "none";
  target.__thsssFiles = files;
}
