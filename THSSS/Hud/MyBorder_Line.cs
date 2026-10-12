using System.Drawing;

namespace Shooting
{
	public class MyBorder_Line : Life_Line
	{
		public MyBorder_Line(StageDataPackage StageData, string textureName, PointF OriginalPosition)
			: base(StageData, textureName, OriginalPosition)
		{
			size = 16f;
			maxLife = 11;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			Life = MyPlane.EnchantmentCount;
		}

		public override void Show()
		{
			SizeF sizeF = new SizeF(size, size);
			for (int i = 0; i < MyPlane.EnchantmentCountNeeded; i++)
			{
				TextureObject textureObject = TextureObjectDictionary["Border0d"];
				PointF pointF = new PointF(OriginalPosition.X + (float)(i * 12), OriginalPosition.Y);
				SpriteMain.Draw2D(textureObject.TXTure, textureObject.PosRect, sizeF, new PointF(0f, 0f), 0f, pointF, Color.FromArgb(TransparentValue, Color.White));
			}
			for (int i = 0; i < Life; i++)
			{
				TextureObject textureObject = TextureObjectDictionary["Border"];
				PointF pointF = new PointF(OriginalPosition.X + (float)(i * 12), OriginalPosition.Y);
				SpriteMain.Draw2D(textureObject.TXTure, textureObject.PosRect, sizeF, new PointF(0f, 0f), 0f, pointF, Color.FromArgb(TransparentValue, Color.White));
			}
		}
	}
}
