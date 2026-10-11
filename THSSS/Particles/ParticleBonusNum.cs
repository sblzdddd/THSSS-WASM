using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000193 RID: 403
	internal class ParticleBonusNum : BaseEffect
	{
		// Token: 0x060009A1 RID: 2465 RVA: 0x0005BF54 File Offset: 0x0005A154
		public ParticleBonusNum(StageDataPackage StageData, string textureName, PointF OriginalPosition)
			: base(StageData, textureName, OriginalPosition, 0f, -1.5707963705062866)
		{
			base.OriginalPosition = OriginalPosition;
			base.LifeTime = 100;
			base.Active = false;
			base.Angle = -base.Direction + 1.5707963267948966;
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0005BFAC File Offset: 0x0005A1AC
		public override void Ctrl()
		{
			base.Angle = -base.Direction + 1.5707963267948966;
			if (base.Time < 24)
			{
				base.Velocity = (float)Math.Cos((double)base.Time * 3.141592653589793 * 2.0 / 32.0);
			}
			else
			{
				base.Velocity = 0f;
			}
			base.Ctrl();
			if (base.Time > base.LifeTime - 20)
			{
				base.TransparentValueF -= 12f;
			}
		}
	}
}
