using System;
using System.Drawing;

namespace Shooting
{
	public class BackgroundMove : BaseObject_CS
	{
		public BackgroundMove(StageDataPackage StageData, string textureName)
			: base(StageData, textureName, new PointF(0f, 0f), 0f, 0.0)
		{
			base.Background.BackgroundList.Add(this);
			base.OriginalPosition = new PointF((float)(base.BoundRect.Height / 2), (float)(base.BoundRect.Height / 2));
			base.Scale = (float)base.BoundRect.Height / (float)base.TxtureObject.Height;
			base.Velocity = 2f;
			base.DirectionDegree = 225.0;
			base.OutsideRegion = 1000;
		}

		public override void Ctrl()
		{
			base.AngleDegree = -base.DirectionDegree + 90.0;
			base.Ctrl();
			float num = base.OriginalPosition.X;
			float num2 = base.OriginalPosition.Y;
			if (base.OriginalPosition.X <= (float)(-(float)base.BoundRect.Height / 2))
			{
				num = base.OriginalPosition.X + (float)base.BoundRect.Height;
			}
			else if (base.OriginalPosition.X >= (float)(base.BoundRect.Height / 2))
			{
				num = base.OriginalPosition.X - (float)base.BoundRect.Height;
			}
			if (base.OriginalPosition.Y <= (float)(-(float)base.BoundRect.Height / 2))
			{
				num2 = base.OriginalPosition.Y + (float)base.BoundRect.Height;
			}
			else if (base.OriginalPosition.Y >= (float)(base.BoundRect.Height / 2))
			{
				num2 = base.OriginalPosition.Y - (float)base.BoundRect.Height;
			}
			this.PlaceAt(num, num2);
		}

		public override void Show()
		{
			float num = base.OriginalPosition.X;
			float num2 = base.OriginalPosition.Y;
			base.Show();
			num += (float)base.BoundRect.Height;
			this.PlaceAt(num, num2);
			base.Show();
			num2 += (float)base.BoundRect.Height;
			this.PlaceAt(num, num2);
			base.Show();
			num -= (float)base.BoundRect.Height;
			this.PlaceAt(num, num2);
			base.Show();
			num2 -= (float)base.BoundRect.Height;
			this.PlaceAt(num, num2);
		}
	}
}
