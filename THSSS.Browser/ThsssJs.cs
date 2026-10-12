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

		[JSImport("beginImage", "thsss.js")]
		public static partial int BeginImage(byte[] bytes);

		[JSImport("imageState", "thsss.js")]
		public static partial int ImageState(int id);

		[JSImport("finishImage", "thsss.js")]
		public static partial int FinishImage(int id);

		[JSImport("imageWidth", "thsss.js")]
		public static partial int ImageWidth(int id);

		[JSImport("imageHeight", "thsss.js")]
		public static partial int ImageHeight(int id);

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
		public static partial void SetDepth(bool enable, bool write);

		[JSImport("setCull", "thsss.js")]
		public static partial void SetCull(int mode);

		[JSImport("drawSprites", "thsss.js")]
		public static partial void DrawSprites(int textureId, float flipV, byte[] vertices);

		[JSImport("drawEffect", "thsss.js")]
		public static partial void DrawEffect(string name, int textureId, float flipV, byte[] vertices, float blurLevel, float x, float y, float time, float radius, int colorKey);

		[JSImport("drawFixed", "thsss.js")]
		public static partial void DrawFixed(int textureId, byte[] vertices, byte[] world, byte[] view, byte[] proj, float mr, float mg, float mb, float ma, float lighting, float fog, float fogStart, float fogEnd, float fr, float fg, float fb);

		[JSImport("rasterText", "thsss.js")]
		public static partial byte[] RasterText(string text, int width, int height, float sizePx, bool bold, bool italic);

		[JSImport("rasterTextBox", "thsss.js")]
		public static partial byte[] RasterTextBox(string text, int width, int height, float sizePx, bool bold, bool italic, int argb, string family);

		[JSImport("paintText", "thsss.js")]
		public static partial void PaintText(int id, string text, int width, int height, float sizePx, bool bold, bool italic, int argb, string family);

		[JSImport("audioPrepare", "thsss.js")]
		public static partial int AudioPrepare(string path);

		[JSImport("audioVoice", "thsss.js")]
		public static partial int AudioVoice();

		[JSImport("audioDestroy", "thsss.js")]
		public static partial void AudioDestroy(int voice);

		[JSImport("audioSubmit", "thsss.js")]
		public static partial void AudioSubmit(int voice, int buffer, int playBegin, int playLength, int loopCount, int loopBegin, int loopLength);

		[JSImport("audioStart", "thsss.js")]
		public static partial void AudioStart(int voice);

		[JSImport("audioStop", "thsss.js")]
		public static partial void AudioStop(int voice);

		[JSImport("audioVolume", "thsss.js")]
		public static partial void AudioVolume(int voice, float volume);

		[JSImport("audioPan", "thsss.js")]
		public static partial void AudioPan(int voice, float left, float right);

		[JSImport("audioFilter", "thsss.js")]
		public static partial void AudioFilter(int voice, bool highpass, float frequency);

		[JSImport("audioSamples", "thsss.js")]
		public static partial int AudioSamples(int voice);

		[JSImport("audioChannels", "thsss.js")]
		public static partial int AudioChannels(int buffer);
	}
}
