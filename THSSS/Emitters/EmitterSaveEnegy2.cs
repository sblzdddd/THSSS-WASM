using System;
using System.Drawing;

namespace Shooting
{
	internal class EmitterSaveEnegy2 : BaseEffect
	{
		public EmitterSaveEnegy2(StageDataPackage StageData, PointF Position)
			: base(StageData)
		{
			this.Position = Position;
			Velocity = 0f;
			Direction = 0.0;
			LifeTime = 20;
		}

		public override void Shoot()
		{
			if (Time % 3 == 0)
			{
				for (int i = 0; i < 5; i++)
				{
					double num = (double)((float)Ran.Next(360) / 180f) * 3.141592653589793;
					int num2 = Ran.Next(150, 200);
					PointF pointF = new PointF((float)((double)Position.X + (double)num2 * Math.Cos(num)), (float)((double)Position.Y + (double)num2 * Math.Sin(num)));
					ParticleBack particleBack = new ParticleBack(StageData, "snow", pointF, 0f, num + 3.141592653589793)
					{
						AngularVelocity = (float)Ran.Next(10) / 100f,
						TransparentValueF = 200f,
						LifeTime = Ran.Next(30, 50),
						Scale = (float)Ran.Next(80, 100) / 100f
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
