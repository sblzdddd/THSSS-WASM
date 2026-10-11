using System;
using System.Drawing;
using System.Threading;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x02000108 RID: 264
	internal class GameState_MusicLoading : BaseMenu, IGameState
	{
		// Token: 0x0600073F RID: 1855 RVA: 0x000418F3 File Offset: 0x0003FAF3
		public GameState_MusicLoading(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x000418FF File Offset: 0x0003FAFF
		public GameState_MusicLoading()
		{
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0004190A File Offset: 0x0003FB0A
		public override void Init()
		{
			this.LoadFlag = false;
			base.Background.Light = 0;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00041924 File Offset: 0x0003FB24
		public override void UpdateData()
		{
			base.UpdateData();
			if (base.TimeMain == 1)
			{
				this.LoadingThread = new Thread(new ThreadStart(this.PreloadMusic));
				this.LoadingThread.Start();
			}
			if (this.LoadFlag && base.TimeMain > 0)
			{
				base.StageData.StateSwitchData = new StateSwitchDataPackage
				{
					NextState = base.StageData.GlobalData.LastState.StageName,
					NeedInit = false
				};
				((BaseGameState)base.StageData.GlobalData.LastState).TimeSycn();
				this.LoadingThread.Abort();
			}
			if (base.TimeMain < 50)
			{
				base.Background.Light += 5;
			}
			else if (base.TimeMain > 60)
			{
				if (base.TimeMain % 60 >= 30)
				{
					base.Background.Light += 4;
				}
				else if (base.TimeMain % 60 < 30)
				{
					base.Background.Light -= 4;
				}
			}
			Particle particle = new Particle(base.StageData, "Star", new PointF((float)base.StageData.Ran.Next(400, 640), (float)base.StageData.Ran.Next(440, 580)), (float)(base.StageData.Ran.Next(10, 20) / 10), -1.5707963267948966 - (double)((float)base.StageData.Ran.Next(1, 5) / 10f));
			particle.Active = true;
			particle.Scale = (float)base.StageData.Ran.Next(25, 70) / 100f;
			particle.LifeTime = 70;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00041B34 File Offset: 0x0003FD34
		private void PreloadMusic()
		{
			if (base.GlobalData.LastState is BaseStage)
			{
				string stageName = ((BaseStage)base.GlobalData.LastState).StageName;
				switch (stageName)
				{
				case "St1":
					base.BGM_Player.PreLoad(".\\BGM\\Stage01.wav");
					base.BGM_Player.PreLoad(".\\BGM\\Boss01.wav");
					base.GlobalData.LastState.StageData.ClearCSCathe();
					base.GlobalData.LastState.StageData.PreLoadCS(".\\CS\\St01");
					break;
				case "St2":
					base.BGM_Player.PreLoad(".\\BGM\\Stage02.wav");
					base.BGM_Player.PreLoad(".\\BGM\\Boss02.wav");
					base.GlobalData.LastState.StageData.ClearCSCathe();
					base.GlobalData.LastState.StageData.PreLoadCS(".\\CS\\St02");
					break;
				case "St3":
					base.BGM_Player.PreLoad(".\\BGM\\Stage03.wav");
					base.BGM_Player.PreLoad(".\\BGM\\Boss03.wav");
					base.GlobalData.LastState.StageData.ClearCSCathe();
					base.GlobalData.LastState.StageData.PreLoadCS(".\\CS\\St03");
					break;
				case "St4":
					base.BGM_Player.PreLoad(".\\BGM\\Stage04.wav");
					base.BGM_Player.PreLoad(".\\BGM\\Boss04.wav");
					base.GlobalData.LastState.StageData.ClearCSCathe();
					base.GlobalData.LastState.StageData.PreLoadCS(".\\CS\\St04");
					break;
				case "St5":
					base.BGM_Player.PreLoad(".\\BGM\\Stage05.wav");
					base.BGM_Player.PreLoad(".\\BGM\\Boss05.wav");
					base.GlobalData.LastState.StageData.ClearCSCathe();
					base.GlobalData.LastState.StageData.PreLoadCS(".\\CS\\St05");
					break;
				case "St6":
					base.BGM_Player.PreLoad(".\\BGM\\Stage06.wav");
					base.BGM_Player.PreLoad(".\\BGM\\Boss06.wav");
					base.GlobalData.LastState.StageData.ClearCSCathe();
					base.GlobalData.LastState.StageData.PreLoadCS(".\\CS\\St06");
					break;
				case "StEx":
					base.BGM_Player.PreLoad(".\\BGM\\StageEx.wav");
					base.BGM_Player.PreLoad(".\\BGM\\BossEx.wav");
					base.GlobalData.LastState.StageData.ClearCSCathe();
					base.GlobalData.LastState.StageData.PreLoadCS(".\\CS\\StEx");
					break;
				}
			}
			this.LoadFlag = true;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x00041EB8 File Offset: 0x000400B8
		public override void Render()
		{
			base.GlobalData.LastState.Render();
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.DeviceMain.SetRenderState(RenderState.SourceBlend, 5);
			base.DeviceMain.SetRenderState(RenderState.DestinationBlend, 2);
			base.EffectList.ForEach(delegate(BaseEffect x)
			{
				x.Show();
			});
			base.SpriteMain.End();
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.SpriteMain.Draw2D(base.GlobalData.TextureObjectDictionary["NowLoading-0"], 1f, 0f, new Point(520, 440), base.Background.Light);
			base.StageData.EffectList.ForEach(delegate(BaseEffect x)
			{
				if (!x.Active)
				{
					x.Show();
				}
			});
			base.SpriteMain.End();
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x00041FCE File Offset: 0x000401CE
		public override void BGM_Pause()
		{
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00041FD1 File Offset: 0x000401D1
		public override void BGM_Resume()
		{
		}

		// Token: 0x0400027E RID: 638
		private bool LoadFlag;

		// Token: 0x0400027F RID: 639
		private Thread LoadingThread;
	}
}
