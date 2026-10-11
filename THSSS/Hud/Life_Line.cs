using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200000E RID: 14
	public class Life_Line : BaseInterface
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00005F1C File Offset: 0x0000411C
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00005ECC File Offset: 0x000040CC
		public int Life
		{
			get
			{
				return this.life;
			}
			set
			{
				if (value > this.maxLife)
				{
					this.life = this.maxLife;
				}
				else if (value < 0)
				{
					this.life = 0;
				}
				else
				{
					this.life = value;
				}
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005F34 File Offset: 0x00004134
		public Life_Line(StageDataPackage StageData, string textureName, PointF OriginalPosition)
			: base(StageData, textureName, OriginalPosition)
		{
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005F55 File Offset: 0x00004155
		public Life_Line(StageDataPackage StageData, TextureObject TxtureObject, PointF OriginalPosition)
			: base(StageData, null, OriginalPosition)
		{
			base.TxtureObject = TxtureObject;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005F80 File Offset: 0x00004180
		public override void Show()
		{
			SizeF sizeF = new SizeF(this.size, this.size);
			for (int i = 0; i < this.life; i++)
			{
				PointF pointF = new PointF(base.OriginalPosition.X + (float)i * this.size, base.OriginalPosition.Y);
				base.SpriteMain.Draw2D(base.TxtureObject.TXTure, base.TxtureObject.PosRect, sizeF, new PointF(0f, 0f), 0f, pointF, Color.FromArgb(base.TransparentValue, Color.White));
			}
		}

		// Token: 0x0400004D RID: 77
		private int life;

		// Token: 0x0400004E RID: 78
		protected int maxLife = 9;

		// Token: 0x0400004F RID: 79
		public float size = 20f;
	}
}
