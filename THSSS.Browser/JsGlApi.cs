using SlimDX.Gl;

namespace Thsss.Browser
{
	public sealed class JsGlApi : IGlApi
	{
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
			ThsssJs.BindTarget(textureId);
		}

		public void SetViewport(int x, int y, int width, int height, int canvasWidth, int canvasHeight)
		{
			ThsssJs.SetViewport(x, y, width, height, canvasWidth, canvasHeight);
		}

		public void SetBlend(bool enable, int src, int dst, int equation)
		{
			ThsssJs.SetBlend(enable, src, dst, equation);
		}

		public void SetDepth(bool enable)
		{
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
	}
}
