using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200000F RID: 15
	public class MyLife_Line : Life_Line
	{
		// Token: 0x0600010B RID: 267 RVA: 0x00006034 File Offset: 0x00004234
		public MyLife_Line(StageDataPackage StageData, string textureName, PointF OriginalPosition)
			: base(StageData, textureName, OriginalPosition)
		{
			this.size = 16f;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000604D File Offset: 0x0000424D
		public MyLife_Line(StageDataPackage StageData, TextureObject TxtureObject, PointF OriginalPosition)
			: base(StageData, TxtureObject, OriginalPosition)
		{
			this.size = 16f;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006066 File Offset: 0x00004266
		public override void Ctrl()
		{
			base.Ctrl();
			base.Life = base.MyPlane.Life;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00006084 File Offset: 0x00004284
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
