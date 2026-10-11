using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001CE RID: 462
	public class Bullet_Touhou_01_04 : Bullet_Touhou_01
	{
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x00067490 File Offset: 0x00065690
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x000674A7 File Offset: 0x000656A7
		public string SubBulletName { get; set; }

		// Token: 0x06000A9F RID: 2719 RVA: 0x000674B0 File Offset: 0x000656B0
		public Bullet_Touhou_01_04(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction, ColorType)
		{
			base.LifeTime = 50;
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x000674D0 File Offset: 0x000656D0
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == base.LifeTime)
			{
				byte colorType = base.ColorType;
				int num = (int)(base.Difficulty + 4);
				float num2 = -0.5235988f;
				float num3 = -2.0943952f;
				switch (base.Difficulty)
				{
				case DifficultLevel.Easy:
					num = 2;
					break;
				case DifficultLevel.Normal:
					num = 4;
					break;
				case DifficultLevel.Hard:
					num = 6;
					break;
				case DifficultLevel.Lunatic:
					num = 7;
					break;
				case DifficultLevel.Ultra:
					num = 8;
					break;
				}
				float num4 = num3 / ((float)num - 1f);
				for (int i = 0; i < num; i++)
				{
					BaseBullet_Touhou baseBullet_Touhou = new BaseBullet_Touhou(base.StageData, this.SubBulletName + colorType.ToString(), base.OriginalPosition, 2.5f, (double)num2, colorType);
					baseBullet_Touhou.Ay = 0.08f;
					baseBullet_Touhou.MaxVelocity = 2.5f;
					num2 += num4;
				}
				base.StageData.SoundPlay("se_kira01.wav");
				this.GiveEndEffect();
			}
		}
	}
}
