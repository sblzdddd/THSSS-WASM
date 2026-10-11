using System.Drawing.Text;

namespace System.Drawing
{
	public abstract class Brush : System.IDisposable
	{
		public void Dispose()
		{
		}
	}

	public class SolidBrush : Brush
	{
		public Color Color { get; set; }

		public SolidBrush(Color color)
		{
			this.Color = color;
		}
	}

	public class Graphics : System.IDisposable
	{
		public TextRenderingHint TextRenderingHint { get; set; }

		public static Graphics FromImage(Image image)
		{
			return new Graphics();
		}

		public void DrawString(string text, Font font, Brush brush, float x, float y)
		{
		}

		public void Dispose()
		{
		}
	}
}
