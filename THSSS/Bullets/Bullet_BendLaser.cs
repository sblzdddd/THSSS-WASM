using System;
using System.Drawing;

namespace Shooting
{
	public class Bullet_BendLaser : BaseBullet_Touhou
	{
		public Bullet_BendLaser(StageDataPackage StageData)
			: base(StageData)
		{
		}

		public Bullet_BendLaser(StageDataPackage StageData, PointF OriginalPosition, float Velocity, double Direction, byte ColorType, int Length)
			: this(StageData, OriginalPosition, Velocity, Direction, ColorType, Length, 16)
		{
		}

		public Bullet_BendLaser(StageDataPackage StageData, PointF OriginalPosition, float Velocity, double Direction, byte ColorType, int Length, int Width)
			: this(StageData, null, OriginalPosition, Velocity, Direction, Length)
		{
			this.ColorType = ColorType;
			var particleBeginBullet = new ParticleBeginBullet(StageData, Position, ColorType);
			particleBeginBullet.Scale = 0.5f;
			particleBeginBullet.Active = true;
			PL = new Particle3D_BendLaser(StageData, GhostingCount, Width, (int)ColorType)
			{
				OriginalPosition = this.OriginalPosition,
				LastPoints = LastPoints
			};
		}

		public Bullet_BendLaser(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int Length)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			GhostingCount = Length;
			Region = 2;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time == 1)
			{
				if (PL == null)
				{
					PL = new Particle3D_BendLaser(StageData, GhostingCount, (int)(ScaleLength * 16f), (int)ColorType)
					{
						OriginalPosition = OriginalPosition,
						LastPoints = LastPoints
					};
				}
			}
			PL.LastPoints = LastPoints;
			PL.TransparentValueF = TransparentValueF;
			if (Time % 8 == 0)
			{
				Grazed = false;
			}
		}

		public override bool OutBoundary()
		{
			bool flag;
			if (LifeTime != 0 && Time > LifeTime)
			{
				PL.Dispose();
				StageData.Particle3D.ParticleList.Remove(PL);
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
					if (OutsideRegion < 10)
					{
						num = 20;
					}
					else
					{
						num = OutsideRegion * 2;
					}
					if ((float)(-(float)num) < LastPoints[LastPoints.Length - 1].X && LastPoints[LastPoints.Length - 1].X < (float)(BoundRect.Width + num) && (float)(-(float)num) < LastPoints[LastPoints.Length - 1].Y && LastPoints[LastPoints.Length - 1].Y < (float)(BoundRect.Height + num))
					{
						flag = false;
					}
					else if ((float)(-(float)num) < OriginalPosition.X && OriginalPosition.X < (float)(BoundRect.Width + num) && (float)(-(float)num) < OriginalPosition.Y && OriginalPosition.Y < (float)(BoundRect.Height + num))
					{
						flag = false;
					}
					else
					{
						PL.Dispose();
						StageData.Particle3D.ParticleList.Remove(PL);
						flag = true;
					}
				}
			}
			else
			{
				if (OriginalPosition.X < 0f || OriginalPosition.X >= (float)BoundRect.Width)
				{
					Reflect -= 1;
					Direction -= 2.0 * (Direction - 1.5707963267948966);
				}
				if (OriginalPosition.Y < 20f)
				{
					Reflect -= 1;
					Direction = -Direction;
				}
				flag = false;
			}
			return flag;
		}

		public override bool HitCheck(BaseObject Target, float Radius)
		{
			for (var i = 3; i < LastPoints.Length - 3; i++)
			{
				var pointF = LastPoints[i];
				var num = Math.Sqrt((double)((pointF.X - Target.OriginalPosition.X) * (pointF.X - Target.OriginalPosition.X) + (pointF.Y - Target.OriginalPosition.Y) * (pointF.Y - Target.OriginalPosition.Y)));
				if (num - (double)Target.Region < (double)Radius)
				{
					return true;
				}
			}
			return false;
		}

		public override void GiveEndEffect()
		{
			for (var i = 0; i < LastPoints.Length; i += 8)
			{
				new ParticleEndBullet(StageData, LastPoints[i], ColorType, SizeValue);
			}
			StageData.Particle3D.ParticleList.Remove(PL);
		}

		public override void GiveItems()
		{
			var num = 5;
			var num2 = Region / num;
			for (var i = 0; i < LastPoints.Length; i += 8)
			{
				for (var j = 0; j <= num2; j++)
				{
					var num3 = Direction;
					for (var k = 0; k < j * 5 + 1; k++)
					{
						var pointF = new PointF((float)((double)LastPoints[i].X + (double)j * Math.Cos(num3)), (float)((double)LastPoints[i].Y + (double)j * Math.Sin(num3)));
						new StarItem_Touhou(StageData, pointF);
						num3 += 6.283185307179586 / (double)(j * 6);
					}
				}
			}
		}

		public override void Show()
		{
		}

		public override void ShowRegion()
		{
			var originalPosition = OriginalPosition;
			for (var i = 3; i < LastPoints.Length - 3; i++)
			{
				OriginalPosition = LastPoints[i];
				base.ShowRegion();
			}
			OriginalPosition = originalPosition;
		}

		private Particle3D_BendLaser PL;
	}
}
