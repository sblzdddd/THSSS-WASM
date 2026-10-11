using System;
using System.Drawing;

namespace Shooting
{
	internal class EnemyPlane_TouhouGhost : BaseEnemyPlane_Touhou
	{
		public bool BackFire { get; set; }

		public Color BackFireColor { get; set; }

		public EnemyPlane_TouhouGhost(StageDataPackage StageData, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: base(StageData, null, OriginalPosition, Velocity, Direction, ColorType)
		{
			base.HealthPoint = 30f;
			base.Scale = 0.2f;
			base.MaxScale = 1f;
			base.ScaleVelocity = 0.2f;
		}

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

		public override void Shoot()
		{
		}

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
