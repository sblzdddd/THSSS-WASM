using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001A8 RID: 424
	public class StarPointLineR : StarPointLineG
	{
		// Token: 0x06000A1B RID: 2587 RVA: 0x00060496 File Offset: 0x0005E696
		public StarPointLineR(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, OriginalPosition)
		{
			this.type = "B";
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x000604B0 File Offset: 0x0005E6B0
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.MyPlane.LastColor == EnchantmentType.Red && base.Story == null)
			{
				base.DestPoint = new PointF(66f, 404f);
			}
			else
			{
				base.DestPoint = new PointF(-6f, 404f);
			}
			base.Velocity = 3f;
		}
	}
}
