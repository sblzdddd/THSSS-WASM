using System;
using System.Drawing;

namespace Shooting
{
	public class Item_Line_SpellChip : Item_Line
	{
		public Item_Line_SpellChip(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, OriginalPosition)
		{
			base.FontType = "ShardNum_";
			this.Delta = 9;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			base.Value = 0;
			base.Text = base.MyPlane.SpellChip.ToString() + "/" + base.MyPlane.nextSpellChip.ToString();
			base.Text = base.Text.PadLeft(5, ' ');
		}
	}
}
