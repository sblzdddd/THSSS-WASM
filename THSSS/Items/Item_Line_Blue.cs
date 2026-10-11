using System;
using System.Drawing;

namespace Shooting
{
	public class Item_Line_Blue : Item_Line
	{
		public Item_Line_Blue(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, OriginalPosition)
		{
			base.FontType = "BlueNum_";
		}

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
