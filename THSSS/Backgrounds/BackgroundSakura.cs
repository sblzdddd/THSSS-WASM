using System;
using System.Drawing;

namespace Shooting
{
	internal class BackgroundSakura : BaseParticle3D
	{
		public BackgroundSakura(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.Background3D.BackgroundParticleList.Add(this);
		}

		public override void Ctrl()
		{
			base.Time++;
			this.Shoot();
		}

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

		public override void Show()
		{
		}
	}
}
