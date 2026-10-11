using System;

namespace Shooting
{
	// Token: 0x0200009A RID: 154
	internal class GameState_Boss05 : GameState_SSS05, IGameState
	{
		// Token: 0x060004B9 RID: 1209 RVA: 0x0002482E File Offset: 0x00022A2E
		public GameState_Boss05(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			base.StageName = "Bs5";
			this.testStartTime = 8600;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x00024851 File Offset: 0x00022A51
		public override void Drama()
		{
			base.Drama();
			base.MyPlane.EnchantmentCount = base.MyPlane.EnchantmentCountNeeded;
		}
	}
}
