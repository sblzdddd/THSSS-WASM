using System.Drawing;

namespace Shooting
{
	internal class MusicTitle : BaseEffect
	{
		public new PointF Position
		{
			get
			{
				return new PointF(OriginalPosition.X + 36f, OriginalPosition.Y + 16f);
			}
		}

		public MusicTitle(StageDataPackage StageData, string textureName)
			: this(StageData, textureName, new Point(0, 0))
		{
			DestPoint = new Point(BoundRect.Width, BoundRect.Height - 16);
			Scale = 0.5f;
			OriginalPosition = new PointF((float)BoundRect.Width, (float)(BoundRect.Height + 100));
		}

		public MusicTitle(StageDataPackage StageData, string textureName, Point DestPoint)
			: base(StageData)
		{
			TxtureObject = TextureObjectDictionary[textureName];
			OriginalPosition = new PointF((float)(BoundRect.Width - 10), 350f);
			Velocity = 4f;
			Direction = 1.5707963267948966;
			Scale = 2f;
			this.DestPoint = DestPoint;
			LifeTime = 240;
		}

		public override void Move()
		{
			float num = (float)GetDistance(DestPoint);
			if (num > 50f)
			{
				Velocity = 7f;
			}
			else if (num > 35f)
			{
				Velocity = 5f;
			}
			else if (num > 15f)
			{
				Velocity = 3f;
			}
			else if (num > 1f)
			{
				Velocity = 1f;
			}
			else
			{
				Velocity = 0f;
			}
			Direction = GetDirection(DestPoint);
			Angle = (double)(-(double)((float)(Direction - 1.5707963267948966)));
			base.Move();
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Scale > 1f)
			{
				Scale -= 0.03f;
			}
			if (Time >= LifeTime - 35)
			{
				MaxTransparent -= 8;
			}
		}

		public override bool OutBoundary()
		{
			return Time > LifeTime;
		}

		public override void Show()
		{
			SizeF sizeF = new SizeF((float)TxtureObject.Width * Scale, (float)TxtureObject.Height * Scale);
			SpriteMain.Draw2D(TxtureObject.TXTure, TxtureObject.PosRect, sizeF, TxtureObject.RightTop, 0f, Position, Color.FromArgb(TransparentValue, Color.White));
		}
	}
}
