using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001B7 RID: 439
	internal class BackgroundSakura : BaseParticle3D
	{
		// Token: 0x06000A4A RID: 2634 RVA: 0x00062C52 File Offset: 0x00060E52
		public BackgroundSakura(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.Background3D.BackgroundParticleList.Add(this);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00062C77 File Offset: 0x00060E77
		public override void Ctrl()
		{
			base.Time++;
			this.Shoot();
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x00062C90 File Offset: 0x00060E90
		public override void Shoot()
		{
			int num = 25;
			float num2 = 2.7f;
			if (base.Boss != null)
			{
				if (base.Boss.Life >= 2)
				{
					num = 50;
					num2 = 1.35f;
				}
			}
			if (base.Time % num == 0)
			{
				int num3 = base.Time / 2;
				if (num3 > 110)
				{
					num3 = 110;
				}
				BackgroundParticle3D1 backgroundParticle3D = new BackgroundParticle3D1(base.StageData, "Sakura", new PointF((float)(num3 + base.Ran.Next(30)), -80f), num2, 1.720796332755361, 350);
				backgroundParticle3D.Angle = (double)((float)base.StageData.Ran.Next(-10, 10) * 3.1415927f / 180f);
				backgroundParticle3D.Active = true;
				backgroundParticle3D.MaxTransparent = 60;
				backgroundParticle3D.Direction3D = 0.75;
				backgroundParticle3D.Depth = -100f;
				backgroundParticle3D.MaxDepth = 1000f;
				backgroundParticle3D.OutsideRegion = 128;
			}
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00062DB6 File Offset: 0x00060FB6
		public override void Show()
		{
		}
	}
}
