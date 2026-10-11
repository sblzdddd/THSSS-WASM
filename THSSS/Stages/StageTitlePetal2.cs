using System;
using System.Drawing;
using SlimDX;

namespace Shooting
{
	// Token: 0x020001BB RID: 443
	public class StageTitlePetal2 : BaseParticle3D
	{
		// Token: 0x06000A58 RID: 2648 RVA: 0x00063414 File Offset: 0x00061614
		public StageTitlePetal2(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.Background3D.BackgroundParticleList.Add(this);
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2 - 45), 40f);
			base.Direction = 1.5707963267948966;
			base.Velocity = 3f;
			base.LifeTime = 80;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00063490 File Offset: 0x00061690
		public override void Ctrl()
		{
			base.Ctrl();
			this.Shoot();
			float num = 5f;
			base.Vx = 10f * (float)Math.Sin((double)((float)base.Time / num));
			base.Depth += 10f * (float)Math.Cos((double)((float)base.Time / num));
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x000634F4 File Offset: 0x000616F4
		public override void Shoot()
		{
			if (base.Time % 1 == 0)
			{
				for (int i = 0; i < 8; i++)
				{
					Particle3D4 particle3D = new Particle3D4(base.StageData, "Petal00", new PointF((float)base.StageData.Ran.Next((int)base.OriginalPosition.X - 60, (int)base.OriginalPosition.X - 35), (float)((int)base.OriginalPosition.Y)), (float)base.StageData.Ran.Next(10, 30) / 10f, 1.5707963267948966, 100 + base.Ran.Next(40));
					particle3D.Angle = (double)((float)base.StageData.Ran.Next(0, 360) * 3.1415927f / 180f);
					particle3D.AngularVelocity3D = (float)base.StageData.Ran.Next(10) / 100f;
					particle3D.Scale = (float)base.StageData.Ran.Next(5, 8) / 10f;
					particle3D.RotatingAxis = new Vector3((float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100));
					particle3D.MaxTransparent = 255;
					particle3D.Depth = (float)base.Ran.Next((int)base.Depth - 50, (int)base.Depth - 50);
				}
			}
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x000636AB File Offset: 0x000618AB
		public override void Show()
		{
		}
	}
}
