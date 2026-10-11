using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000089 RID: 137
	public class SubPlane_Aya : BaseSubPlane
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x00020926 File Offset: 0x0001EB26
		public SubPlane_Aya(StageDataPackage StageData, PointF Position)
			: base(StageData, "Center", Position)
		{
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0002093F File Offset: 0x0001EB3F
		public override void Refresh()
		{
			base.Refresh();
			this.shootFlag = 0;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00020950 File Offset: 0x0001EB50
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

		// Token: 0x0400014F RID: 335
		private int shootFlag = 0;
	}
}
