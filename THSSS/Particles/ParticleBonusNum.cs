using System;
using System.Drawing;

namespace Shooting
{
	internal class ParticleBonusNum : BaseEffect
	{
		public ParticleBonusNum(StageDataPackage StageData, string textureName, PointF OriginalPosition)
			: base(StageData, textureName, OriginalPosition, 0f, -1.5707963705062866)
		{
			this.OriginalPosition = OriginalPosition;
			LifeTime = 100;
			Active = false;
			Angle = -Direction + 1.5707963267948966;
		}

		public override void Ctrl()
		{
			Angle = -Direction + 1.5707963267948966;
			if (Time < 24)
			{
				Velocity = (float)Math.Cos((double)Time * 3.141592653589793 * 2.0 / 32.0);
			}
			else
			{
				Velocity = 0f;
			}
			base.Ctrl();
			if (Time > LifeTime - 20)
			{
				TransparentValueF -= 12f;
			}
		}
	}
}
