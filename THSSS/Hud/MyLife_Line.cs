using System.Drawing;

namespace Shooting
{
	public class MyLife_Line : Life_Line
	{
		public MyLife_Line(StageDataPackage StageData, string textureName, PointF OriginalPosition)
			: base(StageData, textureName, OriginalPosition)
		{
			size = 16f;
		}

		public MyLife_Line(StageDataPackage StageData, TextureObject TxtureObject, PointF OriginalPosition)
			: base(StageData, TxtureObject, OriginalPosition)
		{
			size = 16f;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			Life = MyPlane.Life;
		}

		public override void Show()
		{
			var sizeF = new SizeF(size, size);
			for (var i = 0; i < 8; i++)
			{
				var textureObject = TextureObjectDictionary["Life0d"];
				var pointF = new PointF(OriginalPosition.X + (float)i * size, OriginalPosition.Y);
				SpriteMain.Draw2D(textureObject.TXTure, textureObject.PosRect, sizeF, new PointF(0f, 0f), 0f, pointF, Color.FromArgb(TransparentValue, Color.White));
			}
			for (var i = 0; i < Life; i++)
			{
				var textureObject = TextureObjectDictionary["Life"];
				var pointF = new PointF(OriginalPosition.X + (float)i * size, OriginalPosition.Y);
				SpriteMain.Draw2D(textureObject.TXTure, textureObject.PosRect, sizeF, new PointF(0f, 0f), 0f, pointF, Color.FromArgb(TransparentValue, Color.White));
			}
			if (Life < 8)
			{
				var num = MyPlane.LifeChip * 5 / MyPlane.nextLifeChip[MyPlane.LifeUpCount];
				if (num > 0)
				{
					var textureObject = TextureObjectDictionary["Life" + (num - 1).ToString("D2")];
					var pointF = new PointF(OriginalPosition.X + (float)Life * size, OriginalPosition.Y);
					SpriteMain.Draw2D(textureObject.TXTure, textureObject.PosRect, sizeF, new PointF(0f, 0f), 0f, pointF, Color.FromArgb(TransparentValue, Color.White));
				}
			}
		}
	}
}
