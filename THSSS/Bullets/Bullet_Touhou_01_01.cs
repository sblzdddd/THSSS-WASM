using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000028 RID: 40
	public class Bullet_Touhou_01_01 : Bullet_Touhou_01
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000AD44 File Offset: 0x00008F44
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x0000AD5B File Offset: 0x00008F5B
		public string SubBulletName { get; set; }

		// Token: 0x060001D2 RID: 466 RVA: 0x0000AD64 File Offset: 0x00008F64
		public Bullet_Touhou_01_01(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction, ColorType)
		{
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000AD78 File Offset: 0x00008F78
		public override bool OutBoundary()
		{
			bool flag;
			if (base.LifeTime != 0 && base.Time > base.LifeTime)
			{
				flag = true;
			}
			else if (base.Reflect <= 0)
			{
				if (base.Time < 100)
				{
					flag = false;
				}
				else
				{
					int num;
					if (base.Region < 10)
					{
						num = 10;
					}
					else
					{
						num = base.Region;
					}
					flag = (float)(-(float)num) >= base.OriginalPosition.X || base.OriginalPosition.X >= (float)(base.BoundRect.Width + num) || (float)(-(float)num) >= base.OriginalPosition.Y || base.OriginalPosition.Y >= (float)(base.BoundRect.Height + num);
				}
			}
			else
			{
				int num = 5;
				if (base.OriginalPosition.X < (float)num || base.OriginalPosition.X >= (float)(base.BoundRect.Width - num))
				{
					base.Reflect -= 1;
					base.Direction -= 2.0 * (base.Direction - 1.5707963267948966);
					new BaseBullet_Touhou(base.StageData, this.SubBulletName, base.OriginalPosition, base.Velocity, base.Direction, base.ColorType);
					this.GiveEndEffect();
					flag = true;
				}
				else
				{
					flag = base.OriginalPosition.Y > (float)(base.BoundRect.Bottom + num);
				}
			}
			return flag;
		}
	}
}
