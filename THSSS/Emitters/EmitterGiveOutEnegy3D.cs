using System;
using System.Drawing;
using SlimDX;

namespace Shooting
{
	// Token: 0x020000BE RID: 190
	internal class EmitterGiveOutEnegy3D : BaseEffect
	{
		// Token: 0x06000558 RID: 1368 RVA: 0x0002E560 File Offset: 0x0002C760
		public EmitterGiveOutEnegy3D(StageDataPackage StageData, PointF OriginalPosition, Color color)
			: base(StageData)
		{
			base.OriginalPosition = OriginalPosition;
			base.Velocity = 0f;
			base.Direction = 0.0;
			base.LifeTime = 25;
			base.ColorValue = color;
			BaseEffect baseEffect = new BaseEffect(StageData, "Effect-2", base.Position, 0f, 0.0);
			baseEffect.LifeTime = 100;
			baseEffect.Scale = 1f;
			baseEffect.ScaleVelocity = 0.8f;
			baseEffect.ColorValue = color;
			baseEffect.TransparentValueF = 180f;
			baseEffect.TransparentVelocity = -2f;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0002E610 File Offset: 0x0002C810
		public override void Shoot()
		{
			if (base.Time == 1)
			{
				for (int i = 0; i < 50; i++)
				{
					Particle3D1 particle3D = new Particle3D1(base.StageData, "Effect-1", base.OriginalPosition, (float)base.Ran.Next(50, 130) / 10f, (double)((float)base.Ran.Next(360) / 180f * 3.1415927f), 30 + base.StageData.Ran.Next(50));
					particle3D.Direction3D = (double)((float)base.StageData.Ran.Next(0, 60) * 3.1415927f / 180f);
					particle3D.Angle = (double)((float)base.StageData.Ran.Next(0, 360) * 3.1415927f / 180f);
					particle3D.AngularVelocity3D = (float)base.StageData.Ran.Next(10, 18) / 100f;
					particle3D.Scale = (float)base.StageData.Ran.Next(20, 30) / 10f;
					particle3D.RotatingAxis = new Vector3((float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100));
					particle3D.ColorValue = base.ColorValue;
				}
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0002E7A0 File Offset: 0x0002C9A0
		public override void Show()
		{
		}
	}
}
