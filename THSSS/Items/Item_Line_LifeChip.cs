using System;
using System.Drawing;

namespace Shooting
{
	public class Item_Line_LifeChip : Item_Line
	{
		public Item_Line_LifeChip(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, OriginalPosition)
		{
			base.FontType = "ShardNum_";
			this.Delta = 9;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			base.Value = 0;
			base.Text = base.MyPlane.LifeChip.ToString() + "/" + base.MyPlane.nextLifeChip[base.MyPlane.LifeUpCount].ToString();
			base.Text = base.Text.PadLeft(5, ' ');
		}
	}
}
