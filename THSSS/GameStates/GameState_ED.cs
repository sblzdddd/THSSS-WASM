using System;
using System.Drawing;
using System.IO;
using Shooting.Planes.Story;
using SlimDX.Direct3D9;

namespace Shooting
{
	internal class GameState_ED : BaseGameState, IGameState
	{
		public GameState_ED(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			StageName = "All";
			System.Drawing.Font font = new System.Drawing.Font("Arial", 14f * GlobalDataPackage.FontScale, FontStyle.Bold | FontStyle.Italic);
			DXFont = new SlimDX.Direct3D9.Font(DeviceMain, font);
		}

		public override void UpdateData()
		{
			TimeMain++;
			SoundPlayList.Clear();
			if (StageData.Rep != null)
			{
				if (StageData.OnReplay)
				{
					int num = StageData.Rep.ReadKey();
					if (num == 57358)
					{
						num = StageData.Rep.ReadKey();
					}
					if (num == 4080)
					{
						StateSwitchData = new StateSwitchDataPackage
						{
							NextState = "GameOverMenu",
							NeedInit = true,
							SDPswitch = new StageDataPackage(GlobalData)
						};
						GlobalData.LastState = this;
						StageData.KClass.Hex2Key(0);
						StageData.SoundPlay("se_pause.wav");
					}
					else
					{
						StageData.KClass.Hex2Key(num);
					}
				}
			}
			Background3D.Ctrl();
			Background.Ctrl();
			Background2.Ctrl();
			Drama();
			if (Story != null)
			{
				Story.Ctrl();
			}
			for (int i = EnemyPlaneList.Count - 1; i >= 0; i--)
			{
				EnemyPlaneList[i].Ctrl();
			}
			for (int i = BulletList.Count - 1; i >= 0; i--)
			{
				BulletList[i].Ctrl();
			}
			for (int i = EffectList.Count - 1; i >= 0; i--)
			{
				EffectList[i].Ctrl();
			}
			Particle3D.Ctrl();
			for (int i = InterfaceList.Count - 1; i >= 0; i--)
			{
				InterfaceList[i].Ctrl();
			}
			StageData.Vibrate();
			SoundPlayList.ForEach(delegate(XAudio2_Player x)
			{
				x.Play();
			});
			if (TimeMain % 6 != 0 && KClass.Key_plus && MyPlane.Life >= 0 && StateSwitchData == null)
			{
				UpdateData();
			}
			GlobalData.LastState = this;
		}

		public override void Init()
		{
			StageData.C_History = StageData.PData.C_History.Find(new Predicate<ClearHistory>(StageData.FindClear));
		}

		public void Drama()
		{
			if (TimeMain == 2)
			{
				if (StageData.C_History.PracticeLevel < 7)
				{
					StageData.C_History.PracticeLevel = 7;
				}
				StageFlag = false;
				BackgroundTransitionIn backgroundTransitionIn = new BackgroundTransitionIn(StageData);
				backgroundTransitionIn.Delay = 0;
				backgroundTransitionIn.LifeTime = 280;
				BoundRect = new Rectangle(128, 0, 384, 480);
				if (StageData.ContinueTimes == 0)
				{
					StageData.ChangeBGM(".\\BGM\\ED.ogg", 0, 0, 0, 0, 0);
					new Story_SSStaff(StageData);
				}
				StageData.CurrentStageName = "All";
				StageData.RepInfo.LastStage = StageData.CurrentStageName;
				StageData.C_History = StageData.PData.C_History.Find(new Predicate<ClearHistory>(StageData.FindClear));
				StageData.C_History.ClearTimes++;
				if (StageData.ContinueTimes == 0)
				{
					StageData.C_History.NoContinueClearTimes++;
				}
				StageData.S_History = StageData.PData.S_History.FindAll(new Predicate<ScoreHistory>(StageData.FindScore));
			}
			if (TimeMain > 2)
			{
				if (Story == null && !StageFlag)
				{
					StageFlag = true;
					TimeMain = 3;
				}
			}
			if (Story == null)
			{
				if (TimeMain == 5)
				{
					StageData.ChangeBGM(".\\BGM\\Staff.ogg", 0, 0, 0, 0, 0);
					string[] files = Directory.GetFiles(".\\CS\\Staff\\", "*.mbg");
					foreach (string text in files)
					{
						CS_Data cs_Data = StageData.LoadCS(text);
						CSEmitterController csemitterController = new CSEmitterController(StageData, cs_Data);
						csemitterController.OnRoad = true;
						csemitterController.Time = TimeMain - 60;
					}
					BoundRect = new Rectangle(128, 0, 640, 480);
				}
				if (StageFlag && TimeMain > keyTime[0] && KClass.Key_Z)
				{
					if (StageData.ContinueTimes == 0)
					{
						EndStage ed = new EndStage(StageData, "FinalReplaySaveMenu", true);
						ed.CtrlCall = delegate(string x)
						{
							if (ed.Time == ed.LifeTime)
							{
								BoundRect = new Rectangle(0, 0, 640, 480);
							}
						};
						GlobalData.LastState = this;
						if (StageData.C_History.PracticeLevel < 8)
						{
							StageData.C_History.PracticeLevel = 8;
						}
					}
					else
					{
						new EndStage(StageData, "MainMenu", true);
						GlobalData.LastState = this;
					}
				}
			}
		}

