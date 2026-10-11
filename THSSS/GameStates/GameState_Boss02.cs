using System;

namespace Shooting
{
	// Token: 0x020000E7 RID: 231
	internal class GameState_Boss02 : GameState_SSS02, IGameState
	{
		// Token: 0x0600064C RID: 1612 RVA: 0x0003769C File Offset: 0x0003589C
		public GameState_Boss02(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			base.StageName = "Bs2";
			this.testStartTime = 7740;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x000376BF File Offset: 0x000358BF
		public override void Drama()
		{
			base.Drama();
			base.MyPlane.EnchantmentCount = base.MyPlane.EnchantmentCountNeeded;
		}
	}
}
