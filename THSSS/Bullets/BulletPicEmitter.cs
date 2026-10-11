using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000FA RID: 250
	public class BulletPicEmitter
	{
		// Token: 0x06000703 RID: 1795 RVA: 0x0003EB28 File Offset: 0x0003CD28
		public static void ShootBulletPic00(StageDataPackage StageData, bool[,] BulletPic, string BulletName, PointF OriginalPosition, byte ColorType)
		{
			int num = (int)Math.Sqrt((double)BulletPic.Length);
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num; j++)
				{
					if (BulletPic[i, j])
					{
						PointF pointF = new PointF(OriginalPosition.X + (float)i - (float)(num / 2), OriginalPosition.Y + (float)j - (float)(num / 2));
						BaseBullet_Touhou baseBullet_Touhou = new BaseBullet_Touhou(StageData, BulletName, pointF, 0f, 0.0, ColorType);
						baseBullet_Touhou.Velocity = (float)baseBullet_Touhou.GetDistance(OriginalPosition) * 3f / (float)num;
						baseBullet_Touhou.Direction = baseBullet_Touhou.GetDirection(OriginalPosition) + 3.141592653589793;
					}
				}
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0003EC00 File Offset: 0x0003CE00
		public static void ShootBulletPic01(StageDataPackage StageData, bool[,] BulletPic, string BulletName, PointF OriginalPosition, double Direction, byte ColorType, float Scale, float bigger)
		{
			BulletPicEmitter.ShootBulletPic01(StageData, BulletPic, BulletName, OriginalPosition, 1f, Direction, ColorType, Scale, bigger);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0003EC28 File Offset: 0x0003CE28
		public static void ShootBulletPic01(StageDataPackage StageData, bool[,] BulletPic, string BulletName, PointF OriginalPosition, float BaseVelocity, double Direction, byte ColorType, float Scale, float bigger)
		{
			int num = (int)Math.Sqrt((double)BulletPic.Length);
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num; j++)
				{
					if (BulletPic[i, j])
					{
						float num2 = ((float)i - ((float)num - 1f) / 2f) * (float)Math.Cos(Direction - 1.5707963267948966);
						float num3 = -((float)i - ((float)num - 1f) / 2f) * (float)Math.Sin(Direction - 1.5707963267948966);
						PointF pointF = new PointF(OriginalPosition.X + num2 * Scale, OriginalPosition.Y - num3 * Scale);
						BaseBullet_Touhou baseBullet_Touhou = new BaseBullet_Touhou(StageData, BulletName, pointF, 0f, Direction - (double)(((float)i - ((float)num - 1f) / 2f) * bigger), ColorType);
						baseBullet_Touhou.Velocity = BaseVelocity + (float)j * 2f / (float)num;
					}
				}
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0003ED40 File Offset: 0x0003CF40
		public static void ShootBulletPic02(StageDataPackage StageData, string BPicName, BaseObject Bullet, PointF OriginalPosition, double Direction, byte ColorType, float Scale, bool Mirrored)
		{
			bool[,] array = StageData.GlobalData.BulletPicDictionary[BPicName];
			int num = (int)Math.Sqrt((double)array.Length);
			int num2 = (Mirrored ? (-1) : 1);
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num; j++)
				{
					if (array[i, j])
					{
						float num3 = (float)num2 * ((float)i - ((float)num - 1f) / 2f) * (float)Math.Cos(Direction - 1.5707963267948966) + ((float)j - ((float)num - 1f) / 2f) * (float)Math.Sin(Direction - 1.5707963267948966);
						float num4 = (float)(-(float)num2) * ((float)i - ((float)num - 1f) / 2f) * (float)Math.Sin(Direction - 1.5707963267948966) + ((float)j - ((float)num - 1f) / 2f) * (float)Math.Cos(Direction - 1.5707963267948966);
						PointF pointF = new PointF(OriginalPosition.X + num3 * Scale, OriginalPosition.Y - num4 * Scale);
						BaseBullet_Touhou baseBullet_Touhou = new BaseBullet_Touhou(StageData, null, OriginalPosition, 0f, 0.0);
						baseBullet_Touhou.Copy(Bullet);
						baseBullet_Touhou.ColorType = ColorType;
						baseBullet_Touhou.OriginalPosition = OriginalPosition;
						baseBullet_Touhou.DestPoint = pointF;
					}
				}
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0003EECC File Offset: 0x0003D0CC
		public static void ShootItem(StageDataPackage StageData, string BPicName, PointF OriginalPosition, float Scale)
		{
			bool[,] array = StageData.GlobalData.BulletPicDictionary[BPicName];
			int num = (int)Math.Sqrt((double)array.Length);
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < num; j++)
				{
					if (array[i, j])
					{
						PointF pointF = new PointF(OriginalPosition.X + (float)(i - num / 2) * Scale + (float)StageData.Ran.Next(-2, 3), OriginalPosition.Y + (float)(j - num / 2) * Scale + (float)StageData.Ran.Next(-2, 3));
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
