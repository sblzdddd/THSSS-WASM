using System;
using System.Drawing;
using SlimDX;

namespace Shooting
{
	// Token: 0x020001E9 RID: 489
	internal class EmitterSaveEnegy3D : BaseEffect
	{
		// Token: 0x06000B31 RID: 2865 RVA: 0x0006E7AC File Offset: 0x0006C9AC
		public EmitterSaveEnegy3D(StageDataPackage StageData, PointF OriginalPosition, Color color)
			: base(StageData)
		{
			base.OriginalPosition = OriginalPosition;
			base.Velocity = 0f;
			base.Direction = 0.0;
			base.LifeTime = 25;
			base.ColorValue = color;
			BaseEffect baseEffect = new BaseEffect(StageData, "Effect-2", base.Position, 0f, 0.0);
			baseEffect.LifeTime = 50;
			baseEffect.Scale = 14f;
			baseEffect.ScaleVelocity = -0.25f;
			baseEffect.ColorValue = color;
			baseEffect.TransparentValueF = 0f;
			baseEffect.TransparentVelocity = 6f;
			baseEffect.MaxTransparent = 128;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0006E868 File Offset: 0x0006CA68
		public override void Shoot()
		{
			if (base.Time % 2 == 0)
			{
				for (int i = 0; i < 2; i++)
				{
					double num = (double)((float)base.Ran.Next(360) / 180f) * 3.141592653589793;
					int num2 = base.Ran.Next(150, 200);
					PointF pointF = new PointF((float)((double)base.OriginalPosition.X + (double)num2 * Math.Cos(num)), (float)((double)base.OriginalPosition.Y + (double)num2 * Math.Sin(num)));
					Particle3D1 particle3D = new Particle3D1(base.StageData, "Effect-1", pointF, 0f, num + 3.141592653589793, base.Ran.Next(50, 80))
					{
						Angle = (double)((float)base.StageData.Ran.Next(0, 360) * 3.1415927f / 180f),
						AngularVelocity3D = (float)base.StageData.Ran.Next(10, 18) / 100f,
						Scale = (float)base.StageData.Ran.Next(30, 40) / 10f,
						RotatingAxis = new Vector3((float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100)),
						ColorValue = base.ColorValue
					};
					particle3D.Velocity = 2f * (float)num2 / (float)particle3D.LifeTime;
					particle3D.Accelerate = -particle3D.Velocity * particle3D.Velocity / (float)(2 * num2);
					particle3D.LifeTime -= 15;
				}
			}
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0006EA69 File Offset: 0x0006CC69
		public override void Show()
		{
		}
	}
}
