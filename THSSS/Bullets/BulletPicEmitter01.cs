using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001EE RID: 494
	public class BulletPicEmitter01 : BaseBulleEmitter
	{
		// Token: 0x06000B48 RID: 2888 RVA: 0x0006F7A8 File Offset: 0x0006D9A8
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

		// Token: 0x06000B49 RID: 2889 RVA: 0x0006F82C File Offset: 0x0006DA2C
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

		// Token: 0x06000B4A RID: 2890 RVA: 0x0006F9C9 File Offset: 0x0006DBC9
		public override void Shoot()
		{
		}

		// Token: 0x04000428 RID: 1064
		private bool[,] BulletPic;

		// Token: 0x04000429 RID: 1065
		private BaseObject Bullet;

		// Token: 0x0400042A RID: 1066
		private int indexY = 0;

		// Token: 0x0400042B RID: 1067
		private int length;

		// Token: 0x0400042C RID: 1068
		private byte ColorType;
	}
}
