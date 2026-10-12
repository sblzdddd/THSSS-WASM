using System;
using System.Drawing;

namespace Shooting
{
	public class BulletPicEmitter
	{
		public static void ShootBulletPic00(StageDataPackage StageData, bool[,] BulletPic, string BulletName, PointF OriginalPosition, byte ColorType)
		{
			var num = (int)Math.Sqrt((double)BulletPic.Length);
			for (var i = 0; i < num; i++)
			{
				for (var j = 0; j < num; j++)
				{
					if (BulletPic[i, j])
					{
						var pointF = new PointF(OriginalPosition.X + (float)i - (float)(num / 2), OriginalPosition.Y + (float)j - (float)(num / 2));
						var baseBullet_Touhou = new BaseBullet_Touhou(StageData, BulletName, pointF, 0f, 0.0, ColorType);
						baseBullet_Touhou.Velocity = (float)baseBullet_Touhou.GetDistance(OriginalPosition) * 3f / (float)num;
						baseBullet_Touhou.Direction = baseBullet_Touhou.GetDirection(OriginalPosition) + 3.141592653589793;
					}
				}
			}
		}

		public static void ShootBulletPic01(StageDataPackage StageData, bool[,] BulletPic, string BulletName, PointF OriginalPosition, double Direction, byte ColorType, float Scale, float bigger)
		{
			ShootBulletPic01(StageData, BulletPic, BulletName, OriginalPosition, 1f, Direction, ColorType, Scale, bigger);
		}

		public static void ShootBulletPic01(StageDataPackage StageData, bool[,] BulletPic, string BulletName, PointF OriginalPosition, float BaseVelocity, double Direction, byte ColorType, float Scale, float bigger)
		{
			var num = (int)Math.Sqrt((double)BulletPic.Length);
			for (var i = 0; i < num; i++)
			{
				for (var j = 0; j < num; j++)
				{
					if (BulletPic[i, j])
					{
						var num2 = ((float)i - ((float)num - 1f) / 2f) * (float)Math.Cos(Direction - 1.5707963267948966);
						var num3 = -((float)i - ((float)num - 1f) / 2f) * (float)Math.Sin(Direction - 1.5707963267948966);
						var pointF = new PointF(OriginalPosition.X + num2 * Scale, OriginalPosition.Y - num3 * Scale);
						var baseBullet_Touhou = new BaseBullet_Touhou(StageData, BulletName, pointF, 0f, Direction - (double)(((float)i - ((float)num - 1f) / 2f) * bigger), ColorType);
						baseBullet_Touhou.Velocity = BaseVelocity + (float)j * 2f / (float)num;
					}
				}
			}
		}

		public static void ShootBulletPic02(StageDataPackage StageData, string BPicName, BaseObject Bullet, PointF OriginalPosition, double Direction, byte ColorType, float Scale, bool Mirrored)
		{
			var array = StageData.GlobalData.BulletPicDictionary[BPicName];
			var num = (int)Math.Sqrt((double)array.Length);
			var num2 = (Mirrored ? (-1) : 1);
			for (var i = 0; i < num; i++)
			{
				for (var j = 0; j < num; j++)
				{
					if (array[i, j])
					{
						var num3 = (float)num2 * ((float)i - ((float)num - 1f) / 2f) * (float)Math.Cos(Direction - 1.5707963267948966) + ((float)j - ((float)num - 1f) / 2f) * (float)Math.Sin(Direction - 1.5707963267948966);
						var num4 = (float)(-(float)num2) * ((float)i - ((float)num - 1f) / 2f) * (float)Math.Sin(Direction - 1.5707963267948966) + ((float)j - ((float)num - 1f) / 2f) * (float)Math.Cos(Direction - 1.5707963267948966);
						var pointF = new PointF(OriginalPosition.X + num3 * Scale, OriginalPosition.Y - num4 * Scale);
						var baseBullet_Touhou = new BaseBullet_Touhou(StageData, null, OriginalPosition, 0f, 0.0);
						baseBullet_Touhou.Copy(Bullet);
						baseBullet_Touhou.ColorType = ColorType;
						baseBullet_Touhou.OriginalPosition = OriginalPosition;
						baseBullet_Touhou.DestPoint = pointF;
					}
				}
			}
		}

		public static void ShootItem(StageDataPackage StageData, string BPicName, PointF OriginalPosition, float Scale)
		{
			var array = StageData.GlobalData.BulletPicDictionary[BPicName];
			var num = (int)Math.Sqrt((double)array.Length);
			for (var i = 0; i < num; i++)
			{
				for (var j = 0; j < num; j++)
				{
					if (array[i, j])
					{
						var pointF = new PointF(OriginalPosition.X + (float)(i - num / 2) * Scale + (float)StageData.Ran.Next(-2, 3), OriginalPosition.Y + (float)(j - num / 2) * Scale + (float)StageData.Ran.Next(-2, 3));
						if (StageData.Ran.Next(100) < 60)
						{
							new ScoreItem_Touhou(StageData, pointF);
						}
						else
						{
							new PowerItem_Touhou(StageData, pointF);
						}
					}
				}
			}
		}
	}
}
