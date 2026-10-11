using System;
using System.Drawing;

namespace Shooting
{
	internal class EmitterSaveEnegy : BaseEffect
	{
		public int ColorType { get; set; }

		public EmitterSaveEnegy(StageDataPackage StageData, PointF Position, int colorType)
			: base(StageData)
		{
			base.Position = Position;
			base.Velocity = 0f;
			base.Direction = 0.0;
			base.LifeTime = 200;
			this.ColorType = colorType;
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
					ParticleSmaller particleSmaller = new ParticleSmaller(base.StageData, "MyBullet000" + this.ColorType.ToString(), pointF, 0f, num + 3.141592653589793);
					particleSmaller.TransparentValueF = 200f;
					particleSmaller.LifeTime = 50;
					particleSmaller.Velocity = (float)num2 / (float)particleSmaller.LifeTime;
					particleSmaller.Scale = (float)base.Ran.Next(20, 50) / 100f;
				}
			}
		}

		public override void Show()
		{
		}
	}
}
