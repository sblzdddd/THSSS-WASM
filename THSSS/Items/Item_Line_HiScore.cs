using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200006D RID: 109
	public class Item_Line_HiScore : Item_Line
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0001A754 File Offset: 0x00018954
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x0001A76B File Offset: 0x0001896B
		private long scoreValue { get; set; }

		// Token: 0x060003D1 RID: 977 RVA: 0x0001A774 File Offset: 0x00018974
		public Item_Line_HiScore(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, OriginalPosition)
		{
			base.FontType = "HiScoreNum_";
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0001A790 File Offset: 0x00018990
		public override void Show()
		{
			base.MaxValue = 0;
			base.Value = 0;
			if (base.MyPlane.HiScore - this.scoreValue < 1000L)
			{
				this.scoreValue += 1000L;
			}
			else
			{
				this.scoreValue += (base.MyPlane.HiScore - this.scoreValue) / 5L;
			}
			if (this.scoreValue > base.MyPlane.HiScore)
			{
				this.scoreValue = base.MyPlane.HiScore;
			}
			base.Text = (10000000000L + this.scoreValue).ToString("N0");
			base.Text = base.Text.Remove(0, 1).PadLeft(10, ' ');
			base.Show();
		}
	}
}
