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
			base.StageName = "All";
			global::System.Drawing.Font font = new global::System.Drawing.Font("Arial", 14f * GlobalDataPackage.FontScale, FontStyle.Bold | FontStyle.Italic);
			base.DXFont = new SlimDX.Direct3D9.Font(base.DeviceMain, font);
		}

		public override void UpdateData()
		{
			base.TimeMain++;
			base.SoundPlayList.Clear();
			if (base.StageData.Rep != null)
			{
				if (base.StageData.OnReplay)
				{
					int num = base.StageData.Rep.ReadKey();
					if (num == 57358)
					{
						num = base.StageData.Rep.ReadKey();
					}
					if (num == 4080)
					{
						base.StateSwitchData = new StateSwitchDataPackage
						{
							NextState = "GameOverMenu",
							NeedInit = true,
							SDPswitch = new StageDataPackage(base.GlobalData)
						};
						base.GlobalData.LastState = this;
						base.StageData.KClass.Hex2Key(0);
						base.StageData.SoundPlay("se_pause.wav");
					}
					else
					{
						base.StageData.KClass.Hex2Key(num);
					}
				}
			}
			base.Background3D.Ctrl();
			base.Background.Ctrl();
			base.Background2.Ctrl();
			this.Drama();
			if (base.Story != null)
			{
				base.Story.Ctrl();
			}
			for (int i = base.EnemyPlaneList.Count - 1; i >= 0; i--)
			{
				base.EnemyPlaneList[i].Ctrl();
			}
			for (int i = base.BulletList.Count - 1; i >= 0; i--)
			{
				base.BulletList[i].Ctrl();
			}
			for (int i = base.EffectList.Count - 1; i >= 0; i--)
			{
				base.EffectList[i].Ctrl();
			}
			base.Particle3D.Ctrl();
			for (int i = base.InterfaceList.Count - 1; i >= 0; i--)
			{
				base.InterfaceList[i].Ctrl();
			}
			base.StageData.Vibrate();
			base.SoundPlayList.ForEach(delegate(XAudio2_Player x)
			{
				x.Play();
			});
			if (base.TimeMain % 6 != 0 && base.KClass.Key_plus && base.MyPlane.Life >= 0 && base.StateSwitchData == null)
			{
				this.UpdateData();
			}
			base.GlobalData.LastState = this;
		}

		public override void Init()
		{
			base.StageData.C_History = base.StageData.PData.C_History.Find(new Predicate<ClearHistory>(base.StageData.FindClear));
		}

		public void Drama()
		{
			if (base.TimeMain == 2)
			{
				if (base.StageData.C_History.PracticeLevel < 7)
				{
					base.StageData.C_History.PracticeLevel = 7;
				}
				this.StageFlag = false;
				BackgroundTransitionIn backgroundTransitionIn = new BackgroundTransitionIn(base.StageData);
				backgroundTransitionIn.Delay = 0;
				backgroundTransitionIn.LifeTime = 280;
				base.BoundRect = new Rectangle(128, 0, 384, 480);
				if (base.StageData.ContinueTimes == 0)
				{
					base.StageData.ChangeBGM(".\\BGM\\ED.ogg", 0, 0, 0, 0, 0);
					new Story_SSStaff(base.StageData);
				}
				base.StageData.CurrentStageName = "All";
				base.StageData.RepInfo.LastStage = base.StageData.CurrentStageName;
				base.StageData.C_History = base.StageData.PData.C_History.Find(new Predicate<ClearHistory>(base.StageData.FindClear));
				base.StageData.C_History.ClearTimes++;
				if (base.StageData.ContinueTimes == 0)
				{
					base.StageData.C_History.NoContinueClearTimes++;
				}
				base.StageData.S_History = base.StageData.PData.S_History.FindAll(new Predicate<ScoreHistory>(base.StageData.FindScore));
			}
			if (base.TimeMain > 2)
			{
				if (base.Story == null && !this.StageFlag)
				{
					this.StageFlag = true;
					base.TimeMain = 3;
				}
			}
			if (base.Story == null)
			{
				if (base.TimeMain == 5)
				{
					base.StageData.ChangeBGM(".\\BGM\\Staff.ogg", 0, 0, 0, 0, 0);
					string[] files = Directory.GetFiles(".\\CS\\Staff\\", "*.mbg");
					foreach (string text in files)
					{
						CS_Data cs_Data = base.StageData.LoadCS(text);
						CSEmitterController csemitterController = new CSEmitterController(base.StageData, cs_Data);
						csemitterController.OnRoad = true;
						csemitterController.Time = base.TimeMain - 60;
					}
					base.BoundRect = new Rectangle(128, 0, 640, 480);
				}
				if (this.StageFlag && base.TimeMain > this.keyTime[0] && base.KClass.Key_Z)
				{
					if (base.StageData.ContinueTimes == 0)
					{
						EndStage ed = new EndStage(base.StageData, "FinalReplaySaveMenu", true);
						ed.CtrlCall = delegate(string x)
						{
							if (ed.Time == ed.LifeTime)
							{
								this.BoundRect = new Rectangle(0, 0, 640, 480);
							}
						};
						base.GlobalData.LastState = this;
						if (base.StageData.C_History.PracticeLevel < 8)
						{
							base.StageData.C_History.PracticeLevel = 8;
						}
					}
					else
					{
						new EndStage(base.StageData, "MainMenu", true);
						base.GlobalData.LastState = this;
					}
				}
			}
		}

		public override void BGM_ON()
		{
		}

		public override void Render()
		{
			base.GlobalData.ScreenTexMan.Begin();
			base.Background3D.Show();
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
			base.DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
			base.Background2.Show(true);
			base.SpriteMain.End();
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.Background2.Show(false);
			base.SpriteMain.End();
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
			base.DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
			base.BulletList.ForEach(delegate(BaseBullet_Touhou x)
			{
				if (x.Active)
				{
					x.Show();
				}
			});
			base.EffectList.ForEach(delegate(BaseEffect x)
			{
				if (x.Active)
				{
					x.Show();
				}
			});
			base.SpriteMain.End();
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.BulletList.ForEach(delegate(BaseBullet_Touhou x)
			{
				if (!x.Active && x.Layer < 0)
				{
					x.Show();
				}
			});
			base.BulletList.ForEach(delegate(BaseBullet_Touhou x)
			{
				if (!x.Active && x.Layer == 0)
				{
					x.Show();
				}
			});
			base.BulletList.ForEach(delegate(BaseBullet_Touhou x)
			{
				if (!x.Active && x.Layer > 0)
				{
					x.Show();
				}
			});
			base.EffectList.ForEach(delegate(BaseEffect x)
			{
				if (!x.Active && x.Layer < 0)
				{
					x.Show();
				}
			});
			base.EffectList.ForEach(delegate(BaseEffect x)
			{
				if (!x.Active && x.Layer == 0)
				{
					x.Show();
				}
			});
			base.EffectList.ForEach(delegate(BaseEffect x)
			{
				if (!x.Active && x.Layer > 0)
				{
					x.Show();
				}
			});
			if (base.Story != null)
			{
				base.Story.Show();
			}
			base.SpriteMain.End();
			base.Particle3D.Show();
			base.GlobalData.ScreenTexMan.End();
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.GlobalData.EffectMain = base.GlobalData.EffectDictionary["Filter"];
			base.GlobalData.EffectMain.Technique = "technique1";
			base.GlobalData.EffectMain.Begin();
			base.GlobalData.EffectMain.BeginPass(0);
			base.GlobalData.SpriteMain.Draw2D(base.GlobalData.ScreenTexMan.RenderTexture, new Point(0, 0), 0f, new PointF(0f, 0f), Color.White);
			base.SpriteMain.End();
			base.GlobalData.EffectMain.EndPass();
			base.GlobalData.EffectMain.End();
		}

		private int testStartTime = 0;

		private bool StageFlag;

		private int[] keyTime = new int[] { 500 };
	}
}
