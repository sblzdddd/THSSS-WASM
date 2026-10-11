using System;

namespace Shooting
{
	// Token: 0x02000154 RID: 340
	internal class SpellDisable : BaseEffect
	{
		// Token: 0x060008B8 RID: 2232 RVA: 0x00050E72 File Offset: 0x0004F072
		public SpellDisable(StageDataPackage StageData)
			: base(StageData)
		{
			this.spellenable = base.MyPlane.SpellEnabled;
			base.LifeTime = 30;
			base.MyPlane.SpellEnabled = false;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00050EA8 File Offset: 0x0004F0A8
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == base.LifeTime)
			{
				base.MyPlane.SpellEnabled = this.spellenable;
			}
		}

		// Token: 0x04000321 RID: 801
		private bool spellenable;
	}
}
