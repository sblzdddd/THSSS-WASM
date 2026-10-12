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
			if (TxtureObject != null)
			{
				SpriteMain.Draw2D(TxtureObject, ScaleWidth, ScaleLength, 0f, new PointF(Position.X + (float)TxtureObject.Width * Scale / 2f + (float)OffX, Position.Y + (float)OffY + (float)TxtureObject.Height * Scale / 2f), Color.FromArgb(TransparentValue, ColorValue), Mirrored);
			}
		}

		public int OffX = 50;

		public int OffY = -5;
	}
}
