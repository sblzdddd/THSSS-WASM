using System;
using System.Collections.Generic;
using System.Drawing;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x020001A4 RID: 420
	internal class GameState_SplashScreen : IGameState
	{
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x0005E7AC File Offset: 0x0005C9AC
		// (set) Token: 0x060009E0 RID: 2528 RVA: 0x0005E7C3 File Offset: 0x0005C9C3
		public TextureObject TxtureObject { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0005E7CC File Offset: 0x0005C9CC
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x0005E7E3 File Offset: 0x0005C9E3
		public TextureObject TxtureObject1 { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x0005E7EC File Offset: 0x0005C9EC
		// (set) Token: 0x060009E4 RID: 2532 RVA: 0x0005E803 File Offset: 0x0005CA03
		public GlobalDataPackage GlobalData { get; set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x0005E80C File Offset: 0x0005CA0C
		// (set) Token: 0x060009E6 RID: 2534 RVA: 0x0005E823 File Offset: 0x0005CA23
		public StageDataPackage StageData { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x0005E82C File Offset: 0x0005CA2C
		// (set) Token: 0x060009E8 RID: 2536 RVA: 0x0005E843 File Offset: 0x0005CA43
		public string StageName { get; set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x0005E84C File Offset: 0x0005CA4C
		public Device DeviceMain
		{
			get
			{
				return this.GlobalData.DeviceMain;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x0005E86C File Offset: 0x0005CA6C
		// (set) Token: 0x060009EB RID: 2539 RVA: 0x0005E889 File Offset: 0x0005CA89
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

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x0005E898 File Offset: 0x0005CA98
		public MySprite SpriteMain
		{
			get
			{
				return this.StageData.SpriteMain;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x0005E8B8 File Offset: 0x0005CAB8
		// (set) Token: 0x060009EE RID: 2542 RVA: 0x0005E8CF File Offset: 0x0005CACF
		public int TimeMain { get; set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x0005E8D8 File Offset: 0x0005CAD8
		// (set) Token: 0x060009F0 RID: 2544 RVA: 0x0005E8F5 File Offset: 0x0005CAF5
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

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x0005E904 File Offset: 0x0005CB04
		// (set) Token: 0x060009F2 RID: 2546 RVA: 0x0005E921 File Offset: 0x0005CB21
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

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0005E930 File Offset: 0x0005CB30
		// (set) Token: 0x060009F4 RID: 2548 RVA: 0x0005E94D File Offset: 0x0005CB4D
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

		// Token: 0x060009F5 RID: 2549 RVA: 0x0005E95C File Offset: 0x0005CB5C
		public GameState_SplashScreen()
		{
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0005E967 File Offset: 0x0005CB67
		public GameState_SplashScreen(GlobalDataPackage GlobalData)
		{
			this.GlobalData = GlobalData;
			this.LoadTexture();
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0005E984 File Offset: 0x0005CB84
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

		// Token: 0x060009F8 RID: 2552 RVA: 0x0005EA28 File Offset: 0x0005CC28
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

		// Token: 0x060009F9 RID: 2553 RVA: 0x0005EAB0 File Offset: 0x0005CCB0
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

		// Token: 0x060009FA RID: 2554 RVA: 0x0005EE5C File Offset: 0x0005D05C
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

		// Token: 0x060009FB RID: 2555 RVA: 0x0005EFA1 File Offset: 0x0005D1A1
		public void BGM_ON()
		{
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0005EFA4 File Offset: 0x0005D1A4
		public virtual void BGM_Resume()
		{
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0005EFA7 File Offset: 0x0005D1A7
		public virtual void BGM_Pause()
		{
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0005EFAC File Offset: 0x0005D1AC
		public void Dispose()
		{
			if (this.StageData != null)
			{
				this.StageData.Dispose();
			}
		}
	}
}
