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
			HealthPoint = 30f;
			Scale = 0.2f;
			MaxScale = 1f;
			ScaleVelocity = 0.2f;
		}

		public override void GiveItems()
		{
			if (ColorType == 0 || ColorType == 2)
			{
				var pointF = new PointF(OriginalPosition.X + (float)Ran.Next(-5, 5), OriginalPosition.Y + (float)Ran.Next(-5, 5));
				new PowerItem_Touhou(StageData, pointF);
			}
			else if (ColorType == 1 || ColorType == 3)
			{
				var pointF = new PointF(OriginalPosition.X + (float)Ran.Next(-5, 5), OriginalPosition.Y + (float)Ran.Next(-5, 5));
				new ScoreItem_Touhou(StageData, OriginalPosition);
			}
			if (Difficulty > DifficultLevel.Normal)
			{
				var num = ((Difficulty > DifficultLevel.Hard) ? 8 : 1);
				var num2 = GetDirection(MyPlane);
				for (var i = 0; i < num; i++)
				{
					for (var j = 0; j < 5; j++)
					{
						var num3 = (int)(ColorType * 3 + 2);
						new Bullet_Touhou_01(StageData, "bullet2_" + num3, OriginalPosition, 1.5f + (float)j * 0.4f, num2, (byte)num3);
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
			TxtureObject = TextureObjectDictionary[string.Concat(new object[]
			{
				"EnemyGhost",
				ColorType,
				"_",
				Time % 32 / 4
			})];
			var num = 6;
			var pointF = new PointF(Position.X + (float)Ran.Next(num) * (float)Math.Sin((double)Ran.Next(360) * 3.141592653589793 / 180.0), Position.Y + (float)Ran.Next(num) * (float)Math.Cos((double)Ran.Next(360) * 3.141592653589793 / 180.0));
			var baseEffect = new BaseEffect(StageData, string.Concat(new object[]
			{
				"EnemyGhost",
				ColorType,
				"_",
				Time % 32 / 4
			}), pointF, 1.5f, -1.5707963267948966);
			baseEffect.Scale = 0.5f;
			baseEffect.LifeTime = 13;
			baseEffect.ScaleVelocity = -0.04f;
		}
	}
}
