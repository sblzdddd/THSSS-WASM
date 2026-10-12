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
			ItemList.Add(this);
			Velocity = (float)Ran.Next(1, 6);
			Direction = (double)((float)Ran.Next(180, 360) / 360f) * 3.141592653589793 * 2.0;
			Angle = 0.0;
			if ((float)Direction - 1.5707964f > 0f)
			{
				AngularVelocity = -0.2f;
			}
			else
			{
				AngularVelocity = 0.2f;
			}
		}

		public override void HitCheckAll()
		{
			if (HitCheck(MyPlane, 200f))
			{
				Obtain = true;
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			Angle += (double)AngularVelocity;
			if (Time == 12)
			{
				Direction = 1.5707963267948966;
				AngularVelocity = 0f;
				Angle = 0.0;
			}
			else if (Time > 12)
			{
				if (Obtain)
				{
					Direction = 3.141592653589793 + Math.Atan2((double)(Position.Y - MyPlane.Position.Y), (double)(Position.X - MyPlane.Position.X));
					Velocity = 8f;
				}
				else
				{
					Velocity = 4f;
				}
			}
			if (OutBoundary())
			{
				ItemList.Remove(this);
			}
		}

		public override bool OutBoundary()
		{
			if (OriginalPosition.X < 0f)
			{
				OriginalPosition = new PointF(0f, OriginalPosition.Y);
			}
			else if (OriginalPosition.X > (float)BoundRect.Width)
			{
				OriginalPosition = new PointF((float)BoundRect.Width, OriginalPosition.Y);
			}
			return Time >= 30 && Position.Y > (float)(BoundRect.Bottom + 30);
		}
	}
}
