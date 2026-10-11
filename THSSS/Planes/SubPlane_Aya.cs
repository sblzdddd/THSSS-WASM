using System;
using System.Drawing;

namespace Shooting
{
	public class SubPlane_Aya : BaseSubPlane
	{
		public SubPlane_Aya(StageDataPackage StageData, PointF Position)
			: base(StageData, "Center", Position)
		{
		}

		public override void Refresh()
		{
			base.Refresh();
			this.shootFlag = 0;
		}

		public override void Shoot()
		{
			BaseMyBullet_Touhou baseMyBullet_Touhou = new BaseMyBullet_Touhou(base.StageData, "ReimuAyaBullet01", base.OriginalPosition, 8f, base.ShootDirection);
			baseMyBullet_Touhou.TransparentValueF = 150f;
			baseMyBullet_Touhou.Angle = 1.5707963705062866;
			baseMyBullet_Touhou.Damage = 6;
			if (this.shootFlag++ % 2 == 0)
			{
				BaseMyBullet_Touhou baseMyBullet_Touhou2 = new BaseMyBullet_Touhou(base.StageData, "ReimuAyaBullet01", base.OriginalPosition, 8f, base.ShootDirection + (double)((float)base.Ran.Next(-3, 4) / 20f));
				baseMyBullet_Touhou2.TransparentValueF = 150f;
				baseMyBullet_Touhou2.Angle = 1.5707963705062866;
				baseMyBullet_Touhou2.Damage = 6;
			}
		}

		private int shootFlag = 0;
	}
}
