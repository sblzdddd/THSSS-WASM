namespace Shooting
{
	internal class GameState_Boss05 : GameState_SSS05, IGameState
	{
		public GameState_Boss05(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			base.StageName = "Bs5";
			this.testStartTime = 8600;
		}

		public override void Drama()
		{
			base.Drama();
			base.MyPlane.EnchantmentCount = base.MyPlane.EnchantmentCountNeeded;
		}
	}
}
