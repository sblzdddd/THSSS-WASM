using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200000C RID: 12
	public class SubPlane_AyaB : BaseSubPlane
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00005D0B File Offset: 0x00003F0B
		public SubPlane_AyaB(StageDataPackage StageData, PointF Position)
			: base(StageData, "MarisaSubPlane10", Position)
		{
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005D1D File Offset: 0x00003F1D
		public override void Refresh()
		{
			base.Refresh();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005D28 File Offset: 0x00003F28
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
