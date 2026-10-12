using System.Drawing;

namespace Shooting
{
	public class SCMenuItem : DescriptionMenuItem
	{
		public SCMenuItem(StageDataPackage StageData, string Name)
			: base(StageData, Name)
		{
		}

		public override void Show()
		{
			base.Show();
			if (base.TxtureObject != null)
			{
				base.SpriteMain.Draw2D(base.TxtureObject, base.ScaleWidth, base.ScaleLength, 0f, new PointF(base.Position.X + (float)base.TxtureObject.Width * base.Scale / 2f + (float)this.OffX, base.Position.Y + (float)this.OffY + (float)base.TxtureObject.Height * base.Scale / 2f), Color.FromArgb(base.TransparentValue, base.ColorValue), base.Mirrored);
			}
		}

		public int OffX = 50;

		public int OffY = -5;
	}
}
