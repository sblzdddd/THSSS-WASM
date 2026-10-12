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
			ScaleLength = 0.1f;
			DesScaleLength = 3f;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (ScaleLength <= DesScaleLength)
			{
				ScaleLength += 0.02f;
			}
		}

		public override bool HitCheck(BaseObject MyPlane)
		{
			var distance = GetDistance(MyPlane);
			var direction = GetDirection(MyPlane);
			var num = distance * Math.Cos((double)((float)(Direction - direction)));
			var num2 = distance * Math.Sin((double)((float)(Direction - direction)));
			var num3 = (double)((float)Region * ScaleWidth);
			var num4 = (double)((float)TxtureObject.Height * ScaleLength * 0.45f);
			return num2 * num2 / num3 / num3 + (num - (double)((float)TxtureObject.Height * ScaleLength / 2f)) * (num - (double)((float)TxtureObject.Height * ScaleLength / 2f)) / num4 / num4 < 1.0;
		}

		public override void GiveItems()
		{
		}

		public override void Show()
		{
			if (TxtureObject != null)
			{
				var sizeF = new SizeF((float)TxtureObject.Width * ScaleWidth, (float)TxtureObject.Height * ScaleLength);
				SpriteMain.Draw2D(TxtureObject.TXTure, TxtureObject.PosRect, sizeF, new PointF((float)(TxtureObject.Width / 2), 0f), (float)(Direction - 1.5707963267948966 + Angle), Position, Color.FromArgb(TransparentValue, ColorValue));
			}
		}

		public override void ShowRegion()
		{
			var originalPosition = OriginalPosition;
			OriginalPosition = new PointF(OriginalPosition.X + (float)TxtureObject.Height * ScaleLength * 0.5f * (float)Math.Cos(Direction), OriginalPosition.Y + (float)TxtureObject.Height * ScaleLength * 0.5f * (float)Math.Sin(Direction));
			SpriteMain.Draw2D(TextureObjectDictionary["Region"], 2f * (float)Region / (float)TextureObjectDictionary["Region"].Width * ScaleWidth, (float)TxtureObject.Height * 0.9f / (float)TextureObjectDictionary["Region"].Height * ScaleLength, (float)(Direction - 1.5707963267948966 + Angle), Position, Color.White, Mirrored);
			OriginalPosition = originalPosition;
		}
	}
}
