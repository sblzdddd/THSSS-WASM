using System;
using System.Drawing;
using SlimDX;

namespace Shooting
{
	// Token: 0x020000A9 RID: 169
	internal class BackgroundLeaf3D : BaseParticle3D
	{
		// Token: 0x060004F5 RID: 1269 RVA: 0x00027F42 File Offset: 0x00026142
		public BackgroundLeaf3D(StageDataPackage StageData)
		{
			base.StageData = StageData;
			StageData.Particle3D.ParticleList.Add(this);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00027F67 File Offset: 0x00026167
		public override void Ctrl()
		{
			base.Time++;
			this.Shoot();
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00027F80 File Offset: 0x00026180
		public override void Shoot()
		{
			if (base.Time % 20 == 0)
			{
				for (int i = 0; i < 2; i++)
				{
					BackgroundParticle3D1 backgroundParticle3D = new BackgroundParticle3D1(base.StageData, "Leaf", new PointF((float)base.StageData.Ran.Next(130, 700), (float)base.StageData.Ran.Next(-50, -20)), (float)base.StageData.Ran.Next(2, 4), 1.5707963267948966 + (double)((float)base.StageData.Ran.Next(1, 8) / 10f), 200);
					backgroundParticle3D.Angle = (double)((float)base.StageData.Ran.Next(0, 360) * 3.1415927f / 180f);
					backgroundParticle3D.AngularVelocity3D = (float)base.StageData.Ran.Next(5) / 100f;
					backgroundParticle3D.Scale = (float)base.StageData.Ran.Next(5, 20) / 10f;
					backgroundParticle3D.RotatingAxis = new Vector3((float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100));
					backgroundParticle3D.ColorValue = Color.FromArgb(base.StageData.Ran.Next(150, 250), 255, base.StageData.Ran.Next(150, 255));
				}
			}
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0002813D File Offset: 0x0002633D
		public override void Show()
		{
		}
	}
}
