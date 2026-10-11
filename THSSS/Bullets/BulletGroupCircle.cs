using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001AC RID: 428
	public class BulletGroupCircle : BaseObject
	{
		// Token: 0x06000A2D RID: 2605 RVA: 0x00060A28 File Offset: 0x0005EC28
		public BulletGroupCircle(StageDataPackage StageData, BaseObject bullet, PointF OriginalPosition, double Direction, int Count, int Radius, byte ColorType)
		{
			double num = Direction;
			for (int i = 0; i < Count; i++)
			{
				PointF pointF = new PointF((float)((double)OriginalPosition.X + (double)Radius * Math.Cos(num)), (float)((double)OriginalPosition.Y + (double)Radius * Math.Sin(num)));
				BaseBullet_Touhou baseBullet_Touhou = new BaseBullet_Touhou(StageData, null, pointF, 0f, 0.0, ColorType);
				baseBullet_Touhou.Copy(bullet);
				baseBullet_Touhou.OriginalPosition = pointF;
				baseBullet_Touhou.Direction = num;
				num += 6.283185307179586 / (double)Count;
			}
			StageData.SoundPlay("se_tan00a.wav");
		}
	}
}
