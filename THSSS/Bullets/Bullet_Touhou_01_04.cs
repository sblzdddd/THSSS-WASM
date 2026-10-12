using System.Drawing;

namespace Shooting
{
	public class Bullet_Touhou_01_04 : Bullet_Touhou_01
	{
		public string SubBulletName { get; set; }

		public Bullet_Touhou_01_04(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction, ColorType)
		{
			LifeTime = 50;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time == LifeTime)
			{
				var colorType = ColorType;
				var num = (int)(Difficulty + 4);
				var num2 = -0.5235988f;
				var num3 = -2.0943952f;
				switch (Difficulty)
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
				var num4 = num3 / ((float)num - 1f);
				for (var i = 0; i < num; i++)
				{
					var baseBullet_Touhou = new BaseBullet_Touhou(StageData, SubBulletName + colorType.ToString(), OriginalPosition, 2.5f, (double)num2, colorType);
					baseBullet_Touhou.Ay = 0.08f;
					baseBullet_Touhou.MaxVelocity = 2.5f;
					num2 += num4;
				}
				StageData.SoundPlay("se_kira01.wav");
				GiveEndEffect();
			}
		}
	}
}
