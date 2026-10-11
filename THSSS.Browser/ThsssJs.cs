using System.Runtime.InteropServices.JavaScript;

namespace Thsss.Browser
{
	public static partial class ThsssJs
	{
		[JSImport("setStatus", "thsss.js")]
		public static partial void SetStatus(string text);

		[JSImport("pageBase", "thsss.js")]
		public static partial string PageBase();

		[JSImport("readSaves", "thsss.js")]
		public static partial string ReadSaves();

		[JSImport("writeSave", "thsss.js")]
		public static partial void WriteSave(string path, string data);

		[JSImport("clear", "thsss.js")]
		public static partial void Clear(float r, float g, float b, float a, bool color, bool depth);

		[JSImport("createTexture", "thsss.js")]
		public static partial int CreateTexture(int width, int height, byte[] pixels);

		[JSImport("createRenderTarget", "thsss.js")]
		public static partial int CreateRenderTarget(int width, int height);

		[JSImport("updateTexture", "thsss.js")]
		public static partial void UpdateTexture(int id, int width, int height, byte[] pixels);

		[JSImport("deleteTexture", "thsss.js")]
		public static partial void DeleteTexture(int id);

		[JSImport("bindTarget", "thsss.js")]
		public static partial void BindTarget(int id);

		[JSImport("setViewport", "thsss.js")]
		public static partial void SetViewport(int x, int y, int width, int height, int canvasWidth, int canvasHeight);

		[JSImport("setBlend", "thsss.js")]
		public static partial void SetBlend(bool enable, int src, int dst, int equation);

		[JSImport("setDepth", "thsss.js")]
		public static partial void SetDepth(bool enable);

		[JSImport("setCull", "thsss.js")]
		public static partial void SetCull(int mode);

		[JSImport("drawSprites", "thsss.js")]
		public static partial void DrawSprites(int textureId, float flipV, byte[] vertices);

		[JSImport("drawFixed", "thsss.js")]
		public static partial void DrawFixed(int textureId, byte[] vertices, byte[] world, byte[] view, byte[] proj, float mr, float mg, float mb, float ma, float lighting, float fog, float fogStart, float fogEnd, float fr, float fg, float fb);

		[JSImport("rasterText", "thsss.js")]
		public static partial byte[] RasterText(string text, int width, int height, float sizePx, bool bold, bool italic);
	}
}
