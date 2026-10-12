using System.Drawing;

namespace Shooting
{
	public class SubPlane_AyaB : BaseSubPlane
	{
		public SubPlane_AyaB(StageDataPackage StageData, PointF Position)
			: base(StageData, "MarisaSubPlane10", Position)
		{
		}

		public override void Refresh()
		{
			base.Refresh();
		}

		public override void Shoot()
		{
			if (base.KClass.Key_Shift)
			{
				BaseMyBullet_Touhou baseMyBullet_Touhou = new BaseMyBullet_Touhou(base.StageData, "AyaBullet10", base.OriginalPosition, 12f, base.ShootDirection);
				baseMyBullet_Touhou.Angle = 1.5707963705062866;
				baseMyBullet_Touhou.Damage = 4;
			}
			else
			{
				BulletGuidance bulletGuidance = new BulletGuidance(base.StageData, "AyaBullet10", base.Position, 8f, base.ShootDirection);
				bulletGuidance.TransparentValueF = 180f;
				bulletGuidance.Angle = 1.5707963705062866;
			}
		}
	}
}
