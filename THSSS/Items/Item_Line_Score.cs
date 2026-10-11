using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000B9 RID: 185
	public class Item_Line_Score : Item_Line
	{
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x0002DFDC File Offset: 0x0002C1DC
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x0002DFF3 File Offset: 0x0002C1F3
		private long scoreValue { get; set; }

		// Token: 0x0600054D RID: 1357 RVA: 0x0002DFFC File Offset: 0x0002C1FC
		public Item_Line_Score(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, OriginalPosition)
		{
			base.FontType = "ScoreNum_";
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0002E018 File Offset: 0x0002C218
		public override void Show()
		{
			base.MaxValue = 0;
			base.Value = 0;
			if (base.MyPlane.Score - this.scoreValue < 1000L)
			{
				this.scoreValue += 1000L;
			}
			else
			{
				this.scoreValue += (base.MyPlane.Score - this.scoreValue) / 5L;
			}
			if (this.scoreValue > base.MyPlane.Score)
			{
				this.scoreValue = base.MyPlane.Score;
			}
			base.Text = (10000000000L + this.scoreValue).ToString("N0");
			base.Text = base.Text.Remove(0, 1).PadLeft(10, ' ');
			base.Show();
		}
	}
}
