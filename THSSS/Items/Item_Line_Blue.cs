using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000085 RID: 133
	public class Item_Line_Blue : Item_Line
	{
		// Token: 0x06000463 RID: 1123 RVA: 0x0001FE35 File Offset: 0x0001E035
		public Item_Line_Blue(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, OriginalPosition)
		{
			base.FontType = "BlueNum_";
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0001FE50 File Offset: 0x0001E050
		public override void Ctrl()
		{
			base.Ctrl();
			base.MaxValue = 10000;
			base.Value = 0;
			base.Text = base.MyPlane.HighItemScore.ToString("N0");
			base.Text = base.Text.PadLeft(11, ' ');
		}
	}
}
