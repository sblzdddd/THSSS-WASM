using System;
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
				return this.GlobalData.DeviceMain;
			}
		}

		public StateSwitchDataPackage StateSwitchData
		{
			get
			{
				return this.StageData.StateSwitchData;
			}
			set
			{
				this.StageData.StateSwitchData = value;
			}
		}

		public MySprite SpriteMain
		{
			get
			{
				return this.StageData.SpriteMain;
			}
		}

		public int TimeMain { get; set; }

		public BackgroundManager Background
		{
			get
			{
				return this.StageData.Background;
			}
			set
			{
				this.StageData.Background = value;
			}
		}

		public BackgroundManager3D Background3D
		{
			get
			{
				return this.StageData.Background3D;
			}
			set
			{
				this.StageData.Background3D = value;
			}
		}

		public ParticleManager3D Particle3D
		{
			get
			{
				return this.StageData.Particle3D;
			}
			set
			{
				this.StageData.Particle3D = value;
			}
		}

		public GameState_SplashScreen()
		{
		}

		public GameState_SplashScreen(GlobalDataPackage GlobalData)
		{
			this.GlobalData = GlobalData;
			this.LoadTexture();
		}

		public void LoadTexture()
		{
			this.GlobalData.TextureObjectLoader(".\\Image\\NowLoading\\NowLoading.png");
			this.GlobalData.TextureObjectLoader(".\\Image\\NowLoading\\NowLoading-0.png");
			this.GlobalData.TextureObjectLoader(".\\Image\\NowLoading\\NowLoading-1.png");
			this.GlobalData.TextureObjectLoader(".\\Image\\NowLoading\\Splash_Screen.png");
			this.GlobalData.TextureObjectLoader(".\\Image\\NowLoading\\Star.png");
			this.GlobalData.TextureObjectLoader(".\\Image\\NowLoading\\sssssstar.png");
			this.GlobalData.TextureObjectLoader(".\\Image\\NowLoading\\back.png");
			this.TxtureObject = this.GlobalData.TextureObjectDictionary["NowLoading-0"];
		}

		public virtual void Init()
		{
			if (this.StageData == null)
			{
				this.StageData = new StageDataPackage(this.GlobalData);
			}
			this.Background = new BackgroundManager();
			this.TimeMain = 0;
			this.Background.Light = 0;
			this.StageData.EffectList = new List<BaseEffect>();
			this.StageData.BoundRect = new Rectangle(450, 390, 640, 480);
		}

		public virtual void UpdateData()
		{
			this.TimeMain++;
			if (this.TimeMain < 50)
			{
				this.Background.Light += 5;
			}
			else if (this.TimeMain > 60)
			{
				if (this.TimeMain % 60 >= 30)
				{
					this.Background.Light += 4;
				}
				else if (this.TimeMain % 60 < 30)
				{
					this.Background.Light -= 4;
				}
			}
			if (this.Background.Light < 0)
			{
				this.Background.Light = 0;
			}
			else if (this.Background.Light > 255)
			{
				this.Background.Light = 255;
			}
			if (this.TimeMain == 1)
			{
				BaseObject baseObject = new BaseObject(this.StageData, "Splash_Screen", new PointF(320f, 240f), 0f, 1.5707963267948966)
				{
					OutsideRegion = 10000
				};
				this.Background.BackgroundList.Add(baseObject);
				baseObject = new BaseObject(this.StageData, "sssssstar", new PointF(440f, 160f), 0f, 1.5707963267948966)
				{
					DirectionVelocityDegree = 0.5f,
					OutsideRegion = 10000
				};
				this.Background.BackgroundList.Add(baseObject);
				baseObject = new BaseObject(this.StageData, "back", new PointF(320f, 240f), 0f, 1.5707963267948966)
				{
					OutsideRegion = 10000
				};
				this.Background.BackgroundList.Add(baseObject);
			}
			this.Background.Ctrl();
			Particle particle = new Particle(this.StageData, "Star", new PointF((float)this.StageData.Ran.Next(this.StageData.BoundRect.Left - 50, this.StageData.BoundRect.Right), (float)this.StageData.Ran.Next(this.StageData.BoundRect.Top + 50, this.StageData.BoundRect.Top + 100)), (float)(this.StageData.Ran.Next(10, 20) / 10), -1.5707963267948966 - (double)((float)this.StageData.Ran.Next(1, 5) / 10f));
			particle.Active = true;
			particle.Scale = (float)this.StageData.Ran.Next(25, 70) / 100f;
			particle.LifeTime = 70;
			if (this.GlobalData.LoadingList.Count > 0)
			{
				this.GlobalData.TextureObjectLoader(this.GlobalData.LoadingList[0]);
				this.GlobalData.LoadingList.RemoveAt(0);
			}
		}

		public virtual void Render()
		{
			for (int i = this.StageData.EffectList.Count - 1; i >= 0; i--)
			{
				this.StageData.EffectList[i].Ctrl();
			}
			this.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			this.Background.Show(false);
			this.StageData.EffectList.ForEach(delegate(BaseEffect x)
			{
				if (!x.Active)
				{
					x.Show();
				}
			});
			this.SpriteMain.End();
			this.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			this.DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
			this.DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
			this.SpriteMain.Draw2D(this.TxtureObject, 1f, 0f, new Point(520, 432), this.Background.Light);
			this.StageData.EffectList.ForEach(delegate(BaseEffect x)
			{
				if (x.Active)
				{
					x.Show();
				}
			});
			this.SpriteMain.End();
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
			if (this.StageData != null)
			{
				this.StageData.Dispose();
			}
		}
	}
}
