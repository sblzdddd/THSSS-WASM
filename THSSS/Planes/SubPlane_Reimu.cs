using System;
using System.Drawing;

namespace Shooting
{
	public class SubPlane_Reimu : BaseSubPlane
	{
		public SubPlane_Reimu(StageDataPackage StageData, PointF Position)
			: base(StageData, "ReimuSubPlane", Position)
		{
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.KClass.Key_Shift)
			{
				base.TransparentVelocity = -20f;
			}
			else
			{
				base.TransparentVelocity = 20f;
			}
		}

		public override void Shoot()
		{
			if (base.KClass.Key_Shift)
			{
				BaseMyBullet_Touhou baseMyBullet_Touhou = new BaseMyBullet_Touhou(base.StageData, "MaruBullet", base.OriginalPosition, 18f, base.ShootDirection + 0.05235987755982988);
				baseMyBullet_Touhou.Angle = 1.5707963705062866;
				baseMyBullet_Touhou.Damage = 5;
				BaseMyBullet_Touhou baseMyBullet_Touhou2 = new BaseMyBullet_Touhou(base.StageData, "MaruBullet", base.OriginalPosition, 18f, base.ShootDirection - 0.05235987755982988);
				baseMyBullet_Touhou2.Angle = 1.5707963705062866;
				baseMyBullet_Touhou2.Damage = 5;
			}
			else
			{
				BulletGuidance bulletGuidance = new BulletGuidance(base.StageData, "ReimuSubBullet", base.Position, 8f, base.ShootDirection);
				bulletGuidance.TransparentValueF = 180f;
				bulletGuidance.Angle = 1.5707963705062866;
				bulletGuidance.Damage = 9;
			}
		}

		public override void Show()
		{
			if (base.Enabled)
			{
				if (base.TxtureObject != null)
				{
					double num = (double)(-(double)base.Time * 5 % 360) * 3.141592653589793 / 180.0;
					if (base.MyPlane.Power >= 400)
					{
						base.SpriteMain.Draw2D(base.TxtureObject, 1.3f, 1.3f, -(float)num, base.Position, Color.FromArgb(base.TransparentValue * 6 / 10, base.ColorValue), base.Mirrored);
					}
					base.SpriteMain.Draw2D(base.TxtureObject, base.ScaleWidth, base.ScaleLength, (float)num, base.Position, Color.FromArgb(base.TransparentValue, base.ColorValue), base.Mirrored);
				}
			}
		}
	}
}
