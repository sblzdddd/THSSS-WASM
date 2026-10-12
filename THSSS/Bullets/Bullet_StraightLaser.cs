using System;
using System.Drawing;

namespace Shooting
{
	public class Bullet_StraightLaser : BaseBullet_Touhou
	{
		public bool LaserHead { get; set; }

		private int HeadTransparent { get; set; }

		public Bullet_StraightLaser(StageDataPackage StageData)
			: base(StageData)
		{
		}

		public Bullet_StraightLaser(StageDataPackage StageData, PointF OriginalPosition, float Velocity, double Direction, byte ColorType, int Length)
			: this(StageData, OriginalPosition, Velocity, Direction, ColorType, Length, 16)
		{
		}

		public Bullet_StraightLaser(StageDataPackage StageData, PointF OriginalPosition, float Velocity, double Direction, byte ColorType, int Length, int Width)
			: this(StageData, "Laser0_" + ColorType, OriginalPosition, Velocity, Direction, Length)
		{
			this.ColorType = ColorType;
			Active = true;
			Angle = 1.5707963267948966;
			ScaleLength = (float)Width / (float)TxtureObject.Height;
		}

		public Bullet_StraightLaser(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int Length)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			GhostingCount = Length;
			Region = 2;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			ScaleWidth = (float)GetDistance(LastPoints[LastPoints.Length - 1]) / (float)TxtureObject.Width;
			if (Time == 1)
			{
				var particleBeginBullet = new ParticleBeginBullet(StageData, Position, ColorType);
				particleBeginBullet.Scale = 0.5f;
				particleBeginBullet.Active = true;
				particleBeginBullet.LifeTime = GhostingCount;
			}
			if (Time % 8 == 0)
			{
				Grazed = false;
			}
			if (LaserHead)
			{
				HeadTransparent = Ran.Next(150, 255);
			}
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
					flag = ((float)(-(float)num) >= LastPoints[LastPoints.Length - 1].X || LastPoints[LastPoints.Length - 1].X >= (float)(BoundRect.Width + num) || (float)(-(float)num) >= LastPoints[LastPoints.Length - 1].Y || LastPoints[LastPoints.Length - 1].Y >= (float)(BoundRect.Height + num)) && ((float)(-(float)num) >= OriginalPosition.X || OriginalPosition.X >= (float)(BoundRect.Width + num) || (float)(-(float)num) >= OriginalPosition.Y || OriginalPosition.Y >= (float)(BoundRect.Height + num));
				}
			}
			else
			{
				if (OriginalPosition.X < 0f || OriginalPosition.X >= (float)BoundRect.Width)
				{
					Reflect -= 1;
					var bullet_StraightLaser = new Bullet_StraightLaser(StageData, new PointF(0f, 0f), 0f, 0.0, 0, 0);
					bullet_StraightLaser.Copy(this);
					bullet_StraightLaser.Direction -= 2.0 * (Direction - 1.5707963267948966);
				}
				if (OriginalPosition.Y < 20f)
				{
					Reflect -= 1;
					var bullet_StraightLaser = new Bullet_StraightLaser(StageData, new PointF(0f, 0f), 0f, 0.0, 0, 0);
					bullet_StraightLaser.Copy(this);
					bullet_StraightLaser.Direction = -Direction;
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
				if (num - (double)((float)Target.Region * Target.Scale) < (double)(Radius * ScaleLength))
				{
					return true;
				}
			}
			return false;
		}

		public override void GiveEndEffect()
		{
			for (var i = 0; i < LastPoints.Length; i++)
			{
				if (i % 8 == 0)
				{
					new ParticleEndBullet(StageData, LastPoints[i], ColorType, SizeValue);
				}
			}
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
			if (TxtureObject != null)
			{
				var pointF = new PointF((OriginalPosition.X + LastPoints[LastPoints.Length - 1].X) / 2f, (OriginalPosition.Y + LastPoints[LastPoints.Length - 1].Y) / 2f);
				pointF = new PointF(pointF.X + (float)BoundRect.Left, pointF.Y + (float)BoundRect.Top);
				SpriteMain.Draw2D(TxtureObject, ScaleWidth, ScaleLength, (float)(Direction - 1.5707963267948966 + Angle), pointF, Color.FromArgb(TransparentValue, ColorValue), Mirrored);
				if (LaserHead)
				{
					var textureObject = TextureObjectDictionary["光点"];
					if (0 < ColorType && ColorType <= 2)
					{
						textureObject = TextureObjectDictionary["FY_Particle6_0"];
					}
					else if (2 < ColorType && ColorType <= 4)
					{
						textureObject = TextureObjectDictionary["FY_Particle4_0"];
					}
					else if (4 < ColorType && ColorType <= 8)
					{
						textureObject = TextureObjectDictionary["FY_Particle5_0"];
					}
					SpriteMain.Draw2D(textureObject, 0.4f, 0.4f, (float)(Direction - 1.5707963267948966 + Angle + (double)((float)Time / 10f)), Position, Color.FromArgb(HeadTransparent, ColorValue), Mirrored);
				}
			}
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
	}
}
