using System;
using System.Drawing;

namespace Shooting
{
	internal class ParticleBonusNum : BaseEffect
	{
		public ParticleBonusNum(StageDataPackage StageData, string textureName, PointF OriginalPosition)
			: base(StageData, textureName, OriginalPosition, 0f, -1.5707963705062866)
		{
			base.OriginalPosition = OriginalPosition;
			base.LifeTime = 100;
			base.Active = false;
			base.Angle = -base.Direction + 1.5707963267948966;
		}

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
