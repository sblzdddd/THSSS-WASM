using System;
using System.Drawing;
using SlimDX;

namespace Shooting
{
	internal class EmitterSaveEnegy3D : BaseEffect
	{
		public EmitterSaveEnegy3D(StageDataPackage StageData, PointF OriginalPosition, Color color)
			: base(StageData)
		{
			this.OriginalPosition = OriginalPosition;
			Velocity = 0f;
			Direction = 0.0;
			LifeTime = 25;
			ColorValue = color;
			var baseEffect = new BaseEffect(StageData, "Effect-2", Position, 0f, 0.0);
			baseEffect.LifeTime = 50;
			baseEffect.Scale = 14f;
			baseEffect.ScaleVelocity = -0.25f;
			baseEffect.ColorValue = color;
			baseEffect.TransparentValueF = 0f;
			baseEffect.TransparentVelocity = 6f;
			baseEffect.MaxTransparent = 128;
		}

		public override void Shoot()
		{
			if (Time % 2 == 0)
			{
				for (var i = 0; i < 2; i++)
				{
					var num = (double)((float)Ran.Next(360) / 180f) * 3.141592653589793;
					var num2 = Ran.Next(150, 200);
					var pointF = new PointF((float)((double)OriginalPosition.X + (double)num2 * Math.Cos(num)), (float)((double)OriginalPosition.Y + (double)num2 * Math.Sin(num)));
					var particle3D = new Particle3D1(StageData, "Effect-1", pointF, 0f, num + 3.141592653589793, Ran.Next(50, 80))
					{
						Angle = (double)((float)StageData.Ran.Next(0, 360) * 3.1415927f / 180f),
						AngularVelocity3D = (float)StageData.Ran.Next(10, 18) / 100f,
						Scale = (float)StageData.Ran.Next(30, 40) / 10f,
						RotatingAxis = new Vector3((float)StageData.Ran.Next(-100, 100), (float)StageData.Ran.Next(-100, 100), (float)StageData.Ran.Next(-100, 100)),
						ColorValue = ColorValue
					};
					particle3D.Velocity = 2f * (float)num2 / (float)particle3D.LifeTime;
					particle3D.Accelerate = -particle3D.Velocity * particle3D.Velocity / (float)(2 * num2);
					particle3D.LifeTime -= 15;
				}
			}
		}

		public override void Show()
		{
		}
	}
}
