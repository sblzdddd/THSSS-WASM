using System;
using System.Drawing;

namespace Shooting
{
	internal class EmitterSaveEnegy3 : BaseEffect
	{
		public EmitterSaveEnegy3(StageDataPackage StageData, PointF Position)
			: base(StageData)
		{
			this.Position = Position;
			Velocity = 0f;
			Direction = 0.0;
			LifeTime = 90;
		}

		public override void Shoot()
		{
			if (Time % 5 == 0)
			{
				for (int i = 0; i < 5; i++)
				{
					double num = (double)((float)Ran.Next(360) / 180f) * 3.141592653589793;
					int num2 = Ran.Next(10, 100);
					PointF pointF = new PointF((float)((double)Position.X + (double)num2 * Math.Cos(num)), (float)((double)Position.Y + (double)num2 * Math.Sin(num)));
					ParticleLongerByV particleLongerByV = new ParticleLongerByV(StageData, "590-1", pointF, 0f, num + 3.141592653589793)
					{
						TransparentValueF = 200f,
						LifeTime = 20,
						Scale = (float)Ran.Next(20, 50) / 100f
					};
					particleLongerByV.Velocity = (float)num2 / (float)particleLongerByV.LifeTime;
				}
			}
		}

		public override void Show()
		{
		}
	}
}
