using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000174 RID: 372
	public class Item_Line_Red : Item_Line
	{
		// Token: 0x0600091A RID: 2330 RVA: 0x00057A04 File Offset: 0x00055C04
		public Item_Line_Red(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, OriginalPosition)
		{
			base.FontType = "RedNum_";
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00057A20 File Offset: 0x00055C20
		public override void Ctrl()
		{
			base.Ctrl();
			base.MaxValue = 400;
			base.Value = 0;
			base.Text = ((float)base.MyPlane.Power / 100f).ToString("f2") + "/4.00";
			base.Text = base.Text.PadLeft(11, ' ');
		}
	}
}
