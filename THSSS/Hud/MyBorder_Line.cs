using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001FD RID: 509
	public class MyBorder_Line : Life_Line
	{
		// Token: 0x06000B78 RID: 2936 RVA: 0x0007202C File Offset: 0x0007022C
		public MyBorder_Line(StageDataPackage StageData, string textureName, PointF OriginalPosition)
			: base(StageData, textureName, OriginalPosition)
		{
			this.size = 16f;
			this.maxLife = 11;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0007204D File Offset: 0x0007024D
		public override void Ctrl()
		{
			base.Ctrl();
			base.Life = base.MyPlane.EnchantmentCount;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0007206C File Offset: 0x0007026C
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
