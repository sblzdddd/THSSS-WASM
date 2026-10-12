using System.Drawing;

namespace Shooting
{
	public class BackgroundMove : BaseObject_CS
	{
		public BackgroundMove(StageDataPackage StageData, string textureName)
			: base(StageData, textureName, new PointF(0f, 0f), 0f, 0.0)
		{
			Background.BackgroundList.Add(this);
			OriginalPosition = new PointF((float)(BoundRect.Height / 2), (float)(BoundRect.Height / 2));
			Scale = (float)BoundRect.Height / (float)TxtureObject.Height;
			Velocity = 2f;
			DirectionDegree = 225.0;
			OutsideRegion = 1000;
		}

		public override void Ctrl()
		{
			AngleDegree = -DirectionDegree + 90.0;
			base.Ctrl();
			var num = OriginalPosition.X;
			var num2 = OriginalPosition.Y;
			if (OriginalPosition.X <= (float)(-(float)BoundRect.Height / 2))
			{
				num = OriginalPosition.X + (float)BoundRect.Height;
			}
			else if (OriginalPosition.X >= (float)(BoundRect.Height / 2))
			{
				num = OriginalPosition.X - (float)BoundRect.Height;
			}
			if (OriginalPosition.Y <= (float)(-(float)BoundRect.Height / 2))
			{
				num2 = OriginalPosition.Y + (float)BoundRect.Height;
			}
			else if (OriginalPosition.Y >= (float)(BoundRect.Height / 2))
			{
				num2 = OriginalPosition.Y - (float)BoundRect.Height;
			}
			PlaceAt(num, num2);
		}

		public override void Show()
		{
			var num = OriginalPosition.X;
			var num2 = OriginalPosition.Y;
			base.Show();
			num += (float)BoundRect.Height;
			PlaceAt(num, num2);
			base.Show();
			num2 += (float)BoundRect.Height;
			PlaceAt(num, num2);
			base.Show();
			num -= (float)BoundRect.Height;
			PlaceAt(num, num2);
			base.Show();
			num2 -= (float)BoundRect.Height;
			PlaceAt(num, num2);
		}
	}
}
