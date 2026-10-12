using System.Drawing;

namespace Shooting
{
	public class MySpell_Line : Life_Line
	{
		public MySpell_Line(StageDataPackage StageData, string textureName, PointF OriginalPosition)
			: base(StageData, textureName, OriginalPosition)
		{
			size = 16f;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			Life = MyPlane.Spell;
		}

		public override void Show()
		{
			SizeF sizeF = new SizeF(size, size);
			for (int i = 0; i < 8; i++)
			{
				TextureObject textureObject = TextureObjectDictionary["Bomb0d"];
				PointF pointF = new PointF(OriginalPosition.X + (float)i * size, OriginalPosition.Y);
				SpriteMain.Draw2D(textureObject.TXTure, textureObject.PosRect, sizeF, new PointF(0f, 0f), 0f, pointF, Color.FromArgb(TransparentValue, Color.White));
			}
			for (int i = 0; i < Life; i++)
			{
				TextureObject textureObject = TextureObjectDictionary["Bomb"];
				PointF pointF = new PointF(OriginalPosition.X + (float)i * size, OriginalPosition.Y);
				SpriteMain.Draw2D(textureObject.TXTure, textureObject.PosRect, sizeF, new PointF(0f, 0f), 0f, pointF, Color.FromArgb(TransparentValue, Color.White));
			}
			if (Life < 8)
			{
				int num = MyPlane.SpellChip * 4 / MyPlane.nextSpellChip;
				if (num > 0)
				{
					TextureObject textureObject = TextureObjectDictionary["Bomb" + (num - 1).ToString("D2")];
					PointF pointF = new PointF(OriginalPosition.X + (float)Life * size, OriginalPosition.Y);
					SpriteMain.Draw2D(textureObject.TXTure, textureObject.PosRect, sizeF, new PointF(0f, 0f), 0f, pointF, Color.FromArgb(TransparentValue, Color.White));
				}
			}
		}
	}
}
