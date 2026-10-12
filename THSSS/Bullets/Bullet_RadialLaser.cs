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
			this.ColorType = ColorType;
			Active = true;
			Angle = 1.5707963267948966;
			MaxScaleL = (float)Width / (float)TxtureObject.Height;
		}

		public Bullet_RadialLaser(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int Length)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			UnRemoveable = true;
			Region = 2;
			ScaleVW = 0.5f;
			MaxScaleW = (float)Length / (float)TxtureObject.Width;
			ScaleWidth = MaxScaleW;
			MaxScaleL = 1f;
			ScaleLength = 0.2f;
			UnmatchedTime = 60;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time == 1)
			{
				beginEffect = new ParticleBeginBullet(StageData, Position, ColorType)
				{
					Scale = MaxScaleW * 0.5f,
					Active = true,
					LifeTime = LifeTime
				};
				beginEffect.SetBinding(this);
			}
			if (Time == UnmatchedTime)
			{
				ScaleVL = 0.1f;
			}
			if (Time == LifeTime - 8)
			{
				TransparentVelocity = (float)(-(float)MaxTransparent / 8);
				ScaleVL = -ScaleLength / 8f;
			}
			if (Time % 8 == 0)
			{
				Grazed = false;
			}
		}

		public override bool OutBoundary()
		{
			return LifeTime != 0 && Time > LifeTime;
		}

		public override bool HitCheck(BaseObject Target, float Radius)
		{
			bool flag;
			if (Time < UnmatchedTime)
			{
				flag = false;
			}
			else
			{
				double distance = GetDistance(Target);
				double direction = GetDirection(Target);
				double num = distance * Math.Cos((double)((float)(Direction - direction)));
				double num2 = distance * Math.Sin((double)((float)(Direction - direction)));
				double num3 = (double)(Radius * ScaleLength + (float)Target.Region * Target.Scale);
				double num4 = (double)(((float)TxtureObject.Width + Radius) * ScaleWidth * 0.45f + (float)Target.Region * Target.Scale);
				flag = num2 * num2 / num3 / num3 + (num - (double)((float)TxtureObject.Width * ScaleWidth / 2f)) * (num - (double)((float)TxtureObject.Width * ScaleWidth / 2f)) / num4 / num4 < 1.0;
			}
			return flag;
		}

		public override void GiveEndEffect()
		{
			beginEffect.LifeTime = beginEffect.Time + 1;
		}

		public override void Show()
		{
			if (TxtureObject != null)
			{
				SizeF sizeF = new SizeF((float)TxtureObject.Width * ScaleWidth, (float)TxtureObject.Height * ScaleLength);
				SpriteMain.Draw2D(TxtureObject.TXTure, TxtureObject.PosRect, sizeF, new PointF(0f, (float)(TxtureObject.Height / 2)), (float)(Direction - 1.5707963267948966 + Angle), Position, Color.FromArgb(TransparentValue, ColorValue));
			}
		}

		public override void ShowRegion()
		{
			PointF originalPosition = OriginalPosition;
			OriginalPosition = new PointF(OriginalPosition.X + (float)TxtureObject.Width * ScaleWidth * 0.5f * (float)Math.Cos(Direction), OriginalPosition.Y + (float)TxtureObject.Width * ScaleWidth * 0.5f * (float)Math.Sin(Direction));
			SpriteMain.Draw2D(TextureObjectDictionary["Region"], (float)TxtureObject.Width * 0.9f / (float)TextureObjectDictionary["Region"].Height * ScaleWidth, 2f * (float)Region / (float)TextureObjectDictionary["Region"].Width * ScaleLength, (float)(Direction - 1.5707963267948966 + Angle), Position, Color.Red, Mirrored);
			OriginalPosition = originalPosition;
		}

		private BaseEffect beginEffect;
	}
}
