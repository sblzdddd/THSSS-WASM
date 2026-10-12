using System.Collections.Generic;
using System.Drawing;
using SlimDX.Direct3D9;

namespace Shooting
{
	internal class GameState_SplashScreen : IGameState
	{
		public TextureObject TxtureObject { get; set; }

		public TextureObject TxtureObject1 { get; set; }

		public GlobalDataPackage GlobalData { get; set; }

		public StageDataPackage StageData { get; set; }

		public string StageName { get; set; }

		public Device DeviceMain
		{
			get
			{
				return GlobalData.DeviceMain;
			}
		}

		public StateSwitchDataPackage StateSwitchData
		{
			get
			{
				return StageData.StateSwitchData;
			}
			set
			{
				StageData.StateSwitchData = value;
			}
		}

		public MySprite SpriteMain
		{
			get
			{
				return StageData.SpriteMain;
			}
		}

		public int TimeMain { get; set; }

		public BackgroundManager Background
		{
			get
			{
				return StageData.Background;
			}
			set
			{
				StageData.Background = value;
			}
		}

		public BackgroundManager3D Background3D
		{
			get
			{
				return StageData.Background3D;
			}
			set
			{
				StageData.Background3D = value;
			}
		}

		public ParticleManager3D Particle3D
		{
			get
			{
				return StageData.Particle3D;
			}
			set
			{
				StageData.Particle3D = value;
			}
		}

		public GameState_SplashScreen()
		{
		}

		public GameState_SplashScreen(GlobalDataPackage GlobalData)
		{
			this.GlobalData = GlobalData;
			LoadTexture();
		}

		public void LoadTexture()
		{
			GlobalData.TextureObjectLoader(".\\Image\\NowLoading\\NowLoading.png");
			GlobalData.TextureObjectLoader(".\\Image\\NowLoading\\NowLoading-0.png");
			GlobalData.TextureObjectLoader(".\\Image\\NowLoading\\NowLoading-1.png");
			GlobalData.TextureObjectLoader(".\\Image\\NowLoading\\Splash_Screen.png");
			GlobalData.TextureObjectLoader(".\\Image\\NowLoading\\Star.png");
			GlobalData.TextureObjectLoader(".\\Image\\NowLoading\\sssssstar.png");
			GlobalData.TextureObjectLoader(".\\Image\\NowLoading\\back.png");
			TxtureObject = GlobalData.TextureObjectDictionary["NowLoading-0"];
		}

		public virtual void Init()
		{
			if (StageData == null)
			{
				StageData = new StageDataPackage(GlobalData);
			}
			Background = new BackgroundManager();
			TimeMain = 0;
			Background.Light = 0;
			StageData.EffectList = new List<BaseEffect>();
			StageData.BoundRect = new Rectangle(450, 390, 640, 480);
		}

		public virtual void UpdateData()
		{
			TimeMain++;
			if (TimeMain < 50)
			{
				Background.Light += 5;
			}
			else if (TimeMain > 60)
			{
				if (TimeMain % 60 >= 30)
				{
					Background.Light += 4;
				}
				else if (TimeMain % 60 < 30)
				{
					Background.Light -= 4;
				}
			}
			if (Background.Light < 0)
			{
				Background.Light = 0;
			}
			else if (Background.Light > 255)
			{
				Background.Light = 255;
			}
			if (TimeMain == 1)
			{
				var baseObject = new BaseObject(StageData, "Splash_Screen", new PointF(320f, 240f), 0f, 1.5707963267948966)
				{
					OutsideRegion = 10000
				};
				Background.BackgroundList.Add(baseObject);
				baseObject = new BaseObject(StageData, "sssssstar", new PointF(440f, 160f), 0f, 1.5707963267948966)
				{
					DirectionVelocityDegree = 0.5f,
					OutsideRegion = 10000
				};
				Background.BackgroundList.Add(baseObject);
				baseObject = new BaseObject(StageData, "back", new PointF(320f, 240f), 0f, 1.5707963267948966)
				{
					OutsideRegion = 10000
				};
				Background.BackgroundList.Add(baseObject);
			}
			Background.Ctrl();
			var particle = new Particle(StageData, "Star", new PointF((float)StageData.Ran.Next(StageData.BoundRect.Left - 50, StageData.BoundRect.Right), (float)StageData.Ran.Next(StageData.BoundRect.Top + 50, StageData.BoundRect.Top + 100)), (float)(StageData.Ran.Next(10, 20) / 10), -1.5707963267948966 - (double)((float)StageData.Ran.Next(1, 5) / 10f));
			particle.Active = true;
			particle.Scale = (float)StageData.Ran.Next(25, 70) / 100f;
			particle.LifeTime = 70;
			for (var i = StageData.EffectList.Count - 1; i >= 0; i--)
			{
				StageData.EffectList[i].Ctrl();
			}
			if (GlobalData.LoadingList.Count > 0)
			{
#if BROWSER
				Thsss.Browser.TexturePump.Advance(GlobalData);
#else
				this.GlobalData.TextureObjectLoader(this.GlobalData.LoadingList[0]);
				this.GlobalData.LoadingList.RemoveAt(0);
#endif
			}
		}

		public virtual void Render()
		{
			SpriteMain.Begin(SpriteFlags.AlphaBlend);
			Background.Show(false);
			StageData.EffectList.ForEach(delegate(BaseEffect x)
			{
				if (!x.Active)
				{
					x.Show();
				}
			});
			SpriteMain.End();
			SpriteMain.Begin(SpriteFlags.AlphaBlend);
			DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
			DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
			SpriteMain.Draw2D(TxtureObject, 1f, 0f, new Point(520, 432), Background.Light);
			StageData.EffectList.ForEach(delegate(BaseEffect x)
			{
				if (x.Active)
				{
					x.Show();
				}
			});
			SpriteMain.End();
		}

		public void BGM_ON()
		{
		}

		public virtual void BGM_Resume()
		{
		}

		public virtual void BGM_Pause()
		{
		}

		public void Dispose()
		{
			if (StageData != null)
			{
				StageData.Dispose();
			}
		}
	}
}
