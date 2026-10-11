using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000189 RID: 393
	public class Spell : BaseSpell
	{
		// Token: 0x06000951 RID: 2385 RVA: 0x0005A568 File Offset: 0x00058768
		public Spell(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.Position = base.MyPlane.Position;
			base.Damage = 10;
			base.LifeTime = 300;
			base.SpellList.Add(this);
			StageData.VibrateStart(base.LifeTime);
			new BackgroundBlack(StageData, base.Position);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0005A5D4 File Offset: 0x000587D4
		public override void Move()
		{
			base.Position = base.MyPlane.Position;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0005A5EC File Offset: 0x000587EC
		public override bool HitCheck(BaseObject Enemy)
		{
			return Enemy.Position.Y < base.Position.Y + 5f && base.Position.X - 140f < Enemy.Position.X && Enemy.Position.X < base.Position.X + 140f;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0005A67C File Offset: 0x0005887C
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time % 4 == 0)
			{
				int num = 20;
				float num2 = 2f;
				for (int i = 0; i < num; i++)
				{
					new ParticleColored(base.StageData, "光点", new PointF(base.Position.X - (float)num + (float)(2 * i), base.Position.Y - 20f), 20f, -1.5707963267948966 - (double)(num2 / 2f) + (double)((float)i / (float)num * num2));
				}
			}
			if (base.MyPlane.Position.Y < (float)base.BoundRect.Bottom)
			{
				base.MyPlane.Position = new PointF(base.MyPlane.Position.X, base.MyPlane.Position.Y + 2f);
			}
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0005A790 File Offset: 0x00058990
		public override void Show()
		{
		}
	}
}
