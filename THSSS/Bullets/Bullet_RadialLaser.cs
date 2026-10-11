using System;
using System.Drawing;

namespace Shooting
{
	public class Bullet_RadialLaser : BaseBullet_Touhou
	{
		public int UnmatchedTime { get; set; }

		public Bullet_RadialLaser(StageDataPackage StageData)
			: base(StageData)
		{
		}

		public Bullet_RadialLaser(StageDataPackage StageData, PointF OriginalPosition, float Velocity, double Direction, byte ColorType, int Length)
			: this(StageData, OriginalPosition, Velocity, Direction, ColorType, Length, 16)
		{
		}

		public Bullet_RadialLaser(StageDataPackage StageData, PointF OriginalPosition, float Velocity, double Direction, byte ColorType, int Length, int Width)
			: this(StageData, "Laser0_" + ColorType, OriginalPosition, Velocity, Direction, Length)
		{
			base.ColorType = ColorType;
			base.Active = true;
			base.Angle = 1.5707963267948966;
			base.MaxScaleL = (float)Width / (float)base.TxtureObject.Height;
		}

		public Bullet_RadialLaser(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int Length)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			base.UnRemoveable = true;
			base.Region = 2;
			base.ScaleVW = 0.5f;
			base.MaxScaleW = (float)Length / (float)base.TxtureObject.Width;
			base.ScaleWidth = base.MaxScaleW;
			base.MaxScaleL = 1f;
			base.ScaleLength = 0.2f;
			this.UnmatchedTime = 60;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == 1)
			{
				this.beginEffect = new ParticleBeginBullet(base.StageData, base.Position, base.ColorType)
				{
					Scale = base.MaxScaleW * 0.5f,
					Active = true,
					LifeTime = base.LifeTime
				};
				this.beginEffect.SetBinding(this);
			}
			if (base.Time == this.UnmatchedTime)
			{
				base.ScaleVL = 0.1f;
			}
			if (base.Time == base.LifeTime - 8)
			{
				base.TransparentVelocity = (float)(-(float)base.MaxTransparent / 8);
				base.ScaleVL = -base.ScaleLength / 8f;
			}
			if (base.Time % 8 == 0)
			{
				base.Grazed = false;
			}
		}

		public override bool OutBoundary()
		{
			return base.LifeTime != 0 && base.Time > base.LifeTime;
		}

		public override bool HitCheck(BaseObject Target, float Radius)
		{
			bool flag;
			if (base.Time < this.UnmatchedTime)
			{
				flag = false;
			}
			else
			{
				double distance = base.GetDistance(Target);
				double direction = base.GetDirection(Target);
				double num = distance * Math.Cos((double)((float)(base.Direction - direction)));
				double num2 = distance * Math.Sin((double)((float)(base.Direction - direction)));
				double num3 = (double)(Radius * base.ScaleLength + (float)Target.Region * Target.Scale);
				double num4 = (double)(((float)base.TxtureObject.Width + Radius) * base.ScaleWidth * 0.45f + (float)Target.Region * Target.Scale);
				flag = num2 * num2 / num3 / num3 + (num - (double)((float)base.TxtureObject.Width * base.ScaleWidth / 2f)) * (num - (double)((float)base.TxtureObject.Width * base.ScaleWidth / 2f)) / num4 / num4 < 1.0;
			}
			return flag;
		}

		public override void GiveEndEffect()
		{
			this.beginEffect.LifeTime = this.beginEffect.Time + 1;
		}

		public override void Show()
		{
			if (base.TxtureObject != null)
			{
				SizeF sizeF = new SizeF((float)base.TxtureObject.Width * base.ScaleWidth, (float)base.TxtureObject.Height * base.ScaleLength);
				base.SpriteMain.Draw2D(base.TxtureObject.TXTure, base.TxtureObject.PosRect, sizeF, new PointF(0f, (float)(base.TxtureObject.Height / 2)), (float)(base.Direction - 1.5707963267948966 + base.Angle), base.Position, Color.FromArgb(base.TransparentValue, base.ColorValue));
			}
		}

		public override void ShowRegion()
		{
			PointF originalPosition = base.OriginalPosition;
			base.OriginalPosition = new PointF(base.OriginalPosition.X + (float)base.TxtureObject.Width * base.ScaleWidth * 0.5f * (float)Math.Cos(base.Direction), base.OriginalPosition.Y + (float)base.TxtureObject.Width * base.ScaleWidth * 0.5f * (float)Math.Sin(base.Direction));
			base.SpriteMain.Draw2D(base.TextureObjectDictionary["Region"], (float)base.TxtureObject.Width * 0.9f / (float)base.TextureObjectDictionary["Region"].Height * base.ScaleWidth, 2f * (float)base.Region / (float)base.TextureObjectDictionary["Region"].Width * base.ScaleLength, (float)(base.Direction - 1.5707963267948966 + base.Angle), base.Position, Color.Red, base.Mirrored);
			base.OriginalPosition = originalPosition;
		}

		private BaseEffect beginEffect;
	}
}
