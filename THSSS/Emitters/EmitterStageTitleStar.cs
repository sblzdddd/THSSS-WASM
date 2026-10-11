using System;
using System.Drawing;

namespace Shooting
{
	internal class EmitterStageTitleStar : BaseEffect
	{
		public EmitterStageTitleStar(StageDataPackage StageData)
			: base(StageData, null, new PointF(0f, 0f), 0f, 0.0)
		{
			base.OriginalPosition = new PointF(0f, (float)(base.BoundRect.Height / 2 - 50));
			base.Velocity = 4f;
			base.MaxVelocity = 8f;
			base.AccelerateCS = 0.08f;
			base.AccDirection = 0.0;
		}

		public override void Shoot()
		{
			if (base.Time % 1 == 0)
			{
				for (int i = 0; i < 4; i++)
				{
					float num = (float)base.Ran.Next(0, 15);
					double num2 = (double)base.Ran.Next(360) * 3.141592653589793 / 180.0;
					PointF pointF = new PointF(base.OriginalPosition.X + num * (float)Math.Sin(num2), base.OriginalPosition.Y + num * (float)Math.Cos(num2));
					Particle particle = new Particle(base.StageData, "Star", pointF, (float)base.StageData.Ran.Next(1, 110) / 100f, (double)base.Ran.Next(360) * 3.141592653589793 / 180.0);
					particle.Active = true;
					particle.Scale = (float)base.StageData.Ran.Next(15, 40) / 100f;
					particle.LifeTime = 80;
				}
			}
		}
	}
}
