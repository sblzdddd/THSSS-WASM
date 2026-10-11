using System;
using System.Drawing;

namespace Shooting
{
	internal class EmitterSaveEnegy3 : BaseEffect
	{
		public EmitterSaveEnegy3(StageDataPackage StageData, PointF Position)
			: base(StageData)
		{
			base.Position = Position;
			base.Velocity = 0f;
			base.Direction = 0.0;
			base.LifeTime = 90;
		}

		public override void Shoot()
		{
			if (base.Time % 5 == 0)
			{
				for (int i = 0; i < 5; i++)
				{
					double num = (double)((float)base.Ran.Next(360) / 180f) * 3.141592653589793;
					int num2 = base.Ran.Next(10, 100);
					PointF pointF = new PointF((float)((double)base.Position.X + (double)num2 * Math.Cos(num)), (float)((double)base.Position.Y + (double)num2 * Math.Sin(num)));
					ParticleLongerByV particleLongerByV = new ParticleLongerByV(base.StageData, "590-1", pointF, 0f, num + 3.141592653589793)
					{
						TransparentValueF = 200f,
						LifeTime = 20,
						Scale = (float)base.Ran.Next(20, 50) / 100f
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
