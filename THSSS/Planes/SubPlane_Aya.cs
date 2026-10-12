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
			shootFlag = 0;
		}

		public override void Shoot()
		{
			var baseMyBullet_Touhou = new BaseMyBullet_Touhou(StageData, "ReimuAyaBullet01", OriginalPosition, 8f, ShootDirection);
			baseMyBullet_Touhou.TransparentValueF = 150f;
			baseMyBullet_Touhou.Angle = 1.5707963705062866;
			baseMyBullet_Touhou.Damage = 6;
			if (shootFlag++ % 2 == 0)
			{
				var baseMyBullet_Touhou2 = new BaseMyBullet_Touhou(StageData, "ReimuAyaBullet01", OriginalPosition, 8f, ShootDirection + (double)((float)Ran.Next(-3, 4) / 20f));
				baseMyBullet_Touhou2.TransparentValueF = 150f;
				baseMyBullet_Touhou2.Angle = 1.5707963705062866;
				baseMyBullet_Touhou2.Damage = 6;
			}
		}

		private int shootFlag = 0;
	}
}
