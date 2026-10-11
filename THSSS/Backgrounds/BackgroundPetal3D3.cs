using System;
using System.Drawing;
using SlimDX;

namespace Shooting
{
	// Token: 0x02000099 RID: 153
	internal class BackgroundPetal3D3 : BaseParticle3D
	{
		// Token: 0x060004B5 RID: 1205 RVA: 0x000245E4 File Offset: 0x000227E4
		public BackgroundPetal3D3(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.Background3D.BackgroundParticleList.Add(this);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00024609 File Offset: 0x00022809
		public override void Ctrl()
		{
			base.Time++;
			this.Shoot();
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00024624 File Offset: 0x00022824
		public override void Shoot()
		{
			if (base.Time % 4 == 0)
			{
				for (int i = 0; i < 3; i++)
				{
					new BackgroundParticle3D2(base.StageData, "Petal0" + base.Ran.Next(4).ToString(), new PointF((float)base.StageData.Ran.Next(0, base.BoundRect.Width), (float)base.StageData.Ran.Next(-150, (base.Time > 600) ? 100 : 300)), (float)base.StageData.Ran.Next(3, 6), 1.5707963267948966 + (double)((float)base.StageData.Ran.Next(-4, 3) / 10f), 250)
					{
						Angle = (double)((float)base.StageData.Ran.Next(0, 360) * 3.1415927f / 180f),
						AngularVelocity3D = (float)base.StageData.Ran.Next(10) / 100f,
						Scale = (float)base.StageData.Ran.Next(4, 6) / 10f,
						RotatingAxis = new Vector3((float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100)),
						MaxTransparent = 160,
						Depth = (float)base.Ran.Next(-300, 0),
						MaxDepth = 1000f
					}.Direction3D = ((base.Time > 600) ? 0.5235987755982988 : 0.7853981633974483);
				}
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0002482B File Offset: 0x00022A2B
		public override void Show()
		{
		}
	}
}
