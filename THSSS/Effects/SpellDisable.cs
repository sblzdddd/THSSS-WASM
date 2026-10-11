using System;

namespace Shooting
{
	internal class SpellDisable : BaseEffect
	{
		public SpellDisable(StageDataPackage StageData)
			: base(StageData)
		{
			this.spellenable = base.MyPlane.SpellEnabled;
			base.LifeTime = 30;
			base.MyPlane.SpellEnabled = false;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == base.LifeTime)
			{
				base.MyPlane.SpellEnabled = this.spellenable;
			}
		}

		private bool spellenable;
	}
}
