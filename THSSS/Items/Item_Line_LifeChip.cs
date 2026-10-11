using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200006C RID: 108
	public class Item_Line_LifeChip : Item_Line
	{
		// Token: 0x060003CD RID: 973 RVA: 0x0001A6B8 File Offset: 0x000188B8
		public Item_Line_LifeChip(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, OriginalPosition)
		{
			base.FontType = "ShardNum_";
			this.Delta = 9;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0001A6DC File Offset: 0x000188DC
		public override void Ctrl()
		{
			base.Ctrl();
			base.Value = 0;
			base.Text = base.MyPlane.LifeChip.ToString() + "/" + base.MyPlane.nextLifeChip[base.MyPlane.LifeUpCount].ToString();
			base.Text = base.Text.PadLeft(5, ' ');
		}
	}
}
