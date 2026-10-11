using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000202 RID: 514
	public class Bullet_Touhou_01_02 : Bullet_Touhou_01
	{
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x00072570 File Offset: 0x00070770
		// (set) Token: 0x06000B88 RID: 2952 RVA: 0x00072587 File Offset: 0x00070787
		public string SubBulletName { get; set; }

		// Token: 0x06000B89 RID: 2953 RVA: 0x00072590 File Offset: 0x00070790
		public Bullet_Touhou_01_02(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction, ColorType)
		{
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x000725A4 File Offset: 0x000707A4
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == base.LifeTime)
			{
				new BaseBullet_Touhou(base.StageData, this.SubBulletName, base.OriginalPosition, base.Velocity / 2f, base.Direction, base.ColorType);
				this.GiveEndEffect();
			}
		}
	}
}
