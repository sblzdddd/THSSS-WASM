using System;
using System.Drawing;

namespace Shooting
{
	public class CharacterMenuItem : BaseMenuItem
	{
		public PointF DestPoint1 { get; set; }

		public PointF DestPoint2 { get; set; }

		public CharacterMenuItem(StageDataPackage StageData, string textureName)
			: base(StageData, textureName)
		{
			base.DestPoint = this.DestPoint1;
		}

		public override void Ctrl()
		{
			if (base.Enabled)
			{
				base.Time++;
				this.Move();
				base.Velocity += base.Accelerate;
				base.TransparentValueF += base.TransparentVelocity;
				if (base.Time == 1)
				{
					base.OriginalPosition = this.DestPoint2;
				}
				if (base.Selected)
				{
					double num = 1.0 + Math.Sin((double)((float)base.Time / 6f));
					base.ColorValue = Color.White;
					base.TransparentVelocity = 25f;
					base.DestPoint = this.DestPoint1;
				}
				else
				{
					base.ColorValue = Color.Gray;
					base.TransparentVelocity = -25f;
					base.DestPoint = this.DestPoint2;
				}
				if (base.Time < base.TwinkleTime)
				{
					if (base.Time % 4 == 0)
					{
						base.ColorValue = Color.White;
					}
					else if (base.Time % 4 == 2)
					{
						base.ColorValue = Color.Gray;
					}
				}
				if (base.OnRemove)
				{
					base.MaxTransparent -= 15;
				}
			}
		}

		public override void Select()
		{
			base.Time = 1;
			base.Selected = true;
			base.VibrateTime = 0;
			base.TwinkleTime = 0;
		}

		public override void Click()
		{
			base.Time = 1;
			base.VibrateTime = 0;
			base.TwinkleTime = base.Time + 30;
		}
	}
}
