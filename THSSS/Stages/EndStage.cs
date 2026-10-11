using System;

namespace Shooting
{
	// Token: 0x020001CC RID: 460
	internal class EndStage : BaseEffect
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x00066890 File Offset: 0x00064A90
		// (set) Token: 0x06000A95 RID: 2709 RVA: 0x000668A7 File Offset: 0x00064AA7
		public int FadeOutTime { get; set; }

		// Token: 0x06000A96 RID: 2710 RVA: 0x000668B0 File Offset: 0x00064AB0
		public EndStage(StageDataPackage StageData, string NextState, bool NextInit)
			: this(StageData, NextState, NextInit, 50)
		{
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x000668C0 File Offset: 0x00064AC0
		public EndStage(StageDataPackage StageData, string NextState, bool NextInit, int FadeOutTime)
			: base(StageData)
		{
			this.tempVolumn = base.BGM_Player.Volume;
			base.LifeTime = 160;
			this.nextState = NextState;
			this.nextInit = NextInit;
			this.FadeOutTime = FadeOutTime;
			base.MyPlane.SpellEnabled = false;
			if (base.MyPlane.EnchantmentTime > 50)
			{
				base.MyPlane.EnchantmentTime = 50;
			}
			new BackgroundTransitionOut(StageData, FadeOutTime);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00066958 File Offset: 0x00064B58
		public override void Ctrl()
		{
			base.Ctrl();
			int num = base.LifeTime - 100;
			base.BGM_Player.Volume = (int)((float)this.tempVolumn * (1f - (float)base.Time / (float)num));
			if (base.Time == num)
			{
				base.StageData.ChangeBGM(base.BGM_Player.FileName, false);
				base.BGM_Player.Stop();
			}
			if (base.Time == base.LifeTime)
			{
				base.BGM_Player.Volume = this.tempVolumn;
				if (this.nextInit)
				{
					if (base.StageData.OnReplay && this.nextState == "MainMenu")
					{
						base.StageData.Rep.CloseRpy();
						base.StageData.OnReplay = false;
					}
					base.StageData.StateSwitchData = new StateSwitchDataPackage
					{
						NextState = this.nextState,
						NeedInit = true,
						SDPswitch = new StageDataPackage(base.StageData.GlobalData)
					};
				}
				else if (base.StageData.OnPractice)
				{
					base.StageData.StateSwitchData = new StateSwitchDataPackage
					{
						NextState = "GameSetMenu",
						NeedInit = true,
						SDPswitch = new StageDataPackage(base.StageData.GlobalData)
					};
				}
				else
				{
					base.StageData.StateSwitchData = new StateSwitchDataPackage
					{
						NextState = this.nextState,
						NeedInit = false,
						SDPswitch = base.StageData
					};
					base.MyPlane.Refresh();
					base.StageData.SetReplayStageInfo();
				}
				base.Background3D.Clear();
				base.Background.Clear();
				base.Background2.Clear();
				BackgroundTransitionOut backgroundTransitionOut = new BackgroundTransitionOut(base.StageData, 2);
				backgroundTransitionOut.TransparentValueF = 255f;
				backgroundTransitionOut.TransparentVelocity = 0f;
				backgroundTransitionOut.LifeTime = 1;
				base.BulletList.Clear();
				base.EnemyPlaneList.Clear();
				base.ItemList.Clear();
				base.TimeMain = 0;
				base.MyPlane.Refresh();
				base.Ran = new MyRandom(0);
				base.StageData.VibrateStop();
			}
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00066BE6 File Offset: 0x00064DE6
		public override void Show()
		{
		}

		// Token: 0x040003EA RID: 1002
		private int tempVolumn;

		// Token: 0x040003EB RID: 1003
		private string nextState = null;

		// Token: 0x040003EC RID: 1004
		private bool nextInit = false;
	}
}
