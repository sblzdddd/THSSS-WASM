using System;
using System.Drawing;

namespace Shooting
{
	public class BaseMenuItem : BaseObject
	{
		public bool Selected { get; set; }

		public string Name { get; set; }

		public bool OnRemove { get; set; }

		public int TwinkleTime { get; set; }

		public int VibrateTime { get; set; }

		public float OffsetX { get; set; }

		public float OffsetY { get; set; }

		public bool Vibrateable { get; set; }

		public bool Twinkleable { get; set; }

		public bool UnSelectVisible { get; set; }

		public BaseMenuItem()
		{
		}

		public BaseMenuItem(StageDataPackage StageData, string textureName)
		{
			base.StageData = StageData;
			base.TxtureObject = base.TextureObjectDictionary[textureName];
			this.Name = textureName;
			base.MaxVelocity = 10f;
			this.UnSelectVisible = true;
			this.Vibrateable = true;
			this.Twinkleable = true;
			this.Selected = false;
		}

		public override void Move()
		{
			float num = (float)base.GetDistance(base.DestPoint);
			if (num > 0f)
			{
				base.Velocity = (float)Math.Sqrt((double)num) / 1f;
			}
			else
			{
				base.Velocity = 0f;
			}
			base.Direction = base.GetDirection(base.DestPoint);
			base.Move();
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (this.Selected)
			{
				double num = 1.0 + Math.Sin((double)((float)base.Time / 5f));
				if (!this.Twinkleable)
				{
					num = 2.0;
				}
				base.ColorValue = Color.FromArgb(220 + (int)(12.0 * num), 220 + (int)(12.0 * num), 220 + (int)(12.0 * num));
				base.TransparentValueF = 255f;
			}
			else if (!this.UnSelectVisible)
			{
				base.TransparentVelocity = -25f;
			}
			else
			{
				base.ColorValue = Color.FromArgb(150, 150, 150);
				base.TransparentValueF = 255f;
			}
			if (base.Time < this.VibrateTime)
			{
				this.OffsetX = (float)base.Ran.Next(-2, 3);
				this.OffsetY = (float)base.Ran.Next(-2, 3);
			}
			else
			{
				this.OffsetX = 0f;
				this.OffsetY = 0f;
			}
			if (base.Time < this.TwinkleTime)
			{
				if (base.Time % 4 == 0)
				{
					base.TransparentValueF = 255f;
				}
				else if (base.Time % 4 == 2)
				{
					base.TransparentValueF = 0f;
				}
			}
			if (this.OnRemove)
			{
				base.MaxTransparent -= 15;
			}
		}

		public virtual void Select()
		{
			base.Time = 0;
			this.Selected = true;
			if (this.Vibrateable)
			{
				this.VibrateTime = base.Time + 10;
			}
			this.TwinkleTime = 0;
		}

		public virtual void Click()
		{
			base.Time = 0;
			this.VibrateTime = 0;
			this.TwinkleTime = base.Time + 30;
		}

		public override void Show()
		{
			SizeF sizeF = new SizeF((float)base.TxtureObject.Width * base.Scale, (float)base.TxtureObject.Height * base.Scale);
			base.SpriteMain.Draw2D(base.TxtureObject.TXTure, base.TxtureObject.PosRect, sizeF, new PointF(0f, 0f), (float)base.Angle, new PointF(base.Position.X - this.OffsetX, base.Position.Y - this.OffsetY), Color.FromArgb(base.TransparentValue, base.ColorValue));
		}
	}
}
