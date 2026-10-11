using System;
using System.Drawing;

namespace Shooting
{
	internal class EmitterSaveEnegy2 : BaseEffect
	{
		public EmitterSaveEnegy2(StageDataPackage StageData, PointF Position)
			: base(StageData)
		{
			base.Position = Position;
			base.Velocity = 0f;
			base.Direction = 0.0;
			base.LifeTime = 20;
		}

		public override void Shoot()
		{
			if (base.Time % 3 == 0)
			{
				for (int i = 0; i < 5; i++)
				{
					double num = (double)((float)base.Ran.Next(360) / 180f) * 3.141592653589793;
					int num2 = base.Ran.Next(150, 200);
					PointF pointF = new PointF((float)((double)base.Position.X + (double)num2 * Math.Cos(num)), (float)((double)base.Position.Y + (double)num2 * Math.Sin(num)));
					ParticleBack particleBack = new ParticleBack(base.StageData, "snow", pointF, 0f, num + 3.141592653589793)
					{
						AngularVelocity = (float)base.Ran.Next(10) / 100f,
						TransparentValueF = 200f,
						LifeTime = base.Ran.Next(30, 50),
						Scale = (float)base.Ran.Next(80, 100) / 100f
					};
					particleBack.Velocity = (float)num2 / (float)particleBack.LifeTime;
				}
			}
		}

		public override void Show()
		{
		}
	}
}
