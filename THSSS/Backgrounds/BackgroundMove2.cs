using System.Drawing;

namespace Shooting
{
	public class BackgroundMove2 : BaseObject_CS
	{
		public BackgroundMove2(StageDataPackage StageData, string textureName)
			: base(StageData, textureName, new PointF(0f, 0f), 0f, 0.0)
		{
			Background.BackgroundList.Add(this);
			OriginalPosition = new PointF((float)(BoundRect.Height / 2), (float)(BoundRect.Height / 2));
			Length = BoundRect.Height * 2;
			Velocity = 2f;
			DirectionDegree = 225.0;
			OutsideRegion = 10000;
		}

		public override void Ctrl()
		{
			Scale = (float)Length / (float)TxtureObject.Height;
			Width = (float)TxtureObject.Width * Scale;
			AngleDegree = -DirectionDegree + 90.0;
			base.Ctrl();
			float num = OriginalPosition.X;
			float num2 = OriginalPosition.Y;
			if (OriginalPosition.X <= -Width / 2f)
			{
				num = OriginalPosition.X + Width;
			}
			else if (OriginalPosition.X >= Width / 2f)
			{
				num = OriginalPosition.X - Width;
			}
			if (OriginalPosition.Y <= (float)(-(float)Length / 2))
			{
				num2 = OriginalPosition.Y + (float)Length;
			}
			else if (OriginalPosition.Y >= (float)(Length / 2))
			{
				num2 = OriginalPosition.Y - (float)Length;
			}
			PlaceAt(num, num2);
		}

		public override void Show()
		{
			float num = OriginalPosition.X;
			float num2 = OriginalPosition.Y;
			base.Show();
			num += Width;
			PlaceAt(num, num2);
			base.Show();
			num2 += (float)Length;
			PlaceAt(num, num2);
			base.Show();
			num -= Width;
			PlaceAt(num, num2);
			base.Show();
			num2 -= (float)Length;
			PlaceAt(num, num2);
		}

		public int Length;

		private float Width;
	}
}
