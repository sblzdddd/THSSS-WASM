using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000191 RID: 401
	internal class EnemyPlane_TouhouGhost : BaseEnemyPlane_Touhou
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x0005B9F4 File Offset: 0x00059BF4
		// (set) Token: 0x06000996 RID: 2454 RVA: 0x0005BA0B File Offset: 0x00059C0B
		public bool BackFire { get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x0005BA14 File Offset: 0x00059C14
		// (set) Token: 0x06000998 RID: 2456 RVA: 0x0005BA2B File Offset: 0x00059C2B
		public Color BackFireColor { get; set; }

		// Token: 0x06000999 RID: 2457 RVA: 0x0005BA34 File Offset: 0x00059C34
		public EnemyPlane_TouhouGhost(StageDataPackage StageData, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: base(StageData, null, OriginalPosition, Velocity, Direction, ColorType)
		{
			base.HealthPoint = 30f;
			base.Scale = 0.2f;
			base.MaxScale = 1f;
			base.ScaleVelocity = 0.2f;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0005BA84 File Offset: 0x00059C84
		public override void GiveItems()
		{
			if (base.ColorType == 0 || base.ColorType == 2)
			{
				PointF pointF = new PointF(base.OriginalPosition.X + (float)base.Ran.Next(-5, 5), base.OriginalPosition.Y + (float)base.Ran.Next(-5, 5));
				new PowerItem_Touhou(base.StageData, pointF);
			}
			else if (base.ColorType == 1 || base.ColorType == 3)
			{
				PointF pointF = new PointF(base.OriginalPosition.X + (float)base.Ran.Next(-5, 5), base.OriginalPosition.Y + (float)base.Ran.Next(-5, 5));
				new ScoreItem_Touhou(base.StageData, base.OriginalPosition);
			}
			if (base.Difficulty > DifficultLevel.Normal)
			{
				int num = ((base.Difficulty > DifficultLevel.Hard) ? 8 : 1);
				double num2 = base.GetDirection(base.MyPlane);
				for (int i = 0; i < num; i++)
				{
					for (int j = 0; j < 5; j++)
					{
						int num3 = (int)(base.ColorType * 3 + 2);
						new Bullet_Touhou_01(base.StageData, "bullet2_" + num3, base.OriginalPosition, 1.5f + (float)j * 0.4f, num2, (byte)num3);
					}
					num2 += 6.283185307179586 / (double)num;
				}
			}
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0005BC2D File Offset: 0x00059E2D
		public override void Shoot()
		{
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0005BC30 File Offset: 0x00059E30
		public override void TextureCtrl()
		{
			base.TxtureObject = base.TextureObjectDictionary[string.Concat(new object[]
			{
				"EnemyGhost",
				base.ColorType,
				"_",
				base.Time % 32 / 4
			})];
			int num = 6;
			PointF pointF = new PointF(base.Position.X + (float)base.Ran.Next(num) * (float)Math.Sin((double)base.Ran.Next(360) * 3.141592653589793 / 180.0), base.Position.Y + (float)base.Ran.Next(num) * (float)Math.Cos((double)base.Ran.Next(360) * 3.141592653589793 / 180.0));
			BaseEffect baseEffect = new BaseEffect(base.StageData, string.Concat(new object[]
			{
				"EnemyGhost",
				base.ColorType,
				"_",
				base.Time % 32 / 4
			}), pointF, 1.5f, -1.5707963267948966);
			baseEffect.Scale = 0.5f;
			baseEffect.LifeTime = 13;
			baseEffect.ScaleVelocity = -0.04f;
		}
	}
}
