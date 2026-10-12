namespace Shooting
{
	internal class GameState_Boss01 : GameState_SSS01, IGameState
	{
		public GameState_Boss01(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			base.StageName = "Bs1";
			this.testStartTime = 6290;
		}

		public override void Drama()
		{
			base.Drama();
			base.MyPlane.EnchantmentCount = base.MyPlane.EnchantmentCountNeeded;
		}
	}
}
