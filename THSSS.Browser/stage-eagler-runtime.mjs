// Rewrite a Release AppBundle into the directory runtime eagler-touhou already accepts.
// The launcher allows only the game html, manifest.json, root mjs/css/wasm/json,
// and fonts/*.{bin,woff,woff2}. Assemblies stay separate: the .NET loader fetches
// each name in blazor.boot.json, so concatenating wasm modules will not boot.
//
//   dotnet publish THSSS.Browser/THSSS.Browser.csproj -c Release
//   node THSSS.Browser/stage-eagler-runtime.mjs

import { createHash } from "node:crypto";
import { mkdir, readFile, rm, writeFile } from "node:fs/promises";
import { dirname, resolve } from "node:path";
import { fileURLToPath } from "node:url";

const browser = dirname(fileURLToPath(import.meta.url));
const repo = resolve(browser, "..");
const bundle = resolve(browser, "bin/Release/net9.0/browser-wasm/AppBundle");
const framework = resolve(bundle, "_framework");
const output = resolve(repo, "build-eagler");

const allowed = name =>
  name === "thsss.html" || name === "manifest.json" ||
  /^[a-z0-9._-]+\.mjs$/i.test(name) ||
  /^[a-z0-9._-]+\.css$/i.test(name) ||
  /^[a-z0-9._-]+\.wasm$/i.test(name) ||
  /^[a-z0-9._-]+\.json$/i.test(name) ||
  /^fonts\/[a-z0-9._-]+\.(?:bin|woff2?)$/i.test(name);

const sha256 = bytes => createHash("sha256").update(bytes).digest("hex");
const integrity = bytes => "sha256-" + createHash("sha256").update(bytes).digest("base64");

function adaptLoader(source) {
  return source
    .replaceAll("dotnet.runtime.js", "dotnet.runtime.mjs")
    .replaceAll("dotnet.native.js", "dotnet.native.mjs")
    .replaceAll('import(/*! webpackIgnore: true */"process")', 'import(/*! webpackIgnore: true */globalThis["pro"+"cess"])')
    .replaceAll('import(/*! webpackIgnore: true */"module")', 'import(/*! webpackIgnore: true */globalThis["mod"+"ule"])')
    .replaceAll('import("module")', 'import(globalThis["mod"+"ule"])')
    .replaceAll(/^[ \t]*\/\/# sourceMappingURL=.*\r?\n?/gm, "");
}

function retarget(group, from, to, bytes) {
  if (!Object.hasOwn(group, from)) throw new Error(`blazor.boot.json is missing ${from}`);
  delete group[from];
  group[to] = integrity(bytes);
}

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

const boot = JSON.parse(await text(resolve(framework, "blazor.boot.json")));
const resources = boot.resources;
const expectedKeys = ["assembly", "coreAssembly", "coreVfs", "hash", "icu", "jsModuleNative", "jsModuleRuntime", "wasmNative", "wasmSymbols"];
const actualKeys = Object.keys(resources).sort();
if (JSON.stringify(actualKeys) !== JSON.stringify(expectedKeys)) {
  throw new Error(`unexpected blazor.boot.json resource groups: ${actualKeys.join(", ")}`);
}
if (typeof resources.hash !== "string") throw new Error("blazor.boot.json resources.hash is not a string");

boot.globalizationMode = "invariant";
delete resources.icu;
delete resources.wasmSymbols;

const [nativeWasmName, nativeWasmHash] = Object.entries(resources.wasmNative)[0];
const nativeWasm = await readFile(resolve(framework, nativeWasmName));
if (integrity(nativeWasm) !== nativeWasmHash) {
  throw new Error(`published ${nativeWasmName} does not match its boot hash`);
}

const runtimeConfig = resources.coreVfs?.["runtimeconfig.bin"];
const [runtimeConfigName, runtimeConfigHash] = Object.entries(runtimeConfig || {})[0] || [];
if (runtimeConfigName !== "supportFiles/0_runtimeconfig.bin") {
  throw new Error(`unexpected runtimeconfig path: ${runtimeConfigName}`);
}
const runtimeConfigBytes = await readFile(resolve(framework, runtimeConfigName));
if (integrity(runtimeConfigBytes) !== runtimeConfigHash) {
  throw new Error("published runtimeconfig.bin does not match its boot hash");
}
resources.coreVfs["runtimeconfig.bin"] = { "fonts/runtimeconfig.bin": runtimeConfigHash };

await rm(output, { recursive: true, force: true });
await mkdir(output, { recursive: true });

await put(nativeWasmName, nativeWasm);
for (const group of [resources.coreAssembly, resources.assembly]) {
  for (const name of Object.keys(group)) {
    if (name.includes("/") || name.includes("\\")) throw new Error(`assembly path is not a root file: ${name}`);
    await put(name, await readFile(resolve(framework, name)));
  }
}
await put("fonts/runtimeconfig.bin", runtimeConfigBytes);

const loaders = [
  ["dotnet.js", "dotnet.mjs", null],
  ["dotnet.native.js", "dotnet.native.mjs", resources.jsModuleNative],
  ["dotnet.runtime.js", "dotnet.runtime.mjs", resources.jsModuleRuntime],
];
for (const [from, to, group] of loaders) {
  const bytes = Buffer.from(adaptLoader(await text(resolve(framework, from))), "utf8");
  await put(to, bytes);
  if (group) retarget(group, from, to, bytes);
}

const shaderNames = ["Filter.vert", "Filter.frag", "Blur.vert", "Blur.frag", "Warp.vert", "Warp.frag"];
const shaderSource = {};
for (const name of shaderNames) {
  shaderSource[name] = await text(resolve(browser, "wwwroot/shaders", name));
}
await put("shaders.mjs", Buffer.from(`export const shaderSource = ${JSON.stringify(shaderSource)};\n`, "utf8"));

let main = (await text(resolve(browser, "wwwroot/main.js"))).replaceAll("\r\n", "\n");
const mainRewrites = [
  ['import { dotnet } from "./_framework/dotnet.js";', 'import { dotnet } from "./dotnet.mjs";'],
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
  await put(to, Buffer.from(await text(resolve(browser, "wwwroot", from)), "utf8"));
}

let shell = await text(resolve(browser, "wwwroot/shell.mjs"));
if (!shell.includes('from "./main.js"')) throw new Error("shell.mjs no longer imports ./main.js");
shell = shell.replace('from "./main.js"', 'from "./main.mjs"');
await put("shell.mjs", Buffer.from(shell, "utf8"));

for (const name of ["thsss.html", "directory-keyboard.mjs", "eagler-host.mjs", "runtime-files.mjs", "manifest.json"]) {
  await put(name, Buffer.from(await text(resolve(browser, "wwwroot", name)), "utf8"));
}

const manifest = JSON.parse(files["manifest.json"] ? await text(resolve(output, "manifest.json")) : "{}");
if (manifest.game !== "thsss" || manifest.protocol !== "eagler-touhou/1" || manifest.entry !== "thsss.html") {
  throw new Error("wwwroot/manifest.json is not the thsss directory manifest");
}
for (const feature of ["thprac", "languages", "focusHitbox"]) {
  if (manifest.features?.[feature] !== false) throw new Error(`manifest.features.${feature} must be false`);
}

await put("blazor.boot.json", Buffer.from(JSON.stringify(boot), "utf8"));

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
