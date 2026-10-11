using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001EC RID: 492
	public class Bullet_Touhou_01_03 : Bullet_Touhou_01
	{
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0006F014 File Offset: 0x0006D214
		// (set) Token: 0x06000B3A RID: 2874 RVA: 0x0006F02B File Offset: 0x0006D22B
		public string SubBulletName { get; set; }

		// Token: 0x06000B3B RID: 2875 RVA: 0x0006F034 File Offset: 0x0006D234
		public Bullet_Touhou_01_03(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction, ColorType)
		{
			base.LifeTime = 50;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0006F054 File Offset: 0x0006D254
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == base.LifeTime)
			{
				byte colorType = base.ColorType;
				BaseObject baseObject = new BaseObject(base.StageData, this.SubBulletName + colorType.ToString(), new PointF(0f, 0f), 2.5f, 0.0);
				new BulletGroupCircle(base.StageData, baseObject, base.OriginalPosition, base.Direction, (int)base.Difficulty * (int)DifficultLevel.Hard + 3, 0, colorType);
				this.GiveEndEffect();
			}
		}
	}
}
