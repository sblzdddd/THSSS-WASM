namespace Shooting
{
	internal class SpellDisable : BaseEffect
	{
		public SpellDisable(StageDataPackage StageData)
			: base(StageData)
		{
			spellenable = MyPlane.SpellEnabled;
			LifeTime = 30;
			MyPlane.SpellEnabled = false;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time == LifeTime)
			{
				MyPlane.SpellEnabled = spellenable;
			}
		}

		private bool spellenable;
	}
}
