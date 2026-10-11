using System;

namespace Shooting
{
	internal class GameState_Boss02 : GameState_SSS02, IGameState
	{
		public GameState_Boss02(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			base.StageName = "Bs2";
			this.testStartTime = 7740;
		}

		public override void Drama()
		{
			base.Drama();
			base.MyPlane.EnchantmentCount = base.MyPlane.EnchantmentCountNeeded;
		}
	}
}
