using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000190 RID: 400
	public class Item_Line_Gray : Item_Line
	{
		// Token: 0x06000993 RID: 2451 RVA: 0x0005B97C File Offset: 0x00059B7C
		public Item_Line_Gray(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, OriginalPosition)
		{
			base.FontType = "GrazeNum_";
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0005B998 File Offset: 0x00059B98
		public override void Ctrl()
		{
			base.Ctrl();
			base.MaxValue = 0;
			base.Value = 0;
			base.Text = base.MyPlane.Graze.ToString("N0");
			base.Text = base.Text.PadLeft(11, ' ');
		}
	}
}
