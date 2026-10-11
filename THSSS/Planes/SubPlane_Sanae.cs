using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200000B RID: 11
	public class SubPlane_Sanae : BaseSubPlane
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00005A76 File Offset: 0x00003C76
		public SubPlane_Sanae(StageDataPackage StageData, PointF Position)
			: base(StageData, "SanaeSubPlane", Position)
		{
			base.AngleDegree = 90.0;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005A98 File Offset: 0x00003C98
		public override void Ctrl()
		{
			base.Ctrl();
			base.Scale = 1f + 0.1f * (float)Math.Sin((double)((float)base.TimeMain / 40f * 2f) * 3.141592653589793);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005AE4 File Offset: 0x00003CE4
		public override void Shoot()
		{
			if (base.KClass.Key_Shift)
			{
				BaseMyBullet_Touhou baseMyBullet_Touhou = new BaseMyBullet_Touhou(base.StageData, "SanaeSubBullet02", base.OriginalPosition, 40f, base.ShootDirection - 0.029999999329447746);
				baseMyBullet_Touhou.Angle = 1.5707963705062866;
				baseMyBullet_Touhou.Damage = 3;
				BaseMyBullet_Touhou baseMyBullet_Touhou2 = new BaseMyBullet_Touhou(base.StageData, "SanaeSubBullet02", base.OriginalPosition, 40f, base.ShootDirection + 0.029999999329447746);
				baseMyBullet_Touhou2.Angle = 1.5707963705062866;
				baseMyBullet_Touhou2.Damage = 3;
			}
			else
			{
				BaseMyBullet_Touhou baseMyBullet_Touhou3 = new BaseMyBullet_Touhou(base.StageData, "SanaeSubBullet00", base.OriginalPosition, 30f, base.ShootDirection - 0.15000000596046448);
				baseMyBullet_Touhou3.Angle = 3.1415927410125732;
				baseMyBullet_Touhou3.Damage = 7;
				baseMyBullet_Touhou3.Mirrored = base.Mirrored;
				BaseMyBullet_Touhou baseMyBullet_Touhou4 = new BaseMyBullet_Touhou(base.StageData, "SanaeSubBullet00", base.OriginalPosition, 30f, base.ShootDirection + 0.15000000596046448);
				baseMyBullet_Touhou4.Angle = 3.1415927410125732;
				baseMyBullet_Touhou4.Damage = 7;
				baseMyBullet_Touhou4.Mirrored = base.Mirrored;
				BaseMyBullet_Touhou baseMyBullet_Touhou5 = new BaseMyBullet_Touhou(base.StageData, "SanaeSubBullet00", base.OriginalPosition, 30f, base.ShootDirection);
				baseMyBullet_Touhou5.Angle = 3.1415927410125732;
				baseMyBullet_Touhou5.Damage = 7;
				baseMyBullet_Touhou5.Mirrored = base.Mirrored;
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005C94 File Offset: 0x00003E94
		public override void Show()
		{
			if (base.Enabled)
			{
				if (base.MyPlane.Power >= 400)
				{
					float scale = base.Scale;
					float transparentValueF = base.TransparentValueF;
					base.Scale = 1.3f;
					base.TransparentValueF = 150f;
					base.Show();
					base.TransparentValueF = transparentValueF;
					base.Scale = scale;
				}
				base.Show();
			}
		}
	}
}
