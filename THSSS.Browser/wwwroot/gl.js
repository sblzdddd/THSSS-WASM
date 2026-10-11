const textures = new Map();
const framebuffers = new Map();
let gl;
let nextId = 1;
let whiteId = 0;
let spriteProg;
let fixedProg;
let spriteBuf;
let fixedBuf;
let viewX = 0, viewY = 0, viewW = 640, viewH = 480, fbW = 640, fbH = 480;

function compile(type, source) {
  const shader = gl.createShader(type);
  gl.shaderSource(shader, source);
  gl.compileShader(shader);
  if (!gl.getShaderParameter(shader, gl.COMPILE_STATUS)) {
    throw new Error(gl.getShaderInfoLog(shader));
  }
  return shader;
}

function program(vs, fs) {
  const prog = gl.createProgram();
  gl.attachShader(prog, compile(gl.VERTEX_SHADER, vs));
  gl.attachShader(prog, compile(gl.FRAGMENT_SHADER, fs));
  gl.linkProgram(prog);
  if (!gl.getProgramParameter(prog, gl.LINK_STATUS)) {
    throw new Error(gl.getProgramInfoLog(prog));
  }
  return prog;
}

function blendFactor(value) {
  switch (value) {
    case 1: return gl.ZERO;
    case 2: return gl.ONE;
    case 3: return gl.SRC_COLOR;
    case 4: return gl.ONE_MINUS_SRC_COLOR;
    case 5: return gl.SRC_ALPHA;
    case 6: return gl.ONE_MINUS_SRC_ALPHA;
    case 7: return gl.DST_ALPHA;
    case 8: return gl.ONE_MINUS_DST_ALPHA;
    case 9: return gl.DST_COLOR;
    case 10: return gl.ONE_MINUS_DST_COLOR;
    case 11: return gl.SRC_ALPHA_SATURATE;
    default: return gl.SRC_ALPHA;
  }
}

function makeTexture(width, height, pixels, renderTarget) {
  const tex = gl.createTexture();
  gl.bindTexture(gl.TEXTURE_2D, tex);
  gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.LINEAR);
  gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, gl.LINEAR);
  gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_S, gl.CLAMP_TO_EDGE);
  gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_T, gl.CLAMP_TO_EDGE);
  gl.pixelStorei(gl.UNPACK_ALIGNMENT, 1);
  const data = pixels && pixels.length ? pixels : null;
  gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, width, height, 0, gl.RGBA, gl.UNSIGNED_BYTE, data);
  const id = nextId++;
  const rec = { tex, width, height, renderTarget };
  textures.set(id, rec);
  if (renderTarget) {
    const fb = gl.createFramebuffer();
    gl.bindFramebuffer(gl.FRAMEBUFFER, fb);
    gl.framebufferTexture2D(gl.FRAMEBUFFER, gl.COLOR_ATTACHMENT0, gl.TEXTURE_2D, tex, 0);
    const depth = gl.createRenderbuffer();
    gl.bindRenderbuffer(gl.RENDERBUFFER, depth);
    gl.renderbufferStorage(gl.RENDERBUFFER, gl.DEPTH_COMPONENT16, width, height);
    gl.framebufferRenderbuffer(gl.FRAMEBUFFER, gl.DEPTH_ATTACHMENT, gl.RENDERBUFFER, depth);
    framebuffers.set(id, fb);
    gl.bindFramebuffer(gl.FRAMEBUFFER, null);
  }
  return id;
}

