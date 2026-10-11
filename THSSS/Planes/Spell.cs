using System;
using System.Drawing;

namespace Shooting
{
	public class Spell : BaseSpell
	{
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

		public override void Move()
		{
			base.Position = base.MyPlane.Position;
		}

		public override bool HitCheck(BaseObject Enemy)
		{
			return Enemy.Position.Y < base.Position.Y + 5f && base.Position.X - 140f < Enemy.Position.X && Enemy.Position.X < base.Position.X + 140f;
		}

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

		public override void Show()
		{
		}
	}
}
