using SlimDX.Gl;

namespace Thsss.Browser
{
	public sealed class JsGlApi : IGlApi
	{
		private int viewX = int.MinValue;

		private int viewY;

		private int viewW;

		private int viewH;

		private int viewCw;

		private int viewCh;

		private int blendSrc = int.MinValue;

		private int blendDst;

		private int blendEquation;

		private bool blendEnable;

		private bool blendKnown;

		private bool depthOn;

		private bool depthKnown;

		private int boundTarget = int.MinValue;

		private static byte[] Pack(float[] values)
		{
			if (values == null)
			{
				return new byte[0];
			}
			byte[] bytes = new byte[values.Length * 4];
			System.Buffer.BlockCopy(values, 0, bytes, 0, bytes.Length);
			return bytes;
		}
		public void Clear(float r, float g, float b, float a, bool color, bool depth)
		{
			ThsssJs.Clear(r, g, b, a, color, depth);
		}

		public int CreateTexture(int width, int height, byte[] rgba)
		{
			return ThsssJs.CreateTexture(width, height, rgba);
		}

		public int CreateRenderTarget(int width, int height)
		{
			return ThsssJs.CreateRenderTarget(width, height);
		}

		public void UpdateTexture(int id, int width, int height, byte[] rgba)
		{
			ThsssJs.UpdateTexture(id, width, height, rgba);
		}

		public void DeleteTexture(int id)
		{
			ThsssJs.DeleteTexture(id);
		}

		public void BindTarget(int textureId)
		{
			if (this.boundTarget == textureId)
			{
				return;
			}
			this.boundTarget = textureId;
			this.viewX = int.MinValue;
			ThsssJs.BindTarget(textureId);
		}

		public void SetViewport(int x, int y, int width, int height, int canvasWidth, int canvasHeight)
		{
			if (this.viewX == x && this.viewY == y && this.viewW == width && this.viewH == height && this.viewCw == canvasWidth && this.viewCh == canvasHeight)
			{
				return;
			}
			this.viewX = x;
			this.viewY = y;
			this.viewW = width;
			this.viewH = height;
			this.viewCw = canvasWidth;
			this.viewCh = canvasHeight;
			ThsssJs.SetViewport(x, y, width, height, canvasWidth, canvasHeight);
		}

		public void SetBlend(bool enable, int src, int dst, int equation)
		{
			if (this.blendKnown && this.blendEnable == enable && this.blendSrc == src && this.blendDst == dst && this.blendEquation == equation)
			{
				return;
			}
			this.blendKnown = true;
			this.blendEnable = enable;
			this.blendSrc = src;
			this.blendDst = dst;
			this.blendEquation = equation;
			ThsssJs.SetBlend(enable, src, dst, equation);
		}

		public void SetDepth(bool enable)
		{
			if (this.depthKnown && this.depthOn == enable)
			{
				return;
			}
			this.depthKnown = true;
			this.depthOn = enable;
			ThsssJs.SetDepth(enable);
		}

		public void SetCull(int mode)
		{
			ThsssJs.SetCull(mode);
		}

		public void DrawSprites(int textureId, float flipV, float[] vertices)
		{
			ThsssJs.DrawSprites(textureId, flipV, Pack(vertices));
		}

		public void DrawFixed(int textureId, float[] vertices, float[] world, float[] view, float[] proj, float mr, float mg, float mb, float ma, float lighting, float fog, float fogStart, float fogEnd, float fr, float fg, float fb)
		{
			ThsssJs.DrawFixed(textureId, Pack(vertices), Pack(world), Pack(view), Pack(proj), mr, mg, mb, ma, lighting, fog, fogStart, fogEnd, fr, fg, fb);
		}

		public byte[] RasterText(string text, int width, int height, float sizePx, bool bold, bool italic)
		{
			return ThsssJs.RasterText(text, width, height, sizePx, bold, italic);
		}

		public void PaintText(int textureId, string text, int width, int height, float sizePx, bool bold, bool italic, int argb, string family)
		{
			ThsssJs.PaintText(textureId, text, width, height, sizePx, bold, italic, argb, family ?? "sans-serif");
		}
	}
}
