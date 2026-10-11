using System;
using System.IO;

namespace SlimDX.Imaging
{
	public static class ImageFiles
	{
		public static byte[] DecodeRgba(string path, out int width, out int height)
		{
			byte[] array = File.ReadAllBytes(path);
			string text = Path.GetExtension(path);
			if (text != null && text.Equals(".bmp", StringComparison.OrdinalIgnoreCase))
			{
				return BmpDecoder.Decode(array, out width, out height);
			}
			return PngDecoder.Decode(array, out width, out height);
		}
	}
}
