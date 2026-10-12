using System;
using System.Drawing;

namespace Shooting
{
	public class BulletPicEmitter01 : BaseBulleEmitter
	{
		public BulletPicEmitter01(StageDataPackage StageData, string BPicName, BaseObject Bullet, PointF OriginalPosition, double Direction, byte ColorType)
			: base(StageData)
		{
			this.OriginalPosition = OriginalPosition;
			BulletPic = StageData.GlobalData.BulletPicDictionary[BPicName];
			this.Bullet = Bullet;
			this.ColorType = ColorType;
			length = (int)Math.Sqrt((double)BulletPic.Length);
			LifeTime = length * 2 + 10;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time >= LifeTime)
			{
				BulletList.ForEach(delegate(BaseBullet_Touhou x)
				{
					x.Enabled = true;
				});
			}
			if (Time % 2 == 0)
			{
				if (indexY < length)
				{
					for (int i = 0; i < length; i++)
					{
						if (BulletPic[i, indexY])
						{
							PointF pointF = new PointF(OriginalPosition.X + (float)(i - length / 2) * 5f, OriginalPosition.Y + (float)(indexY - length / 2) * 5f);
							BaseBullet_Touhou baseBullet_Touhou = new BaseBullet_Touhou(StageData, null, pointF, 0f, 0.0);
							baseBullet_Touhou.Copy(Bullet);
							baseBullet_Touhou.OriginalPosition = pointF;
							baseBullet_Touhou.Velocity = (float)baseBullet_Touhou.GetDistance(OriginalPosition) * 3f / (float)length;
							baseBullet_Touhou.DirectionDegree = (double)Ran.Next(360);
							baseBullet_Touhou.Enabled = false;
							StageData.SoundPlay("se_tan00a.wav");
						}
					}
					indexY++;
				}
			}
		}

		public override void Shoot()
		{
		}

		private bool[,] BulletPic;

		private BaseObject Bullet;

		private int indexY = 0;

		private int length;

		private byte ColorType;
	}
}
