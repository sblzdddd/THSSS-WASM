using System;

namespace Shooting
{
	// Token: 0x02000083 RID: 131
	internal class GameState_Boss06 : GameState_SSS06, IGameState
	{
		// Token: 0x0600045E RID: 1118 RVA: 0x0001F8EF File Offset: 0x0001DAEF
		public GameState_Boss06(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			base.StageName = "Bs6";
			this.testStartTime = 8400;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001F912 File Offset: 0x0001DB12
		public override void Drama()
		{
			base.Drama();
			base.MyPlane.EnchantmentCount = base.MyPlane.EnchantmentCountNeeded;
		}
	}
}
