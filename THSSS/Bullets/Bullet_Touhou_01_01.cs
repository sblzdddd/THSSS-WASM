using System.Drawing;

namespace Shooting
{
	public class Bullet_Touhou_01_01 : Bullet_Touhou_01
	{
		public string SubBulletName { get; set; }

		public Bullet_Touhou_01_01(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction, ColorType)
		{
		}

		public override bool OutBoundary()
		{
			bool flag;
			if (LifeTime != 0 && Time > LifeTime)
			{
				flag = true;
			}
			else if (Reflect <= 0)
			{
				if (Time < 100)
				{
					flag = false;
				}
				else
				{
					int num;
					if (Region < 10)
					{
						num = 10;
					}
					else
					{
						num = Region;
					}
					flag = (float)(-(float)num) >= OriginalPosition.X || OriginalPosition.X >= (float)(BoundRect.Width + num) || (float)(-(float)num) >= OriginalPosition.Y || OriginalPosition.Y >= (float)(BoundRect.Height + num);
				}
			}
			else
			{
				int num = 5;
				if (OriginalPosition.X < (float)num || OriginalPosition.X >= (float)(BoundRect.Width - num))
				{
					Reflect -= 1;
					Direction -= 2.0 * (Direction - 1.5707963267948966);
					new BaseBullet_Touhou(StageData, SubBulletName, OriginalPosition, Velocity, Direction, ColorType);
					GiveEndEffect();
					flag = true;
				}
				else
				{
					flag = OriginalPosition.Y > (float)(BoundRect.Bottom + num);
				}
			}
			return flag;
		}
	}
}
