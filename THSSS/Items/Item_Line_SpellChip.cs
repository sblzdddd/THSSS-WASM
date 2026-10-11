using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000046 RID: 70
	public class Item_Line_SpellChip : Item_Line
	{
		// Token: 0x0600026E RID: 622 RVA: 0x0000FC98 File Offset: 0x0000DE98
		public Item_Line_SpellChip(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, OriginalPosition)
		{
			base.FontType = "ShardNum_";
			this.Delta = 9;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000FCBC File Offset: 0x0000DEBC
		public override void Ctrl()
		{
			base.Ctrl();
			base.Value = 0;
			base.Text = base.MyPlane.SpellChip.ToString() + "/" + base.MyPlane.nextSpellChip.ToString();
			base.Text = base.Text.PadLeft(5, ' ');
		}
	}
}
