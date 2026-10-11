using System;

namespace Shooting
{
	// Token: 0x020000CC RID: 204
	internal class GameState_Boss03 : GameState_SSS03, IGameState
	{
		// Token: 0x060005B3 RID: 1459 RVA: 0x00031FCE File Offset: 0x000301CE
		public GameState_Boss03(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			base.StageName = "Bs3";
			this.testStartTime = 7150;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00031FF1 File Offset: 0x000301F1
		public override void Drama()
		{
			base.Drama();
			base.MyPlane.EnchantmentCount = base.MyPlane.EnchantmentCountNeeded;
		}
	}
}
