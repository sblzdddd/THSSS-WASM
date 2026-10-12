using System.Drawing;

namespace Shooting
{
	public class Item_Line_Green : Item_Line
	{
		public Item_Line_Green(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, OriginalPosition)
		{
			base.ColorValue = Color.FromArgb(177, 255, 191);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			base.MaxValue = 3000;
			base.Value = 0;
			base.Text = base.MyPlane.StarPoint.ToString("N0");
			base.Text = base.Text.PadLeft(11, ' ');
		}
	}
}
