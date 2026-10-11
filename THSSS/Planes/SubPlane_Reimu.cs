using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000F0 RID: 240
	public class SubPlane_Reimu : BaseSubPlane
	{
		// Token: 0x0600067C RID: 1660 RVA: 0x000399E1 File Offset: 0x00037BE1
		public SubPlane_Reimu(StageDataPackage StageData, PointF Position)
			: base(StageData, "ReimuSubPlane", Position)
		{
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x000399F4 File Offset: 0x00037BF4
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

		// Token: 0x0600067E RID: 1662 RVA: 0x00039A3C File Offset: 0x00037C3C
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

		// Token: 0x0600067F RID: 1663 RVA: 0x00039B3C File Offset: 0x00037D3C
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
