using System.Drawing;

namespace Shooting
{
	public class Item_Line_Gray : Item_Line
	{
		public Item_Line_Gray(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, OriginalPosition)
		{
			base.FontType = "GrazeNum_";
		}

		public override void Ctrl()
		{
			base.Ctrl();
			base.MaxValue = 0;
			base.Value = 0;
			base.Text = base.MyPlane.Graze.ToString("N0");
			base.Text = base.Text.PadLeft(11, ' ');
		}
	}
}
