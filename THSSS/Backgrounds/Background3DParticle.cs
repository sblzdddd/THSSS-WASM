using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200013D RID: 317
	internal class Background3DParticle : Background3DObject
	{
		// Token: 0x06000847 RID: 2119 RVA: 0x0004CC2A File Offset: 0x0004AE2A
		public Background3DParticle(StageDataPackage StageData, string modelName, PointF OriginalPosition, bool InsertAtFirst)
			: base(StageData, modelName, OriginalPosition, InsertAtFirst)
		{
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0004CC3C File Offset: 0x0004AE3C
		public override void Show()
		{
			float transparentValueF = base.TransparentValueF;
			this.tr = 50.0 * Math.Sin((double)((float)base.Time / 2f));
			base.TransparentValueF += (float)((int)this.tr);
			base.Show();
			base.TransparentValueF = transparentValueF;
		}

		// Token: 0x040002F3 RID: 755
		private double tr;
	}
}
