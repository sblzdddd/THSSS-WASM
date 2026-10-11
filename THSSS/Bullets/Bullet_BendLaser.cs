using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001ED RID: 493
	public class Bullet_BendLaser : BaseBullet_Touhou
	{
		// Token: 0x06000B3D RID: 2877 RVA: 0x0006F0EE File Offset: 0x0006D2EE
		public Bullet_BendLaser(StageDataPackage StageData)
			: base(StageData)
		{
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0006F0FA File Offset: 0x0006D2FA
		public Bullet_BendLaser(StageDataPackage StageData, PointF OriginalPosition, float Velocity, double Direction, byte ColorType, int Length)
			: this(StageData, OriginalPosition, Velocity, Direction, ColorType, Length, 16)
		{
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0006F110 File Offset: 0x0006D310
		public Bullet_BendLaser(StageDataPackage StageData, PointF OriginalPosition, float Velocity, double Direction, byte ColorType, int Length, int Width)
			: this(StageData, null, OriginalPosition, Velocity, Direction, Length)
		{
			base.ColorType = ColorType;
			ParticleBeginBullet particleBeginBullet = new ParticleBeginBullet(StageData, base.Position, ColorType);
			particleBeginBullet.Scale = 0.5f;
			particleBeginBullet.Active = true;
			this.PL = new Particle3D_BendLaser(StageData, base.GhostingCount, Width, (int)ColorType)
			{
				OriginalPosition = base.OriginalPosition,
				LastPoints = base.LastPoints
			};
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0006F18E File Offset: 0x0006D38E
		public Bullet_BendLaser(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int Length)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			base.GhostingCount = Length;
			base.Region = 2;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0006F1B4 File Offset: 0x0006D3B4
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == 1)
			{
				if (this.PL == null)
				{
					this.PL = new Particle3D_BendLaser(base.StageData, base.GhostingCount, (int)(base.ScaleLength * 16f), (int)base.ColorType)
					{
						OriginalPosition = base.OriginalPosition,
						LastPoints = base.LastPoints
					};
				}
			}
			this.PL.LastPoints = base.LastPoints;
			this.PL.TransparentValueF = base.TransparentValueF;
			if (base.Time % 8 == 0)
			{
				base.Grazed = false;
			}
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0006F274 File Offset: 0x0006D474
		public override bool OutBoundary()
		{
			bool flag;
			if (base.LifeTime != 0 && base.Time > base.LifeTime)
			{
				this.PL.Dispose();
				base.StageData.Particle3D.ParticleList.Remove(this.PL);
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
					if (base.OutsideRegion < 10)
					{
						num = 20;
					}
					else
					{
						num = base.OutsideRegion * 2;
					}
					if ((float)(-(float)num) < base.LastPoints[base.LastPoints.Length - 1].X && base.LastPoints[base.LastPoints.Length - 1].X < (float)(base.BoundRect.Width + num) && (float)(-(float)num) < base.LastPoints[base.LastPoints.Length - 1].Y && base.LastPoints[base.LastPoints.Length - 1].Y < (float)(base.BoundRect.Height + num))
					{
						flag = false;
					}
					else if ((float)(-(float)num) < base.OriginalPosition.X && base.OriginalPosition.X < (float)(base.BoundRect.Width + num) && (float)(-(float)num) < base.OriginalPosition.Y && base.OriginalPosition.Y < (float)(base.BoundRect.Height + num))
					{
						flag = false;
					}
					else
					{
						this.PL.Dispose();
						base.StageData.Particle3D.ParticleList.Remove(this.PL);
						flag = true;
					}
				}
			}
			else
			{
				if (base.OriginalPosition.X < 0f || base.OriginalPosition.X >= (float)base.BoundRect.Width)
				{
					base.Reflect -= 1;
					base.Direction -= 2.0 * (base.Direction - 1.5707963267948966);
				}
				if (base.OriginalPosition.Y < 20f)
				{
					base.Reflect -= 1;
					base.Direction = -base.Direction;
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0006F534 File Offset: 0x0006D734
		public override bool HitCheck(BaseObject Target, float Radius)
		{
			for (int i = 3; i < base.LastPoints.Length - 3; i++)
			{
				PointF pointF = base.LastPoints[i];
				double num = Math.Sqrt((double)((pointF.X - Target.OriginalPosition.X) * (pointF.X - Target.OriginalPosition.X) + (pointF.Y - Target.OriginalPosition.Y) * (pointF.Y - Target.OriginalPosition.Y)));
				if (num - (double)Target.Region < (double)Radius)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0006F600 File Offset: 0x0006D800
		public override void GiveEndEffect()
		{
			for (int i = 0; i < base.LastPoints.Length; i += 8)
			{
				new ParticleEndBullet(base.StageData, base.LastPoints[i], base.ColorType, base.SizeValue);
			}
			base.StageData.Particle3D.ParticleList.Remove(this.PL);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0006F66C File Offset: 0x0006D86C
		public override void GiveItems()
		{
			int num = 5;
			int num2 = base.Region / num;
			for (int i = 0; i < base.LastPoints.Length; i += 8)
			{
				for (int j = 0; j <= num2; j++)
				{
					double num3 = base.Direction;
					for (int k = 0; k < j * 5 + 1; k++)
					{
						PointF pointF = new PointF((float)((double)base.LastPoints[i].X + (double)j * Math.Cos(num3)), (float)((double)base.LastPoints[i].Y + (double)j * Math.Sin(num3)));
						new StarItem_Touhou(base.StageData, pointF);
						num3 += 6.283185307179586 / (double)(j * 6);
					}
				}
			}
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0006F74C File Offset: 0x0006D94C
		public override void Show()
		{
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0006F750 File Offset: 0x0006D950
		public override void ShowRegion()
		{
			PointF originalPosition = base.OriginalPosition;
			for (int i = 3; i < base.LastPoints.Length - 3; i++)
			{
				base.OriginalPosition = base.LastPoints[i];
				base.ShowRegion();
			}
			base.OriginalPosition = originalPosition;
		}

		// Token: 0x04000427 RID: 1063
		private Particle3D_BendLaser PL;
	}
}
