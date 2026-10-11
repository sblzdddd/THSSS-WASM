using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000194 RID: 404
	internal class SelectBox : BaseEffect
	{
		// Token: 0x060009A3 RID: 2467 RVA: 0x0005C05B File Offset: 0x0005A25B
		public SelectBox(StageDataPackage StageData)
			: base(StageData)
		{
			base.TxtureObject = base.TextureObjectDictionary["selectframe2"];
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0005C080 File Offset: 0x0005A280
		public override void Show()
		{
			base.SpriteMain.Draw2D(base.TxtureObject.TXTure, new Rectangle((int)base.OriginalPosition.X, (int)base.OriginalPosition.Y, 512, 22), new SizeF(512f, 22f), new PointF(0f, 0f), 0f, base.OriginalPosition, Color.White);
		}
	}
}
