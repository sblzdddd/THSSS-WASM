using System;

namespace Shooting
{
	// Token: 0x02000107 RID: 263
	internal class GameState_Boss01 : GameState_SSS01, IGameState
	{
		// Token: 0x0600073D RID: 1853 RVA: 0x000418AF File Offset: 0x0003FAAF
		public GameState_Boss01(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			base.StageName = "Bs1";
			this.testStartTime = 6290;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x000418D2 File Offset: 0x0003FAD2
		public override void Drama()
		{
			base.Drama();
			base.MyPlane.EnchantmentCount = base.MyPlane.EnchantmentCountNeeded;
		}
	}
}
