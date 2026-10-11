using System;
using System.Drawing;

namespace Shooting
{
	internal class BulletLaser : BaseBullet
	{
		public float DesScaleWidth { get; set; }

		public float DesScaleLength { get; set; }

		public BulletLaser(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			base.ScaleLength = 0.1f;
			this.DesScaleLength = 3f;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.ScaleLength <= this.DesScaleLength)
			{
				base.ScaleLength += 0.02f;
			}
		}

		public override bool HitCheck(BaseObject MyPlane)
		{
			double distance = base.GetDistance(MyPlane);
			double direction = base.GetDirection(MyPlane);
			double num = distance * Math.Cos((double)((float)(base.Direction - direction)));
			double num2 = distance * Math.Sin((double)((float)(base.Direction - direction)));
			double num3 = (double)((float)base.Region * base.ScaleWidth);
			double num4 = (double)((float)base.TxtureObject.Height * base.ScaleLength * 0.45f);
			return num2 * num2 / num3 / num3 + (num - (double)((float)base.TxtureObject.Height * base.ScaleLength / 2f)) * (num - (double)((float)base.TxtureObject.Height * base.ScaleLength / 2f)) / num4 / num4 < 1.0;
		}

		public override void GiveItems()
		{
		}

		public override void Show()
		{
			if (base.TxtureObject != null)
			{
				SizeF sizeF = new SizeF((float)base.TxtureObject.Width * base.ScaleWidth, (float)base.TxtureObject.Height * base.ScaleLength);
				base.SpriteMain.Draw2D(base.TxtureObject.TXTure, base.TxtureObject.PosRect, sizeF, new PointF((float)(base.TxtureObject.Width / 2), 0f), (float)(base.Direction - 1.5707963267948966 + base.Angle), base.Position, Color.FromArgb(base.TransparentValue, base.ColorValue));
			}
		}

		public override void ShowRegion()
		{
			PointF originalPosition = base.OriginalPosition;
			base.OriginalPosition = new PointF(base.OriginalPosition.X + (float)base.TxtureObject.Height * base.ScaleLength * 0.5f * (float)Math.Cos(base.Direction), base.OriginalPosition.Y + (float)base.TxtureObject.Height * base.ScaleLength * 0.5f * (float)Math.Sin(base.Direction));
			base.SpriteMain.Draw2D(base.TextureObjectDictionary["Region"], 2f * (float)base.Region / (float)base.TextureObjectDictionary["Region"].Width * base.ScaleWidth, (float)base.TxtureObject.Height * 0.9f / (float)base.TextureObjectDictionary["Region"].Height * base.ScaleLength, (float)(base.Direction - 1.5707963267948966 + base.Angle), base.Position, Color.White, base.Mirrored);
			base.OriginalPosition = originalPosition;
		}
	}
}
