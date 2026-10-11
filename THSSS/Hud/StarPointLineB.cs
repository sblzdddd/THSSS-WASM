using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200018F RID: 399
	public class StarPointLineB : StarPointLineG
	{
		// Token: 0x06000991 RID: 2449 RVA: 0x0005B8F0 File Offset: 0x00059AF0
		public StarPointLineB(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, OriginalPosition)
		{
			this.type = "A";
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0005B908 File Offset: 0x00059B08
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.MyPlane.LastColor == EnchantmentType.Blue && base.Story == null)
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
