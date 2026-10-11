using System;
using System.Drawing;

namespace Shooting
{
	public class BaseItem : BaseObject
	{
		public float AngularVelocity { get; set; }

		public bool Obtain { get; set; }

		public bool Doubled { get; set; }

		public BaseItem(StageDataPackage StageData, string textureName, PointF Position)
			: base(StageData, textureName, Position, 0f, 0.0)
		{
			base.ItemList.Add(this);
			base.Velocity = (float)base.Ran.Next(1, 6);
			base.Direction = (double)((float)base.Ran.Next(180, 360) / 360f) * 3.141592653589793 * 2.0;
			base.Angle = 0.0;
			if ((float)base.Direction - 1.5707964f > 0f)
			{
				this.AngularVelocity = -0.2f;
			}
			else
			{
				this.AngularVelocity = 0.2f;
			}
		}

		public override void HitCheckAll()
		{
			if (this.HitCheck(base.MyPlane, 200f))
			{
				this.Obtain = true;
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			base.Angle += (double)this.AngularVelocity;
			if (base.Time == 12)
			{
				base.Direction = 1.5707963267948966;
				this.AngularVelocity = 0f;
				base.Angle = 0.0;
			}
			else if (base.Time > 12)
			{
				if (this.Obtain)
				{
					base.Direction = 3.141592653589793 + Math.Atan2((double)(base.Position.Y - base.MyPlane.Position.Y), (double)(base.Position.X - base.MyPlane.Position.X));
					base.Velocity = 8f;
				}
				else
				{
					base.Velocity = 4f;
				}
			}
			if (this.OutBoundary())
			{
				base.ItemList.Remove(this);
			}
		}

		public override bool OutBoundary()
		{
			if (base.OriginalPosition.X < 0f)
			{
				base.OriginalPosition = new PointF(0f, base.OriginalPosition.Y);
			}
			else if (base.OriginalPosition.X > (float)base.BoundRect.Width)
			{
				base.OriginalPosition = new PointF((float)base.BoundRect.Width, base.OriginalPosition.Y);
			}
			return base.Time >= 30 && base.Position.Y > (float)(base.BoundRect.Bottom + 30);
		}
	}
}
