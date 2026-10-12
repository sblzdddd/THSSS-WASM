using System;
using System.Drawing;

namespace Shooting
{
	public class BossStarEmitter01 : BaseEffect
	{
		public BossStarEmitter01(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			this.OriginalPosition = OriginalPosition;
			LifeTime = 130;
		}

		public override void Shoot()
		{
			if (Time % 1 == 0)
			{
				for (var i = 0; i < 2; i++)
				{
					var num = (float)Ran.Next(0, 15);
					var num2 = (double)Ran.Next(360) * 3.141592653589793 / 180.0;
					var pointF = new PointF(Position.X + num * (float)Math.Sin(num2), Position.Y + num * (float)Math.Cos(num2));
					var particle = new Particle(StageData, "Star0" + ColorType.ToString(), pointF, (float)StageData.Ran.Next(1, 15) / 10f, Direction);
					particle.Scale = (float)StageData.Ran.Next(15, 40) / 100f;
					particle.MaxTransparent = 255;
					particle.LifeTime = 40 + Ran.Next(60);
				}
			}
		}

		public int ColorType;
	}
}
