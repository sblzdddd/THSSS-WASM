using System;
using System.Drawing;

namespace Shooting
{
	public class BossStarEmitter01 : BaseEffect
	{
		public BossStarEmitter01(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			base.OriginalPosition = OriginalPosition;
			base.LifeTime = 130;
		}

		public override void Shoot()
		{
			if (base.Time % 1 == 0)
			{
				for (int i = 0; i < 2; i++)
				{
					float num = (float)base.Ran.Next(0, 15);
					double num2 = (double)base.Ran.Next(360) * 3.141592653589793 / 180.0;
					PointF pointF = new PointF(base.Position.X + num * (float)Math.Sin(num2), base.Position.Y + num * (float)Math.Cos(num2));
					Particle particle = new Particle(base.StageData, "Star0" + this.ColorType.ToString(), pointF, (float)base.StageData.Ran.Next(1, 15) / 10f, base.Direction);
					particle.Scale = (float)base.StageData.Ran.Next(15, 40) / 100f;
					particle.MaxTransparent = 255;
					particle.LifeTime = 40 + base.Ran.Next(60);
				}
			}
		}

		public int ColorType;
	}
}