export function createGl(canvas) {
  gl = canvas.getContext("webgl2", { alpha: false, depth: true, antialias: false, stencil: false, preserveDrawingBuffer: true });
  if (!gl) {
    throw new Error("WebGL2 is not available.");
  }
  const spriteVs = `#version 300 es
layout(location=0) in vec2 aPos;
layout(location=1) in vec2 aUv;
layout(location=2) in vec4 aColor;
uniform vec2 uScreen;
out vec2 vUv;
out vec4 vColor;
void main() {
  vec2 c = vec2(aPos.x / uScreen.x * 2.0 - 1.0, 1.0 - aPos.y / uScreen.y * 2.0);
  gl_Position = vec4(c, 0.0, 1.0);
  vUv = aUv;
  vColor = aColor;
}`;
  const spriteFs = `#version 300 es
precision mediump float;
uniform sampler2D uTex;
uniform float uFlipV;
in vec2 vUv;
in vec4 vColor;
out vec4 o;
void main() {
  vec2 uv = vec2(vUv.x, mix(vUv.y, 1.0 - vUv.y, uFlipV));
  o = texture(uTex, uv) * vColor;
}`;
  const fixedVs = `#version 300 es
layout(location=0) in vec3 aPos;
layout(location=1) in vec2 aUv;
layout(location=2) in vec4 aColor;
uniform mat4 uWorld;
uniform mat4 uView;
uniform mat4 uProj;
out vec2 vUv;
out vec4 vColor;
out float vFogZ;
void main() {
  vec4 world = uWorld * vec4(aPos, 1.0);
  vec4 view = uView * world;
  gl_Position = uProj * view;
  vFogZ = view.z;
  vUv = aUv;
  vColor = aColor;
}`;
  const fixedFs = `#version 300 es
precision mediump float;
uniform sampler2D uTex;
uniform vec4 uMaterial;
uniform float uLighting;
uniform float uFog;
uniform float uFogStart;
uniform float uFogEnd;
uniform vec3 uFogColor;
in vec2 vUv;
in vec4 vColor;
in float vFogZ;
out vec4 o;
void main() {
  vec4 tex = texture(uTex, vUv);
  vec4 col = tex * vColor;
  if (uLighting > 0.5) col *= uMaterial;
  if (uFog > 0.5) {
    float f = clamp((uFogEnd - vFogZ) / max(1.0, uFogEnd - uFogStart), 0.0, 1.0);
    col.rgb = mix(uFogColor, col.rgb, f);
  }
  o = col;
}`;
  spriteProg = program(spriteVs, spriteFs);
  fixedProg = program(fixedVs, fixedFs);
  spriteBuf = gl.createBuffer();
  fixedBuf = gl.createBuffer();
  const white = new Uint8Array([255, 255, 255, 255]);
  whiteId = makeTexture(1, 1, white, false);
  gl.enable(gl.BLEND);
  gl.blendFunc(gl.SRC_ALPHA, gl.ONE_MINUS_SRC_ALPHA);
  gl.clearColor(0, 0, 0, 1);
  gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT);
  return {
    clear: glClear,
    createTexture: glCreateTexture,
    createRenderTarget: glCreateRenderTarget,
    updateTexture: glUpdateTexture,
    deleteTexture: glDeleteTexture,
    bindTarget: glBindTarget,
    setViewport: glSetViewport,
    setBlend: glSetBlend,
    setDepth: glSetDepth,
    setCull: glSetCull,
    drawSprites: glDrawSprites,
    drawFixed: glDrawFixed,
    rasterText: glRasterText
  };
}

export function glClear(r, g, b, a, color, depth) {
  let bits = 0;
  if (color) {
    gl.clearColor(r, g, b, a);
    bits |= gl.COLOR_BUFFER_BIT;
  }
  if (depth) bits |= gl.DEPTH_BUFFER_BIT;
  if (bits) gl.clear(bits);
}

export function glCreateTexture(width, height, pixels) {
  return makeTexture(width | 0, height | 0, pixels, false);
}

export function glCreateRenderTarget(width, height) {
  return makeTexture(width | 0, height | 0, null, true);
}

export function glUpdateTexture(id, width, height, pixels) {
  const rec = textures.get(id | 0);
  if (!rec) return;
  gl.bindTexture(gl.TEXTURE_2D, rec.tex);
  gl.pixelStorei(gl.UNPACK_ALIGNMENT, 1);
  gl.texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, width | 0, height | 0, 0, gl.RGBA, gl.UNSIGNED_BYTE, pixels);
  rec.width = width | 0;
  rec.height = height | 0;
}

export function glDeleteTexture(id) {
  const rec = textures.get(id | 0);
  if (!rec) return;
  gl.deleteTexture(rec.tex);
  const fb = framebuffers.get(id | 0);
  if (fb) gl.deleteFramebuffer(fb);
  textures.delete(id | 0);
  framebuffers.delete(id | 0);
}

export function glBindTarget(id) {
  const fb = id ? framebuffers.get(id | 0) : null;
  gl.bindFramebuffer(gl.FRAMEBUFFER, fb || null);
}

export function glSetViewport(x, y, width, height, canvasWidth, canvasHeight) {
  viewX = x | 0;
  viewY = y | 0;
  viewW = Math.max(1, width | 0);
  viewH = Math.max(1, height | 0);
  fbW = Math.max(1, canvasWidth | 0);
  fbH = Math.max(1, canvasHeight | 0);
  gl.viewport(viewX, fbH - viewY - viewH, viewW, viewH);
}

export function glSetBlend(enable, src, dst, equation) {
  if (enable) gl.enable(gl.BLEND);
  else gl.disable(gl.BLEND);
  gl.blendFunc(blendFactor(src | 0), blendFactor(dst | 0));
  const eq = equation | 0;
  if (eq === 2) gl.blendEquation(gl.FUNC_SUBTRACT);
  else if (eq === 3) gl.blendEquation(gl.FUNC_REVERSE_SUBTRACT);
  else if (eq === 4) gl.blendEquation(gl.MIN);
  else if (eq === 5) gl.blendEquation(gl.MAX);
  else gl.blendEquation(gl.FUNC_ADD);
}

export function glSetDepth(enable) {
  if (enable) {
    gl.enable(gl.DEPTH_TEST);
    gl.depthFunc(gl.LEQUAL);
  } else {
    gl.disable(gl.DEPTH_TEST);
  }
}

