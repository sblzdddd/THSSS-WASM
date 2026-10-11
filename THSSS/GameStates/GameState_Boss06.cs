using System;

namespace Shooting
{
	internal class GameState_Boss06 : GameState_SSS06, IGameState
	{
		public GameState_Boss06(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			base.StageName = "Bs6";
			this.testStartTime = 8400;
		}

		public override void Drama()
		{
			base.Drama();
			base.MyPlane.EnchantmentCount = base.MyPlane.EnchantmentCountNeeded;
		}
	}
}
