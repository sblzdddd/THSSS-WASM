using System;
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
