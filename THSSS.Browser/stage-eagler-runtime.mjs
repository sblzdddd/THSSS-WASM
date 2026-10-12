// Rewrite a NativeAOT publish directory into the directory runtime eagler-touhou accepts.
// The launcher allows the game html, manifest.json, root mjs/css/wasm/json,
// and fonts/*.{bin,woff,woff2}. The runtime and the game are already one wasm module.
//
//   dotnet publish THSSS.Browser/THSSS.Browser.csproj -c Release
//   node THSSS.Browser/stage-eagler-runtime.mjs

import { createHash } from "node:crypto";
import { mkdir, readFile, rm, writeFile } from "node:fs/promises";
import { dirname, resolve } from "node:path";
import { fileURLToPath } from "node:url";

const browser = dirname(fileURLToPath(import.meta.url));
const repo = resolve(browser, "..");
const publish = resolve(browser, "bin/Release/net10.0/browser-wasm/publish");
const output = resolve(repo, "build-eagler");

const allowed = name =>
  name === "thsss.html" || name === "manifest.json" ||
  /^[a-z0-9._-]+\.mjs$/i.test(name) ||
  /^[a-z0-9._-]+\.css$/i.test(name) ||
  /^[a-z0-9._-]+\.wasm$/i.test(name) ||
  /^[a-z0-9._-]+\.json$/i.test(name) ||
  /^fonts\/[a-z0-9._-]+\.(?:bin|woff2?)$/i.test(name);

const sha256 = bytes => createHash("sha256").update(bytes).digest("hex");

const files = {};
async function put(name, bytes) {
  if (!allowed(name)) throw new Error(`staged name is not a directory runtime file: ${name}`);
  if (Object.hasOwn(files, name)) throw new Error(`duplicate staged file: ${name}`);
  await mkdir(dirname(resolve(output, name)), { recursive: true });
  await writeFile(resolve(output, name), bytes);
  files[name] = { bytes: bytes.length, sha256: sha256(bytes) };
}

async function text(path) {
  return readFile(path, "utf8");
}

function stripMap(source) {
  return source.replaceAll(/^[ \t]*\/\/# sourceMappingURL=.*\r?\n?/gm, "");
}

await rm(output, { recursive: true, force: true });
await mkdir(output, { recursive: true });

const wasm = await readFile(resolve(publish, "THSSS.Browser.wasm"));
if (wasm.length < 4 || wasm[0] !== 0 || wasm[1] !== 0x61 || wasm[2] !== 0x73 || wasm[3] !== 0x6d) {
  throw new Error("published THSSS.Browser.wasm is not a wasm module");
}
await put("THSSS.Browser.wasm", wasm);

const moduleJs = stripMap(await text(resolve(publish, "THSSS.Browser.js")));
if (!moduleJs.includes("export default createThsssModule")) {
  throw new Error("published THSSS.Browser.js is not the ES module loader");
}
await put("THSSS.Browser.mjs", Buffer.from(moduleJs, "utf8"));

const shaderNames = ["Filter.vert", "Filter.frag", "Blur.vert", "Blur.frag", "Warp.vert", "Warp.frag"];
const shaderSource = {};
for (const name of shaderNames) {
  shaderSource[name] = await text(resolve(browser, "wwwroot/shaders", name));
}
await put("shaders.mjs", Buffer.from(`export const shaderSource = ${JSON.stringify(shaderSource)};\n`, "utf8"));

let main = stripMap((await text(resolve(browser, "wwwroot/main.js"))).replaceAll("\r\n", "\n"));
const mainRewrites = [
  ['import createThsssModule from "./THSSS.Browser.js";', 'import createThsssModule from "./THSSS.Browser.mjs";'],
  ['import { createGl } from "./gl.js";', 'import { createGl } from "./gl.mjs";'],
  ['import { createAudio } from "./audio.js";', 'import { createAudio } from "./audio.mjs";'],
  ['import { putRuntimeFile } from "./runtime-files.mjs";', 'import { shaderSource } from "./shaders.mjs";\nimport { putRuntimeFile } from "./runtime-files.mjs";'],
];
for (const [from, to] of mainRewrites) {
  if (!main.includes(from)) throw new Error(`main.js is missing ${from}`);
  main = main.replace(from, to);
}
const shaderLoader = `async function loadShader(path) {
  const response = await fetch(new URL(path, document.baseURI));
  if (!response.ok) {
    throw new Error("Missing shader " + path);
  }
  return response.text();
}`;
const embeddedLoader = `async function loadShader(path) {
  const name = path.slice(path.lastIndexOf("/") + 1);
  const text = shaderSource[name];
  if (typeof text !== "string") throw new Error("Missing shader " + path);
  return text;
}`;
if (!main.includes(shaderLoader)) throw new Error("main.js shader loader changed; update the stager");
main = main.replace(shaderLoader, embeddedLoader);
await put("main.mjs", Buffer.from(main, "utf8"));

for (const [from, to] of [["gl.js", "gl.mjs"], ["audio.js", "audio.mjs"]]) {
  await put(to, Buffer.from(stripMap(await text(resolve(browser, "wwwroot", from))), "utf8"));
}

let shell = await text(resolve(browser, "wwwroot/shell.mjs"));
if (!shell.includes('from "./main.js"')) throw new Error("shell.mjs no longer imports ./main.js");
shell = shell.replace('from "./main.js"', 'from "./main.mjs"');
await put("shell.mjs", Buffer.from(shell, "utf8"));

for (const name of ["thsss.html", "directory-keyboard.mjs", "eagler-host.mjs", "runtime-files.mjs", "manifest.json"]) {
  await put(name, Buffer.from(await text(resolve(browser, "wwwroot", name)), "utf8"));
}

const manifest = JSON.parse(await text(resolve(output, "manifest.json")));
if (manifest.game !== "thsss" || manifest.protocol !== "eagler-touhou/1" || manifest.entry !== "thsss.html") {
  throw new Error("wwwroot/manifest.json is not the thsss directory manifest");
}
for (const feature of ["thprac", "languages", "focusHitbox"]) {
  if (manifest.features?.[feature] !== false) throw new Error(`manifest.features.${feature} must be false`);
}

const ordered = Object.fromEntries(Object.keys(files).sort().map(name => [name, files[name]]));
await writeFile(resolve(output, "runtime-files.json"), JSON.stringify({
  schema: "eagler-touhou/runtime-directory/1",
  files: ordered,
}, null, 2) + "\n");

const counts = {};
for (const name of Object.keys(ordered)) {
  const kind = name.startsWith("fonts/") ? "fonts" : name.slice(name.lastIndexOf(".") + 1);
  counts[kind] = (counts[kind] || 0) + 1;
}
console.log(`staged ${Object.keys(ordered).length} files in ${output}`);
console.log(counts);
