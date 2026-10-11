using System;
using System.Drawing;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x020001A5 RID: 421
	internal class GameState_SplashScreen2 : GameState_SplashScreen, IGameState
	{
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0005EFD8 File Offset: 0x0005D1D8
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x0005EFEF File Offset: 0x0005D1EF
		public string PreNextState { get; set; }

		// Token: 0x06000A03 RID: 2563 RVA: 0x0005EFF8 File Offset: 0x0005D1F8
		public GameState_SplashScreen2(GlobalDataPackage GlobalData)
			: this(GlobalData, "MainMenu")
		{
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0005F009 File Offset: 0x0005D209
		public GameState_SplashScreen2(GlobalDataPackage GlobalData, string nextState)
		{
			base.GlobalData = GlobalData;
			base.TxtureObject = GlobalData.TextureObjectDictionary["这只是测试版"];
			this.PreNextState = nextState;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0005F03C File Offset: 0x0005D23C
		public override void UpdateData()
		{
			base.TimeMain++;
			if (base.TimeMain < 70)
			{
				base.Background.Light += 4;
			}
			else if (base.TimeMain > 120)
			{
				base.Background.Light -= 4;
			}
			if (base.Background.Light == 0)
			{
				base.StateSwitchData = new StateSwitchDataPackage
				{
					NextState = this.PreNextState,
					NeedInit = true,
					SDPswitch = new StageDataPackage(base.GlobalData)
				};
				base.GlobalData.LastState = this;
			}
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0005F108 File Offset: 0x0005D308
		public override void Render()
		{
			base.SpriteMain.Begin(SpriteFlags.AlphaBlend);
			base.Background.Show(false);
			base.SpriteMain.Draw2D(base.TxtureObject, 1f, 0f, new Point(320, 240), base.Background.Light);
			base.StageData.EffectList.ForEach(delegate(BaseEffect x)
			{
				x.Show();
			});
			base.SpriteMain.End();
		}
	}
}
