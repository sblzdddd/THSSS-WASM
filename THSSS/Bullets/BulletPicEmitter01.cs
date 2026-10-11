using System;
using System.Drawing;

namespace Shooting
{
	public class BulletPicEmitter01 : BaseBulleEmitter
	{
		public BulletPicEmitter01(StageDataPackage StageData, string BPicName, BaseObject Bullet, PointF OriginalPosition, double Direction, byte ColorType)
			: base(StageData)
		{
			base.OriginalPosition = OriginalPosition;
			this.BulletPic = StageData.GlobalData.BulletPicDictionary[BPicName];
			this.Bullet = Bullet;
			this.ColorType = ColorType;
			this.length = (int)Math.Sqrt((double)this.BulletPic.Length);
			base.LifeTime = this.length * 2 + 10;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time >= base.LifeTime)
			{
				base.BulletList.ForEach(delegate(BaseBullet_Touhou x)
				{
					x.Enabled = true;
				});
			}
			if (base.Time % 2 == 0)
			{
				if (this.indexY < this.length)
				{
					for (int i = 0; i < this.length; i++)
					{
						if (this.BulletPic[i, this.indexY])
						{
							PointF pointF = new PointF(base.OriginalPosition.X + (float)(i - this.length / 2) * 5f, base.OriginalPosition.Y + (float)(this.indexY - this.length / 2) * 5f);
							BaseBullet_Touhou baseBullet_Touhou = new BaseBullet_Touhou(base.StageData, null, pointF, 0f, 0.0);
							baseBullet_Touhou.Copy(this.Bullet);
							baseBullet_Touhou.OriginalPosition = pointF;
							baseBullet_Touhou.Velocity = (float)baseBullet_Touhou.GetDistance(base.OriginalPosition) * 3f / (float)this.length;
							baseBullet_Touhou.DirectionDegree = (double)base.Ran.Next(360);
							baseBullet_Touhou.Enabled = false;
							base.StageData.SoundPlay("se_tan00a.wav");
						}
					}
					this.indexY++;
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
