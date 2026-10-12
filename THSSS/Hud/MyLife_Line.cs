using System.Drawing;

namespace Shooting
{
	public class MyLife_Line : Life_Line
	{
		public MyLife_Line(StageDataPackage StageData, string textureName, PointF OriginalPosition)
			: base(StageData, textureName, OriginalPosition)
		{
			this.size = 16f;
		}

		public MyLife_Line(StageDataPackage StageData, TextureObject TxtureObject, PointF OriginalPosition)
			: base(StageData, TxtureObject, OriginalPosition)
		{
			this.size = 16f;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			base.Life = base.MyPlane.Life;
		}

		public override void Show()
		{
			SizeF sizeF = new SizeF(this.size, this.size);
			for (int i = 0; i < 8; i++)
			{
				TextureObject textureObject = base.TextureObjectDictionary["Life0d"];
				PointF pointF = new PointF(base.OriginalPosition.X + (float)i * this.size, base.OriginalPosition.Y);
				base.SpriteMain.Draw2D(textureObject.TXTure, textureObject.PosRect, sizeF, new PointF(0f, 0f), 0f, pointF, Color.FromArgb(base.TransparentValue, Color.White));
			}
			for (int i = 0; i < base.Life; i++)
			{
				TextureObject textureObject = base.TextureObjectDictionary["Life"];
				PointF pointF = new PointF(base.OriginalPosition.X + (float)i * this.size, base.OriginalPosition.Y);
				base.SpriteMain.Draw2D(textureObject.TXTure, textureObject.PosRect, sizeF, new PointF(0f, 0f), 0f, pointF, Color.FromArgb(base.TransparentValue, Color.White));
			}
			if (base.Life < 8)
			{
				int num = base.MyPlane.LifeChip * 5 / base.MyPlane.nextLifeChip[base.MyPlane.LifeUpCount];
				if (num > 0)
				{
					TextureObject textureObject = base.TextureObjectDictionary["Life" + (num - 1).ToString("D2")];
					PointF pointF = new PointF(base.OriginalPosition.X + (float)base.Life * this.size, base.OriginalPosition.Y);
					base.SpriteMain.Draw2D(textureObject.TXTure, textureObject.PosRect, sizeF, new PointF(0f, 0f), 0f, pointF, Color.FromArgb(base.TransparentValue, Color.White));
				}
			}
		}
	}
}
