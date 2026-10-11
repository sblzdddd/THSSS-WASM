using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001E4 RID: 484
	public class Item_Line_Green : Item_Line
	{
		// Token: 0x06000B1B RID: 2843 RVA: 0x0006E06C File Offset: 0x0006C26C
		public Item_Line_Green(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, OriginalPosition)
		{
			base.ColorValue = Color.FromArgb(177, 255, 191);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0006E094 File Offset: 0x0006C294
		public override void Ctrl()
		{
			base.Ctrl();
			base.MaxValue = 3000;
			base.Value = 0;
			base.Text = base.MyPlane.StarPoint.ToString("N0");
			base.Text = base.Text.PadLeft(11, ' ');
		}
	}
}