		public override void BGM_ON()
		{
		}

		public override void Render()
		{
			GlobalData.ScreenTexMan.Begin();
			Background3D.Show();
			SpriteMain.Begin(SpriteFlags.AlphaBlend);
			DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
			DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
			Background2.Show(true);
			SpriteMain.End();
			SpriteMain.Begin(SpriteFlags.AlphaBlend);
			Background2.Show(false);
			SpriteMain.End();
			SpriteMain.Begin(SpriteFlags.AlphaBlend | SpriteFlags.SortTexture);
			DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
			DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
			BulletList.ForEach(delegate(BaseBullet_Touhou x)
			{
				if (x.Active)
				{
					x.Show();
				}
			});
			EffectList.ForEach(delegate(BaseEffect x)
			{
				if (x.Active)
				{
					x.Show();
				}
			});
			SpriteMain.End();
			SpriteMain.Begin(SpriteFlags.AlphaBlend);
			BulletList.ForEach(delegate(BaseBullet_Touhou x)
			{
				if (!x.Active && x.Layer < 0)
				{
					x.Show();
				}
			});
			BulletList.ForEach(delegate(BaseBullet_Touhou x)
			{
				if (!x.Active && x.Layer == 0)
				{
					x.Show();
				}
			});
			BulletList.ForEach(delegate(BaseBullet_Touhou x)
			{
				if (!x.Active && x.Layer > 0)
				{
					x.Show();
				}
			});
			EffectList.ForEach(delegate(BaseEffect x)
			{
				if (!x.Active && x.Layer < 0)
				{
					x.Show();
				}
			});
			EffectList.ForEach(delegate(BaseEffect x)
			{
				if (!x.Active && x.Layer == 0)
				{
					x.Show();
				}
			});
			EffectList.ForEach(delegate(BaseEffect x)
			{
				if (!x.Active && x.Layer > 0)
				{
					x.Show();
				}
			});
			if (Story != null)
			{
				Story.Show();
			}
			SpriteMain.End();
			Particle3D.Show();
			GlobalData.ScreenTexMan.End();
			SpriteMain.Begin(SpriteFlags.AlphaBlend);
			GlobalData.EffectMain = GlobalData.EffectDictionary["Filter"];
			GlobalData.EffectMain.Technique = "technique1";
			GlobalData.EffectMain.Begin();
			GlobalData.EffectMain.BeginPass(0);
			GlobalData.SpriteMain.Draw2D(GlobalData.ScreenTexMan.RenderTexture, new Point(0, 0), 0f, new PointF(0f, 0f), Color.White);
			SpriteMain.End();
			GlobalData.EffectMain.EndPass();
			GlobalData.EffectMain.End();
		}

		private int testStartTime = 0;

		private bool StageFlag;

		private int[] keyTime = new int[] { 500 };
	}
}
