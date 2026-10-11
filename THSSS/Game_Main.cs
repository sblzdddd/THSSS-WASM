using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using SlimDX;
using SlimDX.Direct3D9;
using SlimDX.DirectInput;

namespace Shooting
{
	// Token: 0x0200008F RID: 143
	public class Game_Main
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00020FDC File Offset: 0x0001F1DC
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x00020FF4 File Offset: 0x0001F1F4
		public GlobalDataPackage GlobalData
		{
			get
			{
				return this.GDP;
			}
			set
			{
				this.GDP = value;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00021010 File Offset: 0x0001F210
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x00020FFE File Offset: 0x0001F1FE
		public global::SlimDX.Direct3D9.Device DeviceMain
		{
			get
			{
				return this.GlobalData.DeviceMain;
			}
			set
			{
				this.GlobalData.DeviceMain = value;
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00021030 File Offset: 0x0001F230
		public Game_Main()
		{
			this.Form_Main = new RenderForm_Main();
			this.Form_Main.FormClosing += this.Form_Main_FormClosing;
			this.Form_Main.Deactivate += this.Form_Main_Deactivate;
			this.Form_Main.Activated += this.Form_Main_Activated;
			MouseCtrl.DrawCursor(!this.fullWindow);
			Dpi.GetDpi();
			try
			{
				INI_RW ini_RW = new INI_RW(".\\Setting.INI");
				if (ini_RW.ExistINIFile())
				{
					this.fullWindow = ini_RW.IniReadValue("DirectX", "FullWindow") == "1";
					this.antiAlias = ini_RW.IniReadValue("DirectX", "AntiAlias") == "1";
					this.verticalSync = ini_RW.IniReadValue("DirectX", "VerticalSync") == "1";
					this.useDirectInput = ini_RW.IniReadValue("DirectInput", "UseDirectInput") == "1";
					this.GlobalData.Clear = ini_RW.IniReadValue("Mode", "Clear") == "yes";
					this.singleThreaded = ini_RW.IniReadValue("Mode", "SingleThreaded") == "1";
					this.fixedFPS = ini_RW.IniReadValue("Mode", "FixedFPS") == "1";
					this.delayTolerance = (int)Convert.ToInt16(ini_RW.IniReadValue("Mode", "DelayTolerance"));
					this.windowSize = (int)Convert.ToInt16(ini_RW.IniReadValue("Mode", "WindowSize"));
					this.presentInterval = (int)Convert.ToInt16(ini_RW.IniReadValue("Mode", "PresentInterval"));
					this.mapkey.Left = ini_RW.IniReadValue("Key", "Left");
					this.mapkey.Right = ini_RW.IniReadValue("Key", "Right");
					this.mapkey.Up = ini_RW.IniReadValue("Key", "Up");
					this.mapkey.Down = ini_RW.IniReadValue("Key", "Down");
					this.mapkey.Slow = ini_RW.IniReadValue("Key", "Slow");
					this.mapkey.Shoot = ini_RW.IniReadValue("Key", "Shoot");
					this.mapkey.Bomb = ini_RW.IniReadValue("Key", "Bomb");
					MapJoyStick.Skip = (int)Convert.ToInt16(ini_RW.IniReadValue("JoyStick", "Skip"));
					MapJoyStick.Pause = (int)Convert.ToInt16(ini_RW.IniReadValue("JoyStick", "Pause"));
					MapJoyStick.Slow = (int)Convert.ToInt16(ini_RW.IniReadValue("JoyStick", "Slow"));
					MapJoyStick.Shoot = (int)Convert.ToInt16(ini_RW.IniReadValue("JoyStick", "Shoot"));
					MapJoyStick.Bomb = (int)Convert.ToInt16(ini_RW.IniReadValue("JoyStick", "Bomb"));
					MapJoyStick.C = (int)Convert.ToInt16(ini_RW.IniReadValue("JoyStick", "C"));
					this.bgmVolume = (int)Convert.ToInt16(ini_RW.IniReadValue("Volume", "BGMVolume"));
					this.seVolume = (int)Convert.ToInt16(ini_RW.IniReadValue("Volume", "SEVolume"));
					this.fontType = ini_RW.IniReadValue("Game", "FontType");
				}
			}
			catch
			{
				MessageBox.Show("读取配置文件失败", "Setting Load Error");
			}
			if (this.windowSize == 1)
			{
				this.Form_Main.ClientSize = new Size(800, 600);
			}
			else if (this.windowSize == 2)
			{
				this.Form_Main.ClientSize = new Size(1024, 768);
			}
			if (!this.Direct3DInit())
			{
				Application.ExitThread();
			}
			else
			{
				this.initSuccess = true;
				if (this.useDirectInput)
				{
					this.KCapture = new KeyboardCapture(this.Form_Main.Handle);
					this.JCapture = new JoystickCapture(this.Form_Main.Handle);
				}
				else
				{
					this.Form_Main.KeyDown += this.Form_Main_KeyDown;
					this.Form_Main.KeyUp += this.Form_Main_KeyUp;
				}
				this.PresentState = new GameState_SplashScreen(this.GlobalData);
				this.PresentState.Init();
				this.PresentState.BGM_ON();
				this.GlobalData.LoadResources1();
				this.LoadingThread = new Thread(new ThreadStart(this.AddState));
				this.LoadingThread.Start();
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x000215A4 File Offset: 0x0001F7A4
		public bool Direct3DInit()
		{
			bool flag;
			try
			{
				Direct3D direct3D = new Direct3D();
				AdapterInformation defaultAdapter = direct3D.Adapters.DefaultAdapter;
				global::SlimDX.Direct3D9.Capabilities deviceCaps = direct3D.GetDeviceCaps(defaultAdapter.Adapter, global::SlimDX.Direct3D9.DeviceType.Hardware);
				CreateFlags createFlags;
				if ((deviceCaps.DeviceCaps & DeviceCaps.HWTransformAndLight) == DeviceCaps.HWTransformAndLight)
				{
					createFlags = CreateFlags.HardwareVertexProcessing;
				}
				else
				{
					createFlags = CreateFlags.SoftwareVertexProcessing;
				}
				this.GlobalData.presentParams = new PresentParameters
				{
					BackBufferWidth = 640,
					BackBufferHeight = 480,
					BackBufferFormat = defaultAdapter.CurrentDisplayMode.Format,
					DeviceWindowHandle = this.Form_Main.Handle,
					Windowed = !this.fullWindow,
					PresentFlags = PresentFlags.DiscardDepthStencil,
					SwapEffect = SwapEffect.Discard
				};
				if (this.presentInterval == 1)
				{
					if (this.verticalSync)
					{
						this.GlobalData.presentParams.PresentationInterval = PresentInterval.Default;
					}
					else
					{
						this.GlobalData.presentParams.PresentationInterval = PresentInterval.Immediate;
					}
				}
				else if (this.fullWindow && this.verticalSync)
				{
					if (this.presentInterval == 2)
					{
						this.GlobalData.presentParams.PresentationInterval = PresentInterval.Two;
					}
					else if (this.presentInterval == 3)
					{
						this.GlobalData.presentParams.PresentationInterval = PresentInterval.Three;
					}
				}
				else
				{
					this.GlobalData.presentParams.PresentationInterval = PresentInterval.Immediate;
				}
				this.DeviceMain = new global::SlimDX.Direct3D9.Device(direct3D, defaultAdapter.Adapter, global::SlimDX.Direct3D9.DeviceType.Hardware, this.Form_Main.Handle, createFlags, new PresentParameters[] { this.GlobalData.presentParams });
				this.GlobalData.SpriteMain = new MySprite(this.DeviceMain);
				flag = true;
			}
			catch
			{
				MessageBox.Show("DirectX初始化失败", "DirectX Initial Error");
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000217DC File Offset: 0x0001F9DC
		public void AddState()
		{
			Watch watch = new Watch();
			watch.Start();
			this.GlobalData.BGMVolume = this.bgmVolume;
			this.GlobalData.SEVolume = this.seVolume;
			this.GlobalData.FontType = this.fontType;
			this.GlobalData.LoadResources();
			this.GameStatesDictionary = new Dictionary<string, IGameState>
			{
				{ "SplashScreen", this.PresentState },
				{
					"Stage_test",
					new GameState_testStage(this.GlobalData)
				},
				{
					"St1",
					new GameState_SSS01(this.GlobalData)
				},
				{
					"St2",
					new GameState_SSS02(this.GlobalData)
				},
				{
					"St3",
					new GameState_SSS03(this.GlobalData)
				},
				{
					"St4",
					new GameState_SSS04(this.GlobalData)
				},
				{
					"St5",
					new GameState_SSS05(this.GlobalData)
				},
				{
					"St6",
					new GameState_SSS06(this.GlobalData)
				},
				{
					"StEx",
					new GameState_SSSEx(this.GlobalData)
				},
				{
					"Bs1",
					new GameState_Boss01(this.GlobalData)
				},
				{
					"Bs2",
					new GameState_Boss02(this.GlobalData)
				},
				{
					"Bs3",
					new GameState_Boss03(this.GlobalData)
				},
				{
					"Bs4",
					new GameState_Boss04(this.GlobalData)
				},
				{
					"Bs5",
					new GameState_Boss05(this.GlobalData)
				},
				{
					"Bs6",
					new GameState_Boss06(this.GlobalData)
				},
				{
					"All",
					new GameState_ED(this.GlobalData)
				},
				{
					"PauseMenu",
					new GameState_PauseMenu(this.GlobalData)
				},
				{
					"MainMenu",
					new GameState_MainMenu(this.GlobalData)
				},
				{
					"GameOverMenu",
					new GameState_GameOverMenu(this.GlobalData)
				},
				{
					"GameSetMenu",
					new GameState_GameSetMenu(this.GlobalData)
				},
				{
					"FinalReplaySaveMenu",
					new GameState_ReplaySaveMenu(this.GlobalData)
				},
				{
					"GAME_OVER",
					new GameState_SplashScreen2(this.GlobalData)
					{
						TxtureObject = this.GlobalData.TextureObjectDictionary["GAME_OVER"]
					}
				},
				{
					"此功能尚未开通",
					new GameState_SplashScreen2(this.GlobalData)
					{
						TxtureObject = this.GlobalData.TextureObjectDictionary["此功能尚未开通"]
					}
				},
				{
					"MusicLoading",
					new GameState_MusicLoading(this.GlobalData)
				}
			};
			this.GlobalData.Rep = new Replay();
			if (!this.singleThreaded)
			{
				watch.SleepTo(3000f);
			}
			if (this.antiAlias)
			{
				this.DeviceMain.SetRenderState(RenderState.MultisampleAntialias, true);
			}
			this.GlobalData.LastState = this.PresentState;
			this.InitFlag = true;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00021B08 File Offset: 0x0001FD08
		public void MainProcess()
		{
			if (this.firstFrame)
			{
				this.fpsTimer.Start();
				this.firstFrame = false;
			}
			else if (!this.fixedFPS || this.PresentState is GameState_SplashScreen)
			{
				this.fpsTimer.Start();
			}
			else
			{
				for (int i = 0; i < this.presentInterval; i++)
				{
					this.fpsTimer.IncreaseOneFrame();
				}
			}
			if (this.InitFlag && this.GlobalData.LoadingList.Count == 0)
			{
				this.GlobalData.LoadModelDictionary();
				this.InitFlag = false;
				this.PresentState.StateSwitchData = new StateSwitchDataPackage
				{
					NextState = "MainMenu",
					NeedInit = true,
					SDPswitch = new StageDataPackage(this.GlobalData)
				};
			}
			if (this.PresentState.StateSwitchData != null)
			{
				StateSwitchDataPackage stateSwitchData = this.PresentState.StateSwitchData;
				if (stateSwitchData.NextState == "GameExit")
				{
					this.Form_Main.Close();
					return;
				}
				if (stateSwitchData.NextState != null)
				{
					if (stateSwitchData.NextState == "MainMenu")
					{
						if (this.GlobalData.Rep != null)
						{
							this.GlobalData.Rep.CloseRpy();
						}
					}
					this.GlobalData.BGM_Pause();
					this.PresentState.StateSwitchData = null;
					this.PresentState = this.GameStatesDictionary[stateSwitchData.NextState];
					if (stateSwitchData.SDPswitch != null)
					{
						this.PresentState.StageData = stateSwitchData.SDPswitch;
					}
					if (stateSwitchData.NeedInit)
					{
						this.PresentState.Init();
						this.PresentState.BGM_ON();
					}
					else
					{
						this.GlobalData.BGM_Resume();
					}
				}
			}
			if (this.useDirectInput)
			{
				KeyClass keyClass = new KeyClass();
				KeyClass keyClass2 = new KeyClass();
				try
				{
					this.KCapture.UpdateInput(ref keyClass);
				}
				catch
				{
				}
				try
				{
					this.JCapture.UpdateInput(ref keyClass2);
				}
				catch
				{
				}
				KeyClass.KeyMix(ref this.GlobalData.KClass, keyClass, keyClass2);
				if (this.KCapture.currentKeyboardState != null)
				{
					if (this.KCapture.currentKeyboardState.IsPressed(Key.LeftAlt) && this.KCapture.currentKeyboardState.IsPressed(Key.F4))
					{
						this.Form_Main.Close();
						return;
					}
					if (this.KCapture.currentKeyboardState.IsPressed(Key.F8) && this.KCapture.lastKeyboardState.IsReleased(Key.F8))
					{
						this.WindowSwitch();
					}
					if (this.KCapture.currentKeyboardState.IsPressed(Key.P) && this.KCapture.lastKeyboardState.IsReleased(Key.P))
					{
						this.ScreenShot();
					}
				}
			}
			if (this.deviceLost)
			{
				Thread.Sleep(15);
				Result result = this.DeviceMain.TestCooperativeLevel();
				if (result == global::SlimDX.Direct3D9.ResultCode.DeviceNotReset)
				{
					this.GlobalData.OnResetDevice();
					this.deviceLost = false;
					this.firstFrame = true;
				}
			}
			else
			{
				try
				{
					if (this.OnPause)
					{
						if (this.PresentState is BaseStage)
						{
							this.GlobalData.KClass.Key_ESC = true;
						}
					}
					this.PresentState.UpdateData();
					for (int i = 1; i < this.presentInterval; i++)
					{
						if (this.PresentState.StateSwitchData != null)
						{
							if (this.PresentState.StateSwitchData.NextState == null)
							{
								this.PresentState.UpdateData();
							}
						}
						else
						{
							this.PresentState.UpdateData();
						}
					}
					this.DeviceMain.Clear(ClearFlags.ZBuffer | ClearFlags.Target, Color.Black, 1f, 0);
					this.DeviceMain.BeginScene();
					this.PresentState.Render();
					this.DeviceMain.EndScene();
					this.DeviceMain.Present(Present.None);
					if (this.GlobalData.BGM_Player.OnPause)
					{
						this.PresentState.BGM_Resume();
					}
				}
				catch (Direct3D9Exception ex)
				{
					if (ex.ResultCode == global::SlimDX.Direct3D9.ResultCode.DeviceLost)
					{
						this.GlobalData.OnLostDevice();
						this.deviceLost = true;
					}
					else
					{
						ErrorLog.SaveErrorLog(ex.Message, ex.ResultCode.Description);
						MessageBox.Show(ex.Message);
						this.Form_Main.Close();
					}
				}
				if (!this.verticalSync || (this.presentInterval != 1 && !this.fullWindow))
				{
					float num = 16f * (float)this.presentInterval - this.fpsTimer.GetDuration();
					if (num > 0f)
					{
						Thread.Sleep((int)num);
					}
					while (16.62f * (float)this.presentInterval - this.fpsTimer.GetDuration() > 0f)
					{
					}
					if (this.fixedFPS)
					{
						if (num < -16.66667f * (float)this.delayTolerance)
						{
							int i = 0;
							while ((float)i < -num / 16.66667f - (float)this.delayTolerance)
							{
								this.fpsTimer.IncreaseOneFrame();
								i++;
							}
						}
					}
				}
			}
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0002219C File Offset: 0x0002039C
		private void ToggleFullScreen()
		{
			this.fullWindow = !this.fullWindow;
			this.GlobalData.presentParams.Windowed = !this.fullWindow;
			if (this.fullWindow && this.verticalSync)
			{
				if (this.presentInterval == 2)
				{
					this.GlobalData.presentParams.PresentationInterval = PresentInterval.Two;
				}
				else if (this.presentInterval == 3)
				{
					this.GlobalData.presentParams.PresentationInterval = PresentInterval.Three;
				}
			}
			else if (this.presentInterval == 2 || this.presentInterval == 3)
			{
				this.GlobalData.presentParams.PresentationInterval = PresentInterval.Immediate;
			}
			this.GlobalData.OnLostDevice();
			this.GlobalData.OnResetDevice();
			MouseCtrl.DrawCursor(!this.fullWindow);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00022298 File Offset: 0x00020498
		private void WindowSwitch()
		{
			if (this.fullWindow)
			{
				this.ToggleFullScreen();
				this.Form_Main.ClientSize = new Size(640, 480);
			}
			else
			{
				switch (this.windowSize)
				{
				case 0:
					this.windowSize++;
					this.Form_Main.ClientSize = new Size(800, 600);
					break;
				case 1:
					this.windowSize++;
					this.Form_Main.ClientSize = new Size(1024, 768);
					break;
				case 2:
					this.windowSize = 0;
					this.ToggleFullScreen();
					break;
				}
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00022364 File Offset: 0x00020564
		private void Form_Main_Deactivate(object sender, EventArgs e)
		{
			if (this.initSuccess)
			{
				this.OnPause = true;
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00022388 File Offset: 0x00020588
		private void Form_Main_Activated(object sender, EventArgs e)
		{
			if (this.initSuccess)
			{
				this.OnPause = false;
				this.GlobalData.KClass.Key_ESC = false;
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x000223C0 File Offset: 0x000205C0
		private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.LoadingThread != null && this.LoadingThread.IsAlive)
			{
				this.LoadingThread.Abort();
			}
			this.GlobalData.PData.SaveData();
			this.GlobalData.Dispose();
			if (!this.useDirectInput)
			{
				this.GlobalData.DeviceMain.Dispose();
			}
			if (this.GameStatesDictionary != null)
			{
				foreach (IGameState gameState in this.GameStatesDictionary.Values)
				{
					gameState.Dispose();
				}
			}
			try
			{
				INI_RW ini_RW = new INI_RW(".\\Setting.INI");
				if (ini_RW.ExistINIFile())
				{
					ini_RW.IniWriteValue("Volume", "BGMVolume", this.GlobalData.BGMVolume.ToString());
					ini_RW.IniWriteValue("Volume", "SEVolume", this.GlobalData.SEVolume.ToString());
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00022508 File Offset: 0x00020708
		private void Form_Main_KeyDown(object sender, KeyEventArgs e)
		{
			this.GlobalData.KClass.ArrowLeft = e.KeyCode.ToString() == this.mapkey.Left || this.GlobalData.KClass.ArrowLeft;
			this.GlobalData.KClass.ArrowRight = e.KeyCode.ToString() == this.mapkey.Right || this.GlobalData.KClass.ArrowRight;
			this.GlobalData.KClass.ArrowUp = e.KeyCode.ToString() == this.mapkey.Up || this.GlobalData.KClass.ArrowUp;
			this.GlobalData.KClass.ArrowDown = e.KeyCode.ToString() == this.mapkey.Down || this.GlobalData.KClass.ArrowDown;
			this.GlobalData.KClass.Key_Shift = e.KeyCode.ToString() == this.mapkey.Slow || this.GlobalData.KClass.Key_Shift;
			this.GlobalData.KClass.Key_Z = e.KeyCode.ToString() == this.mapkey.Shoot || this.GlobalData.KClass.Key_Z;
			this.GlobalData.KClass.Key_X = e.KeyCode.ToString() == this.mapkey.Bomb || this.GlobalData.KClass.Key_X;
			string text = e.KeyCode.ToString();
			switch (text)
			{
			case "R":
				this.GlobalData.KClass.Key_C = true;
				break;
			case "ControlKey":
				this.GlobalData.KClass.Key_Ctrl = true;
				this.GlobalData.KClass.Key_plus = true;
				break;
			case "ShiftKey":
				this.GlobalData.KClass.Key_minus = true;
				break;
			case "Return":
				this.GlobalData.KClass.Key_Z = true;
				break;
			case "Escape":
				this.GlobalData.KClass.Key_ESC = true;
				break;
			case "Oemplus":
				this.GlobalData.KClass.Key_plus = true;
				break;
			case "OemMinus":
				this.GlobalData.KClass.Key_minus = true;
				break;
			case "F8":
				this.WindowSwitch();
				break;
			case "P":
				this.ScreenShot();
				break;
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00022884 File Offset: 0x00020A84
		private void Form_Main_KeyUp(object sender, KeyEventArgs e)
		{
			this.GlobalData.KClass.ArrowLeft = !(e.KeyCode.ToString() == this.mapkey.Left) && this.GlobalData.KClass.ArrowLeft;
			this.GlobalData.KClass.ArrowRight = !(e.KeyCode.ToString() == this.mapkey.Right) && this.GlobalData.KClass.ArrowRight;
			this.GlobalData.KClass.ArrowUp = !(e.KeyCode.ToString() == this.mapkey.Up) && this.GlobalData.KClass.ArrowUp;
			this.GlobalData.KClass.ArrowDown = !(e.KeyCode.ToString() == this.mapkey.Down) && this.GlobalData.KClass.ArrowDown;
			this.GlobalData.KClass.Key_Shift = !(e.KeyCode.ToString() == this.mapkey.Slow) && this.GlobalData.KClass.Key_Shift;
			this.GlobalData.KClass.Key_Z = !(e.KeyCode.ToString() == this.mapkey.Shoot) && this.GlobalData.KClass.Key_Z;
			this.GlobalData.KClass.Key_X = !(e.KeyCode.ToString() == this.mapkey.Bomb) && this.GlobalData.KClass.Key_X;
			string text = e.KeyCode.ToString();
			switch (text)
			{
			case "R":
				this.GlobalData.KClass.Key_C = false;
				break;
			case "ControlKey":
				this.GlobalData.KClass.Key_Ctrl = false;
				this.GlobalData.KClass.Key_plus = false;
				break;
			case "ShiftKey":
				this.GlobalData.KClass.Key_minus = false;
				break;
			case "Return":
				this.GlobalData.KClass.Key_Z = false;
				break;
			case "Escape":
				this.GlobalData.KClass.Key_ESC = false;
				break;
			case "Oemplus":
				this.GlobalData.KClass.Key_plus = false;
				break;
			case "OemMinus":
				this.GlobalData.KClass.Key_minus = false;
				break;
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00022BCC File Offset: 0x00020DCC
		private void ScreenShot()
		{
			Surface backBuffer = this.DeviceMain.GetBackBuffer(0, 0);
			int num = 0;
			while (File.Exists(string.Format(".\\ScreenShot\\screen{0:0000}.jpg", num)))
			{
				num++;
			}
			Surface.ToFile(backBuffer, string.Format(".\\ScreenShot\\screen{0:0000}.jpg", num), ImageFileFormat.Jpg);
			if (this.GlobalData != null)
			{
				if (this.GlobalData.SoundDictionary != null)
				{
					if (this.GlobalData.SoundDictionary.ContainsKey("se_shutter.wav"))
					{
						this.GlobalData.SoundDictionary["se_shutter.wav"].Play();
					}
				}
			}
		}

		// Token: 0x04000151 RID: 337
		public RenderForm_Main Form_Main;

		// Token: 0x04000152 RID: 338
		private GlobalDataPackage GDP = new GlobalDataPackage();

		// Token: 0x04000153 RID: 339
		private IGameState PresentState;

		// Token: 0x04000154 RID: 340
		private Dictionary<string, IGameState> GameStatesDictionary;

		// Token: 0x04000155 RID: 341
		private Thread LoadingThread;

		// Token: 0x04000156 RID: 342
		private Watch fpsTimer = new Watch();

		// Token: 0x04000157 RID: 343
		private KeyboardCapture KCapture;

		// Token: 0x04000158 RID: 344
		private JoystickCapture JCapture;

		// Token: 0x04000159 RID: 345
		private MapKey mapkey = new MapKey();

		// Token: 0x0400015A RID: 346
		private bool fullWindow = false;

		// Token: 0x0400015B RID: 347
		private bool useDirectInput = false;

		// Token: 0x0400015C RID: 348
		private bool verticalSync = false;

		// Token: 0x0400015D RID: 349
		private bool antiAlias = false;

		// Token: 0x0400015E RID: 350
		private bool deviceLost = false;

		// Token: 0x0400015F RID: 351
		private bool singleThreaded = false;

		// Token: 0x04000160 RID: 352
		private int windowSize = 0;

		// Token: 0x04000161 RID: 353
		private int presentInterval = 1;

		// Token: 0x04000162 RID: 354
		private bool fixedFPS = false;

		// Token: 0x04000163 RID: 355
		private int delayTolerance = 10;

		// Token: 0x04000164 RID: 356
		private int bgmVolume;

		// Token: 0x04000165 RID: 357
		private int seVolume;

		// Token: 0x04000166 RID: 358
		private string fontType;

		// Token: 0x04000167 RID: 359
		public bool initSuccess = false;

		// Token: 0x04000168 RID: 360
		private bool firstFrame = true;

		// Token: 0x04000169 RID: 361
		private bool OnPause = false;

		// Token: 0x0400016A RID: 362
		private bool InitFlag = false;
	}
}
