using System;
using System.Drawing;
using SlimDX;

namespace Shooting
{
	// Token: 0x020000E1 RID: 225
	internal class BackgroundPetal3D : BaseParticle3D
	{
		// Token: 0x06000625 RID: 1573 RVA: 0x00036B0D File Offset: 0x00034D0D
		public BackgroundPetal3D(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.Background3D.BackgroundParticleList.Add(this);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00036B32 File Offset: 0x00034D32
		public override void Ctrl()
		{
			base.Time++;
			this.Shoot();
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00036B4C File Offset: 0x00034D4C
		public override void Shoot()
		{
			if (base.Time % 5 == 0)
			{
				for (int i = 0; i < 3; i++)
				{
					BackgroundParticle3D1 backgroundParticle3D = new BackgroundParticle3D1(base.StageData, "Petal3" + base.Ran.Next(4).ToString(), new PointF((float)base.StageData.Ran.Next(280, base.BoundRect.Width + 160), (float)base.StageData.Ran.Next(-20, 0)), (float)base.StageData.Ran.Next(15, 25) / 10f, 1.5707963267948966 + (double)((float)base.StageData.Ran.Next(1, 8) / 10f), 200);
					backgroundParticle3D.Angle = (double)((float)base.StageData.Ran.Next(0, 360) * 3.1415927f / 180f);
					backgroundParticle3D.AngularVelocity3D = (float)base.StageData.Ran.Next(10) / 100f;
					backgroundParticle3D.Scale = (float)base.StageData.Ran.Next(35, 50) / 100f;
					backgroundParticle3D.RotatingAxis = new Vector3((float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100));
					backgroundParticle3D.MaxTransparent = 200;
					backgroundParticle3D.Depth = (float)base.Ran.Next(0, 30);
				}
			}
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00036D17 File Offset: 0x00034F17
		public override void Show()
		{
		}
	}
}
