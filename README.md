![Banner](README/Banner.png)
# 东方夏夜祭 ～ Shining Shooting Star

[English](README.en.md)

本项目为 [eagler-touhou](https://github.com/YomotsuHisami/eagler-touhou) 项目的一部分。

原 Windows 版（.NET Framework 4.8、SlimDX、WinForms）的浏览器移植。游戏逻辑仍在 `THSSS`。Direct3D、XAudio2 和 WinForms 由本仓库的替身接到 WebGL2 和 Web Audio。

## 模块

| 目录 | 作用 | 现状 |
| --- | --- | --- |
| `THSSS` | dnSpy 导出的游戏本体。`THSSS.sln` 只含这个工程，目标 `net48` / x86。 | 原逻辑。浏览器差异用 `#if BROWSER` 标出（加载、存档、编码、音频空引用）。内部结构即原程序，这里不展开。 |
| `THSSS-Config` | dnSpy 导出的配置器，独立解决方案，目标 `net20` WinForms，读写 `Setting.INI`。 | 不参与浏览器构建。网页版若没有 `Setting.INI`，会自行写入一份默认配置。 |
| `THSSS.Platform` | `net9.0` 上的 WinForms、GDI+、WMI 替身。 | 能解码 PNG / BMP，并用画布光栅化文字。其余 API 只保留游戏能编译、能跑主循环所需的部分。 |
| `THSSS.SlimDX` | SlimDX API 替身，程序集名仍是 `SlimDX`。 | 设备、Sprite、纹理、固定功能管线、渲染目标、雾和光照已接到 WebGL2。文本 `.x` 网格会读入，套用帧变换后按材质绘制。`FX` 里的 Blur、Warp、Filter 用 `wwwroot/shaders` 下手工改写的 `.vert` / `.frag` 画整屏贴图。XAudio2 接到 Web Audio，播放浏览器能解码的 WAV。键盘状态由页面按键写入；手柄是空实现。 |
| `THSSS.Browser` | `net9.0` / `browser-wasm` 宿主。不参与 `THSSS.sln`。 | 构建时变换并编译 `THSSS` 的源码，把资源拉进虚拟文件系统，以 60fps 调用 `MainProcess`。按键来自页面，存档写入 IndexedDB。 |

## 资源

游戏资源不入库。放在仓库根目录：

- 必需：`Image`、`CS`、`FX`、`Model`、`Story`、`Sound`、`BGM`
- 可选：`Replay`、`Score.dat`
- `BPic`、`ScreenShot` 由运行时创建

`StageGameAssets` 默认为 `true`。构建时校验上述必需目录非空，并复制到 `THSSS.Browser/wwwroot/assets`（已有 `manifest.txt` 时只校验、不重拷）。没有资源时：

```powershell
dotnet build THSSS.Browser/THSSS.Browser.csproj -p:StageGameAssets=false
```

## 开始开发

需要 Windows（构建脚本调用 `powershell`）、.NET SDK 9（`global.json` 把版本限制在 9.0 功能带）、以及对应的 `wasm-tools`。机器上若同时装有更新的 SDK，工作量要装在 9.0 上，而不是 `wasm-tools-net9`。

```powershell
dotnet workload install wasm-tools
dotnet publish THSSS.Browser/THSSS.Browser.csproj -c Release
```

用静态文件服务器打开发布目录 `THSSS.Browser/bin/Release/net9.0/browser-wasm/AppBundle`。不能用 `file://`。先点击画布（解锁音频并取得焦点），然后方向键移动，Z 射击，X 炸弹，Shift 低速，Esc 暂停。

改游戏行为时编辑 `THSSS` 下的源文件。浏览器工程会在编译前做四件事：跳过 `Program.cs` 和 `AssemblyInfo.cs`；把源码里的 `\\` 换成 `/`；把 `Encoding.Default` 换成 GBK；把 `List.ForEach` 换成 `ForEachNet48`。图形、音频、输入的缺口改 `THSSS.SlimDX` 和 `THSSS.Browser/wwwroot` 下的 `gl.js`、`audio.js`。

原版桌面构建用 `THSSS.sln`（工作目录是仓库根，依赖 NuGet 上的 SlimDX）。配置器用 `THSSS-Config/Config.sln`。

GitHub Actions（`.github/workflows/browser-build.yml`）在不含资源的检出上执行 `-p:StageGameAssets=false` 编译。

## 路线图

- 完成游戏配置接入（Config）
- 完整适配触摸屏
- 优化资源加载和大小
- 增加 THSSS-Prac
- 支持联机
