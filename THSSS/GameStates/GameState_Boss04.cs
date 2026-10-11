using System;

namespace Shooting
{
	// Token: 0x020000AC RID: 172
	internal class GameState_Boss04 : GameState_SSS04, IGameState
	{
		// Token: 0x0600050F RID: 1295 RVA: 0x00028A95 File Offset: 0x00026C95
		public GameState_Boss04(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			base.StageName = "Bs4";
			this.testStartTime = 8700;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00028AB8 File Offset: 0x00026CB8
		public override void Drama()
		{
			base.Drama();
			base.MyPlane.EnchantmentCount = base.MyPlane.EnchantmentCountNeeded;
		}
	}
}
