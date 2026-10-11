![Banner](README/Banner.png)
# 东方夏夜祭 ～ Shining Shooting Star

[中文](README.md)

This project is part of [eagler-touhou](https://github.com/YomotsuHisami/eagler-touhou).

A browser port of the Windows game (.NET Framework 4.8, SlimDX, WinForms). Game logic stays in `THSSS`. Direct3D, XAudio2, and WinForms are replaced here and drawn with WebGL2 and Web Audio.

## Modules

| Path | Role | Status |
| --- | --- | --- |
| `THSSS` | dnSpy export of the game. `THSSS.sln` contains only this project (`net48`, x86). | Original logic. Browser differences are marked `#if BROWSER` (loading, saves, encoding, null audio). The layout is the original program and is not documented here. |
| `THSSS-Config` | dnSpy export of the config tool. Separate solution, `net20` WinForms, reads and writes `Setting.INI`. | Not part of the browser build. The web build writes a default `Setting.INI` when none exists. |
| `THSSS.Platform` | WinForms, GDI+, and WMI stand-ins on `net9.0`. | Decodes PNG and BMP, and rasters text through the canvas. Other APIs exist only so the game can compile and run its main loop. |
| `THSSS.SlimDX` | SlimDX stand-in. The assembly name is still `SlimDX`. | Device, sprites, textures, the fixed-function pipeline, render targets, fog, and lighting go through WebGL2. `.x` meshes (`Mesh`) and HLSL (`Effect`) are empty and draw nothing. XAudio2 plays WAV files the browser can decode. Key state is filled from the page; the joystick API is empty. |
| `THSSS.Browser` | `net9.0` / `browser-wasm` host. Not in `THSSS.sln`. | Transforms and compiles the `THSSS` sources, loads assets into the virtual file system, and calls `MainProcess` at 60fps. Keys come from the page. Saves go to IndexedDB. |

## Assets

Game content is not in git. Place it at the repo root:

- Required: `Image`, `CS`, `FX`, `Model`, `Story`, `Sound`, `BGM`
- Optional: `Replay`, `Score.dat`
- `BPic` and `ScreenShot` are created at runtime

`StageGameAssets` defaults to `true`. The build checks that the required folders are non-empty and copies them to `THSSS.Browser/wwwroot/assets` (if `manifest.txt` already exists, it only checks). Without the assets:

```powershell
dotnet build THSSS.Browser/THSSS.Browser.csproj -p:StageGameAssets=false
```

## Development

Requires Windows (the build scripts call `powershell`), the .NET SDK 9 (`global.json` stays on the 9.0 feature band), and the matching `wasm-tools` workload. If a newer SDK is also installed, install the workload for 9.0, not `wasm-tools-net9`.

```powershell
dotnet workload install wasm-tools
dotnet publish THSSS.Browser/THSSS.Browser.csproj -c Release
```

Serve `THSSS.Browser/bin/Release/net9.0/browser-wasm/AppBundle` with a static file server. `file://` does not work. Click the canvas first (unlocks audio and focuses the canvas). Arrows move, Z shoots, X bombs, Shift slows, Esc pauses.

Edit sources under `THSSS` to change game behavior. Before compile, the browser project skips `Program.cs` and `AssemblyInfo.cs`, rewrites `\\` to `/`, replaces `Encoding.Default` with GBK, and replaces `List.ForEach` with `ForEachNet48`. Graphics, audio, and input gaps belong in `THSSS.SlimDX` and in `gl.js` / `audio.js` under `THSSS.Browser/wwwroot`.

The desktop build is `THSSS.sln` (working directory is the repo root; it uses the SlimDX NuGet package). The config tool is `THSSS-Config/Config.sln`.

GitHub Actions (`.github/workflows/browser-build.yml`) compiles a checkout that has no assets, with `-p:StageGameAssets=false`.

## Roadmap

- Implement Mesh and Effect
- Wire up game configuration (Config)
- Full touchscreen support
- Smaller and faster asset loading
- Add THSSS-Prac
- Online play