export function glSetCull(mode) {
  const m = mode | 0;
  if (m === 2 || m === 3) {
    gl.enable(gl.CULL_FACE);
    gl.cullFace(m === 2 ? gl.FRONT : gl.BACK);
  } else {
    gl.disable(gl.CULL_FACE);
  }
}

function bindTex(id) {
  const rec = textures.get(id | 0) || textures.get(whiteId);
  gl.bindTexture(gl.TEXTURE_2D, rec.tex);
}

function asF32(data) {
  if (!data) return new Float32Array(0);
  if (data instanceof Float32Array) return data;
  const u8 = data instanceof Uint8Array ? data : new Uint8Array(data);
  return new Float32Array(u8.buffer, u8.byteOffset, (u8.byteLength / 4) | 0);
}

export function glDrawSprites(textureId, flipV, raw) {
  const data = asF32(raw);
  if (data.length < 8) return;
  gl.useProgram(spriteProg);
  gl.bindBuffer(gl.ARRAY_BUFFER, spriteBuf);
  gl.bufferData(gl.ARRAY_BUFFER, data, gl.DYNAMIC_DRAW);
  const stride = 8 * 4;
  gl.enableVertexAttribArray(0);
  gl.enableVertexAttribArray(1);
  gl.enableVertexAttribArray(2);
  gl.vertexAttribPointer(0, 2, gl.FLOAT, false, stride, 0);
  gl.vertexAttribPointer(1, 2, gl.FLOAT, false, stride, 8);
  gl.vertexAttribPointer(2, 4, gl.FLOAT, false, stride, 16);
  gl.uniform2f(gl.getUniformLocation(spriteProg, "uScreen"), viewW, viewH);
  gl.uniform1f(gl.getUniformLocation(spriteProg, "uFlipV"), flipV);
  gl.uniform1i(gl.getUniformLocation(spriteProg, "uTex"), 0);
  gl.activeTexture(gl.TEXTURE0);
  bindTex(textureId);
  gl.drawArrays(gl.TRIANGLES, 0, (data.length / 8) | 0);
}

export function glDrawFixed(textureId, raw, worldRaw, viewRaw, projRaw, mr, mg, mb, ma, lighting, fog, fogStart, fogEnd, fr, fg, fb) {
  const data = asF32(raw);
  const world = asF32(worldRaw);
  const view = asF32(viewRaw);
  const proj = asF32(projRaw);
  if (data.length < 9) return;
  gl.useProgram(fixedProg);
  gl.bindBuffer(gl.ARRAY_BUFFER, fixedBuf);
  gl.bufferData(gl.ARRAY_BUFFER, data, gl.DYNAMIC_DRAW);
  const stride = 9 * 4;
  gl.enableVertexAttribArray(0);
  gl.enableVertexAttribArray(1);
  gl.enableVertexAttribArray(2);
  gl.vertexAttribPointer(0, 3, gl.FLOAT, false, stride, 0);
  gl.vertexAttribPointer(1, 2, gl.FLOAT, false, stride, 12);
  gl.vertexAttribPointer(2, 4, gl.FLOAT, false, stride, 20);
  gl.uniformMatrix4fv(gl.getUniformLocation(fixedProg, "uWorld"), false, world);
  gl.uniformMatrix4fv(gl.getUniformLocation(fixedProg, "uView"), false, view);
  gl.uniformMatrix4fv(gl.getUniformLocation(fixedProg, "uProj"), false, proj);
  gl.uniform4f(gl.getUniformLocation(fixedProg, "uMaterial"), mr, mg, mb, ma);
  gl.uniform1f(gl.getUniformLocation(fixedProg, "uLighting"), lighting);
  gl.uniform1f(gl.getUniformLocation(fixedProg, "uFog"), fog);
  gl.uniform1f(gl.getUniformLocation(fixedProg, "uFogStart"), fogStart);
  gl.uniform1f(gl.getUniformLocation(fixedProg, "uFogEnd"), fogEnd);
  gl.uniform3f(gl.getUniformLocation(fixedProg, "uFogColor"), fr, fg, fb);
  gl.uniform1i(gl.getUniformLocation(fixedProg, "uTex"), 0);
  gl.activeTexture(gl.TEXTURE0);
  bindTex(textureId);
  gl.drawArrays(gl.TRIANGLES, 0, (data.length / 9) | 0);
}

export function glRasterText(text, width, height, sizePx, bold, italic) {
  const canvas = document.createElement("canvas");
  canvas.width = Math.max(1, width | 0);
  canvas.height = Math.max(1, height | 0);
  const ctx = canvas.getContext("2d");
  ctx.clearRect(0, 0, canvas.width, canvas.height);
  ctx.font = `${italic ? "italic " : ""}${bold ? "bold " : ""}${sizePx || 12}px sans-serif`;
  ctx.fillStyle = "#fff";
  ctx.textBaseline = "top";
  ctx.fillText(text || "", 0, 0);
  return new Uint8Array(ctx.getImageData(0, 0, canvas.width, canvas.height).data);
}
