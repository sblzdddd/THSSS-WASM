using System;
using System.Drawing;

namespace Shooting
{
	public class Item_Line_Red : Item_Line
	{
		public Item_Line_Red(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, OriginalPosition)
		{
			base.FontType = "RedNum_";
		}

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
