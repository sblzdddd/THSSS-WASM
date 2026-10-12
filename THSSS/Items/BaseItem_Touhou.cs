using System;
using System.Drawing;

namespace Shooting
{
	public class BaseItem_Touhou : BaseItem
	{
		public BaseItem_Touhou(StageDataPackage StageData, string textureName, PointF OriginalPosition)
			: base(StageData, textureName, OriginalPosition)
		{
			this.OriginalPosition = OriginalPosition;
			Velocity = 4f;
			Direction = 4.71238898038469;
			Angle = 0.0;
			Accelerate = -Velocity / 40f;
			AngularVelocity = 0.2f;
		}

		public override void Ctrl()
		{
			if (Enabled)
			{
				Time++;
				Move();
				Velocity += Accelerate;
				if (Velocity < 0f)
				{
					Velocity = 0f;
				}
				Angle += (double)AngularVelocity;
				TransparentValueF += TransparentVelocity;
				if (Obtain)
				{
					Direction = 3.141592653589793 + Math.Atan2((double)(Position.Y - MyPlane.Position.Y), (double)(Position.X - MyPlane.Position.X));
					Accelerate = 1f;
					if (Velocity > 10f)
					{
						Velocity = 10f;
					}
				}
				else if (Time == 40)
				{
					AngularVelocity = 0f;
					Angle = 0.0;
				}
				else if (Time > 40)
				{
					Direction = 1.5707963267948966;
					Accelerate = 0.15f;
					if (Velocity > 1.5f)
					{
						Velocity = 1.5f;
					}
				}
				if (OriginalPosition.Y < 0f)
				{
					TransparentValueF = (float)(255 + 2 * (int)OriginalPosition.Y);
				}
				else
				{
					TransparentValueF = 255f;
				}
				if (OutBoundary())
				{
					ItemList.Remove(this);
				}
			}
		}

		public override void HitCheckAll()
		{
			if (Time >= 10)
			{
				if (HitCheck(MyPlane, 48f))
				{
					Obtain = true;
				}
				if (MyPlane.OriginalPosition.Y < 128f)
				{
					Obtain = true;
					Doubled = true;
				}
			}
		}
	}
}
