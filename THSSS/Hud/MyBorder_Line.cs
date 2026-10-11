using System;
using System.Drawing;

namespace Shooting
{
	public class MyBorder_Line : Life_Line
	{
		public MyBorder_Line(StageDataPackage StageData, string textureName, PointF OriginalPosition)
			: base(StageData, textureName, OriginalPosition)
		{
			this.size = 16f;
			this.maxLife = 11;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			base.Life = base.MyPlane.EnchantmentCount;
		}

		public override void Show()
		{
			SizeF sizeF = new SizeF(this.size, this.size);
			for (int i = 0; i < base.MyPlane.EnchantmentCountNeeded; i++)
			{
				TextureObject textureObject = base.TextureObjectDictionary["Border0d"];
				PointF pointF = new PointF(base.OriginalPosition.X + (float)(i * 12), base.OriginalPosition.Y);
				base.SpriteMain.Draw2D(textureObject.TXTure, textureObject.PosRect, sizeF, new PointF(0f, 0f), 0f, pointF, Color.FromArgb(base.TransparentValue, Color.White));
			}
			for (int i = 0; i < base.Life; i++)
			{
				TextureObject textureObject = base.TextureObjectDictionary["Border"];
				PointF pointF = new PointF(base.OriginalPosition.X + (float)(i * 12), base.OriginalPosition.Y);
				base.SpriteMain.Draw2D(textureObject.TXTure, textureObject.PosRect, sizeF, new PointF(0f, 0f), 0f, pointF, Color.FromArgb(base.TransparentValue, Color.White));
			}
		}
	}
}
