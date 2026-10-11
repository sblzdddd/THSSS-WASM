using System;
using System.Drawing;

namespace Shooting
{
	public class BaseItem_Touhou : BaseItem
	{
		public BaseItem_Touhou(StageDataPackage StageData, string textureName, PointF OriginalPosition)
			: base(StageData, textureName, OriginalPosition)
		{
			base.OriginalPosition = OriginalPosition;
			base.Velocity = 4f;
			base.Direction = 4.71238898038469;
			base.Angle = 0.0;
			base.Accelerate = -base.Velocity / 40f;
			base.AngularVelocity = 0.2f;
		}

		public override void Ctrl()
		{
			if (base.Enabled)
			{
				base.Time++;
				this.Move();
				base.Velocity += base.Accelerate;
				if (base.Velocity < 0f)
				{
					base.Velocity = 0f;
				}
				base.Angle += (double)base.AngularVelocity;
				base.TransparentValueF += base.TransparentVelocity;
				if (base.Obtain)
				{
					base.Direction = 3.141592653589793 + Math.Atan2((double)(base.Position.Y - base.MyPlane.Position.Y), (double)(base.Position.X - base.MyPlane.Position.X));
					base.Accelerate = 1f;
					if (base.Velocity > 10f)
					{
						base.Velocity = 10f;
					}
				}
				else if (base.Time == 40)
				{
					base.AngularVelocity = 0f;
					base.Angle = 0.0;
				}
				else if (base.Time > 40)
				{
					base.Direction = 1.5707963267948966;
					base.Accelerate = 0.15f;
					if (base.Velocity > 1.5f)
					{
						base.Velocity = 1.5f;
					}
				}
				if (base.OriginalPosition.Y < 0f)
				{
					base.TransparentValueF = (float)(255 + 2 * (int)base.OriginalPosition.Y);
				}
				else
				{
					base.TransparentValueF = 255f;
				}
				if (this.OutBoundary())
				{
					base.ItemList.Remove(this);
				}
			}
		}

		public override void HitCheckAll()
		{
			if (base.Time >= 10)
			{
				if (this.HitCheck(base.MyPlane, 48f))
				{
					base.Obtain = true;
				}
				if (base.MyPlane.OriginalPosition.Y < 128f)
				{
					base.Obtain = true;
					base.Doubled = true;
				}
			}
		}
	}
}
