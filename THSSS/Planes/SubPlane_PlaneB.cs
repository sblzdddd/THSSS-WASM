using System.Drawing;

namespace Shooting
{
	public class SubPlane_PlaneB : BaseSubPlane
	{
		public SubPlane_PlaneB(StageDataPackage StageData, PointF Position)
			: base(StageData, "MySubPlane", Position)
		{
			ShootDirection = -1.5707963267948966;
		}

		public override void Shoot()
		{
			var baseMyBullet_Touhou = new BaseMyBullet_Touhou(StageData, "子机子弹", OriginalPosition, 4f, ShootDirection);
			baseMyBullet_Touhou.Angle = 3.1415927410125732;
			baseMyBullet_Touhou.Damage = 5;
			baseMyBullet_Touhou.Accelerate = 0.5f;
		}
	}
}
