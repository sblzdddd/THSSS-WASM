using System.Drawing;

namespace Shooting
{
	public class BackgroundMove2 : BaseObject_CS
	{
		public BackgroundMove2(StageDataPackage StageData, string textureName)
			: base(StageData, textureName, new PointF(0f, 0f), 0f, 0.0)
		{
			base.Background.BackgroundList.Add(this);
			base.OriginalPosition = new PointF((float)(base.BoundRect.Height / 2), (float)(base.BoundRect.Height / 2));
			this.Length = base.BoundRect.Height * 2;
			base.Velocity = 2f;
			base.DirectionDegree = 225.0;
			base.OutsideRegion = 10000;
		}

		public override void Ctrl()
		{
			base.Scale = (float)this.Length / (float)base.TxtureObject.Height;
			this.Width = (float)base.TxtureObject.Width * base.Scale;
			base.AngleDegree = -base.DirectionDegree + 90.0;
			base.Ctrl();
			float num = base.OriginalPosition.X;
			float num2 = base.OriginalPosition.Y;
			if (base.OriginalPosition.X <= -this.Width / 2f)
			{
				num = base.OriginalPosition.X + this.Width;
			}
			else if (base.OriginalPosition.X >= this.Width / 2f)
			{
				num = base.OriginalPosition.X - this.Width;
			}
			if (base.OriginalPosition.Y <= (float)(-(float)this.Length / 2))
			{
				num2 = base.OriginalPosition.Y + (float)this.Length;
			}
			else if (base.OriginalPosition.Y >= (float)(this.Length / 2))
			{
				num2 = base.OriginalPosition.Y - (float)this.Length;
			}
			this.PlaceAt(num, num2);
		}

		public override void Show()
		{
			float num = base.OriginalPosition.X;
			float num2 = base.OriginalPosition.Y;
			base.Show();
			num += this.Width;
			this.PlaceAt(num, num2);
			base.Show();
			num2 += (float)this.Length;
			this.PlaceAt(num, num2);
			base.Show();
			num -= this.Width;
			this.PlaceAt(num, num2);
			base.Show();
			num2 -= (float)this.Length;
			this.PlaceAt(num, num2);
		}

		public int Length;

		private float Width;
	}
}
