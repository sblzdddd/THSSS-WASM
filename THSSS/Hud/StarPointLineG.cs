using System;
using System.Drawing;

namespace Shooting
{
	public class StarPointLineG : BaseObject
	{
		public StarPointLineG(StageDataPackage StageData, PointF OriginalPosition)
		{
			base.StageData = StageData;
			base.OriginalPosition = OriginalPosition;
			base.TransparentValueF = 0f;
			base.TransparentVelocity = 10f;
			this.type = "C";
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.MyPlane.OriginalPosition.X < 100f && base.MyPlane.OriginalPosition.Y > (float)(base.BoundRect.Height - 140))
			{
				base.TransparentVelocity = -10f;
			}
			else
			{
				base.TransparentVelocity = 10f;
			}
			if (base.TransparentValue < 50)
			{
				base.TransparentValueF = 50f;
			}
			base.TxtureObject = base.TextureObjectDictionary["border" + this.type + "2"];
			if (base.MyPlane.LastColor == EnchantmentType.Green && base.Story == null)
			{
				base.DestPoint = new PointF(66f, 404f);
			}
			else
			{
				base.DestPoint = new PointF(-6f, 404f);
			}
			base.Velocity = 3f;
		}

		public override void Show()
		{
			if (base.TxtureObject != null)
			{
				base.SpriteMain.Draw2D(base.TextureObjectDictionary["border" + this.type + "1"], 1f, 0f, base.OriginalPosition, Color.FromArgb(base.TransparentValue, Color.White));
				int num = base.TxtureObject.Height * base.MyPlane.StarPoint / base.MyPlane.maxStarPoint;
				num = ((num > 1) ? num : 1);
				Size size = new Size(base.TxtureObject.Width, num);
				Rectangle rectangle = new Rectangle(new Point(base.TxtureObject.PosRect.X, base.TxtureObject.PosRect.Y + base.TxtureObject.Height - num), size);
				base.SpriteMain.Draw2D(base.TxtureObject.TXTure, rectangle, size, base.TxtureObject.RotatingCenter, 0f, new PointF(base.OriginalPosition.X, base.OriginalPosition.Y + (float)base.TxtureObject.Height - (float)num), Color.FromArgb(base.TransparentValue * 8 / 10, Color.White));
				base.SpriteMain.Draw2D(base.TextureObjectDictionary["border" + this.type + "0"], 1f, 0f, base.OriginalPosition, Color.FromArgb(base.TransparentValue, Color.White));
			}
		}

		protected string type;
	}
}
