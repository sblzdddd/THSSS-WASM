using System;
using System.Drawing;

namespace Shooting
{
	public class SubPlane_Sanae : BaseSubPlane
	{
		public SubPlane_Sanae(StageDataPackage StageData, PointF Position)
			: base(StageData, "SanaeSubPlane", Position)
		{
			AngleDegree = 90.0;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			Scale = 1f + 0.1f * (float)Math.Sin((double)((float)TimeMain / 40f * 2f) * 3.141592653589793);
		}

		public override void Shoot()
		{
			if (KClass.Key_Shift)
			{
				BaseMyBullet_Touhou baseMyBullet_Touhou = new BaseMyBullet_Touhou(StageData, "SanaeSubBullet02", OriginalPosition, 40f, ShootDirection - 0.029999999329447746);
				baseMyBullet_Touhou.Angle = 1.5707963705062866;
				baseMyBullet_Touhou.Damage = 3;
				BaseMyBullet_Touhou baseMyBullet_Touhou2 = new BaseMyBullet_Touhou(StageData, "SanaeSubBullet02", OriginalPosition, 40f, ShootDirection + 0.029999999329447746);
				baseMyBullet_Touhou2.Angle = 1.5707963705062866;
				baseMyBullet_Touhou2.Damage = 3;
			}
			else
			{
				BaseMyBullet_Touhou baseMyBullet_Touhou3 = new BaseMyBullet_Touhou(StageData, "SanaeSubBullet00", OriginalPosition, 30f, ShootDirection - 0.15000000596046448);
				baseMyBullet_Touhou3.Angle = 3.1415927410125732;
				baseMyBullet_Touhou3.Damage = 7;
				baseMyBullet_Touhou3.Mirrored = Mirrored;
				BaseMyBullet_Touhou baseMyBullet_Touhou4 = new BaseMyBullet_Touhou(StageData, "SanaeSubBullet00", OriginalPosition, 30f, ShootDirection + 0.15000000596046448);
				baseMyBullet_Touhou4.Angle = 3.1415927410125732;
				baseMyBullet_Touhou4.Damage = 7;
				baseMyBullet_Touhou4.Mirrored = Mirrored;
				BaseMyBullet_Touhou baseMyBullet_Touhou5 = new BaseMyBullet_Touhou(StageData, "SanaeSubBullet00", OriginalPosition, 30f, ShootDirection);
				baseMyBullet_Touhou5.Angle = 3.1415927410125732;
				baseMyBullet_Touhou5.Damage = 7;
				baseMyBullet_Touhou5.Mirrored = Mirrored;
			}
		}

		public override void Show()
		{
			if (Enabled)
			{
				if (MyPlane.Power >= 400)
				{
					float scale = Scale;
					float transparentValueF = TransparentValueF;
					Scale = 1.3f;
					TransparentValueF = 150f;
					base.Show();
					TransparentValueF = transparentValueF;
					Scale = scale;
				}
				base.Show();
			}
		}
	}
}
