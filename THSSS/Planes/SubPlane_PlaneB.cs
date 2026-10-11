using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000037 RID: 55
	public class SubPlane_PlaneB : BaseSubPlane
	{
		// Token: 0x0600022A RID: 554 RVA: 0x0000D927 File Offset: 0x0000BB27
		public SubPlane_PlaneB(StageDataPackage StageData, PointF Position)
			: base(StageData, "MySubPlane", Position)
		{
			base.ShootDirection = -1.5707963267948966;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000D94C File Offset: 0x0000BB4C
		public override void Shoot()
		{
			BaseMyBullet_Touhou baseMyBullet_Touhou = new BaseMyBullet_Touhou(base.StageData, "子机子弹", base.OriginalPosition, 4f, base.ShootDirection);
			baseMyBullet_Touhou.Angle = 3.1415927410125732;
			baseMyBullet_Touhou.Damage = 5;
			baseMyBullet_Touhou.Accelerate = 0.5f;
		}
	}
}
