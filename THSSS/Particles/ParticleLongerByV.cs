using System;
using System.Drawing;

namespace Shooting
{
	internal class ParticleLongerByV : BaseEffect
	{
		public ParticleLongerByV(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData, textureName, Position, Velocity, Direction)
		{
			base.LifeTime = 30;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time > 0)
			{
				base.TransparentValueF -= (float)(255 / base.LifeTime);
				base.ScaleLength = base.ScaleWidth + base.ScaleWidth * Math.Abs(base.Velocity) / 10f;
			}
		}
	}
}
