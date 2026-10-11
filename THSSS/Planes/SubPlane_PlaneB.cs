using System;
using System.Drawing;

namespace Shooting
{
	public class SubPlane_PlaneB : BaseSubPlane
	{
		public SubPlane_PlaneB(StageDataPackage StageData, PointF Position)
			: base(StageData, "MySubPlane", Position)
		{
			base.ShootDirection = -1.5707963267948966;
		}

		public override void Shoot()
		{
			BaseMyBullet_Touhou baseMyBullet_Touhou = new BaseMyBullet_Touhou(base.StageData, "子机子弹", base.OriginalPosition, 4f, base.ShootDirection);
			baseMyBullet_Touhou.Angle = 3.1415927410125732;
			baseMyBullet_Touhou.Damage = 5;
			baseMyBullet_Touhou.Accelerate = 0.5f;
		}
	}
}
