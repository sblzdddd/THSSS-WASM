using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200003C RID: 60
	public class MySpell_Line : Life_Line
	{
		// Token: 0x06000238 RID: 568 RVA: 0x0000DFA5 File Offset: 0x0000C1A5
		public MySpell_Line(StageDataPackage StageData, string textureName, PointF OriginalPosition)
			: base(StageData, textureName, OriginalPosition)
		{
			this.size = 16f;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000DFBE File Offset: 0x0000C1BE
		public override void Ctrl()
		{
			base.Ctrl();
			base.Life = base.MyPlane.Spell;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000DFDC File Offset: 0x0000C1DC
		public override void Show()
		{
			SizeF sizeF = new SizeF(this.size, this.size);
			for (int i = 0; i < 8; i++)
			{
				TextureObject textureObject = base.TextureObjectDictionary["Bomb0d"];
				PointF pointF = new PointF(base.OriginalPosition.X + (float)i * this.size, base.OriginalPosition.Y);
				base.SpriteMain.Draw2D(textureObject.TXTure, textureObject.PosRect, sizeF, new PointF(0f, 0f), 0f, pointF, Color.FromArgb(base.TransparentValue, Color.White));
			}
			for (int i = 0; i < base.Life; i++)
			{
				TextureObject textureObject = base.TextureObjectDictionary["Bomb"];
				PointF pointF = new PointF(base.OriginalPosition.X + (float)i * this.size, base.OriginalPosition.Y);
				base.SpriteMain.Draw2D(textureObject.TXTure, textureObject.PosRect, sizeF, new PointF(0f, 0f), 0f, pointF, Color.FromArgb(base.TransparentValue, Color.White));
			}
			if (base.Life < 8)
			{
				int num = base.MyPlane.SpellChip * 4 / base.MyPlane.nextSpellChip;
				if (num > 0)
				{
					TextureObject textureObject = base.TextureObjectDictionary["Bomb" + (num - 1).ToString("D2")];
					PointF pointF = new PointF(base.OriginalPosition.X + (float)base.Life * this.size, base.OriginalPosition.Y);
					base.SpriteMain.Draw2D(textureObject.TXTure, textureObject.PosRect, sizeF, new PointF(0f, 0f), 0f, pointF, Color.FromArgb(base.TransparentValue, Color.White));
				}
			}
		}
	}
}
