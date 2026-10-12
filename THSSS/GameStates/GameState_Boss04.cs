namespace Shooting
{
	internal class GameState_Boss04 : GameState_SSS04, IGameState
	{
		public GameState_Boss04(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			base.StageName = "Bs4";
			this.testStartTime = 8700;
		}

		public override void Drama()
		{
			base.Drama();
			base.MyPlane.EnchantmentCount = base.MyPlane.EnchantmentCountNeeded;
		}
	}
}
