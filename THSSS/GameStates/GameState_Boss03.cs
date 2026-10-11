using System;

namespace Shooting
{
	internal class GameState_Boss03 : GameState_SSS03, IGameState
	{
		public GameState_Boss03(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			base.StageName = "Bs3";
			this.testStartTime = 7150;
		}

		public override void Drama()
		{
			base.Drama();
			base.MyPlane.EnchantmentCount = base.MyPlane.EnchantmentCountNeeded;
		}
	}
}
