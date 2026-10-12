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
			if (KClass.Key_Shift)
			{
				TransparentVelocity = -20f;
			}
			else
			{
				TransparentVelocity = 20f;
			}
		}

		public override void Shoot()
		{
			if (KClass.Key_Shift)
			{
				BaseMyBullet_Touhou baseMyBullet_Touhou = new BaseMyBullet_Touhou(StageData, "MaruBullet", OriginalPosition, 18f, ShootDirection + 0.05235987755982988);
				baseMyBullet_Touhou.Angle = 1.5707963705062866;
				baseMyBullet_Touhou.Damage = 5;
				BaseMyBullet_Touhou baseMyBullet_Touhou2 = new BaseMyBullet_Touhou(StageData, "MaruBullet", OriginalPosition, 18f, ShootDirection - 0.05235987755982988);
				baseMyBullet_Touhou2.Angle = 1.5707963705062866;
				baseMyBullet_Touhou2.Damage = 5;
			}
			else
			{
				BulletGuidance bulletGuidance = new BulletGuidance(StageData, "ReimuSubBullet", Position, 8f, ShootDirection);
				bulletGuidance.TransparentValueF = 180f;
				bulletGuidance.Angle = 1.5707963705062866;
				bulletGuidance.Damage = 9;
			}
		}

		public override void Show()
		{
			if (Enabled)
			{
				if (TxtureObject != null)
				{
					double num = (double)(-(double)Time * 5 % 360) * 3.141592653589793 / 180.0;
					if (MyPlane.Power >= 400)
					{
						SpriteMain.Draw2D(TxtureObject, 1.3f, 1.3f, -(float)num, Position, Color.FromArgb(TransparentValue * 6 / 10, ColorValue), Mirrored);
					}
					SpriteMain.Draw2D(TxtureObject, ScaleWidth, ScaleLength, (float)num, Position, Color.FromArgb(TransparentValue, ColorValue), Mirrored);
				}
			}
		}
	}
}
