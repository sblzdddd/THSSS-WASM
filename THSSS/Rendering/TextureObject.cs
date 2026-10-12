using System.Drawing;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class TextureObject
	{
		public Texture TXTure { get; set; }

		public int Width
		{
			get
			{
				return this.PosRect.Width;
			}
		}

		public int Height
		{
			get
			{
				return this.PosRect.Height;
			}
		}

		public int SrcWidth { get; set; }

		public int SrcHeight { get; set; }

		public Rectangle PosRect { get; set; }

		public int OffsetX { get; set; }

		public int OffsetY { get; set; }

		public PointF RotatingCenter
		{
			get
			{
				return new PointF((float)(this.PosRect.Width / 2 + this.OffsetX), (float)(this.PosRect.Height / 2 + this.OffsetY));
			}
		}

		public PointF LeftTop
		{
			get
			{
				return new PointF(0f, 0f);
			}
		}

		public PointF RightTop
		{
			get
			{
				return new PointF((float)this.Width, 0f);
			}
		}

		public void Dispose()
		{
			if (!this.TXTure.Disposed)
			{
				this.TXTure.Dispose();
			}
		}
	}
}
