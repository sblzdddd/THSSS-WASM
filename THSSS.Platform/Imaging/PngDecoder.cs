using System;
using System.IO;
using System.IO.Compression;

namespace SlimDX.Imaging
{
	public static class PngDecoder
	{
		public static byte[] Decode(byte[] file, out int width, out int height)
		{
			if (file == null || file.Length < 8 || file[0] != 137 || file[1] != 80)
			{
				throw new InvalidDataException("Not a PNG.");
			}
			int num = 8;
			int num2 = 0;
			int num3 = 0;
			int num4 = 8;
			int num5 = 6;
			byte[] array = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				while (num + 8 <= file.Length)
				{
					int num6 = ReadInt(file, num);
					string text = System.Text.Encoding.ASCII.GetString(file, num + 4, 4);
					int num7 = num + 8;
					if (text == "IHDR")
					{
						num2 = ReadInt(file, num7);
						num3 = ReadInt(file, num7 + 4);
						num4 = file[num7 + 8];
						num5 = file[num7 + 9];
						if (file[num7 + 12] != 0)
						{
							throw new NotSupportedException("Interlaced PNG is not supported.");
						}
					}
					else if (text == "IDAT")
					{
						memoryStream.Write(file, num7, num6);
					}
					else if (text == "IEND")
					{
						break;
					}
					num = num7 + num6 + 4;
				}
				array = memoryStream.ToArray();
			}
			width = num2;
			height = num3;
			int num8 = BytesPerPixel(num5, num4);
			int num9 = num2 * num8;
			byte[] array2 = Inflate(array);
			byte[] array3 = Unfilter(array2, num2, num3, num8, num9);
			return ToRgba(array3, num2, num3, num5, num4, num8);
		}

		private static int BytesPerPixel(int colorType, int depth)
		{
			int num = depth / 8;
			if (num < 1)
			{
				num = 1;
			}
			if (colorType == 2)
			{
				return 3 * num;
			}
			if (colorType == 6)
			{
				return 4 * num;
			}
			if (colorType == 0)
			{
				return num;
			}
			if (colorType == 4)
			{
				return 2 * num;
			}
			throw new NotSupportedException("PNG color type " + colorType + " is not supported.");
		}

		private static byte[] Inflate(byte[] zlib)
		{
			int num = 2;
			int num2 = zlib.Length - 6;
			if (num2 < 1)
			{
				num = 0;
				num2 = zlib.Length;
			}
			using (MemoryStream memoryStream = new MemoryStream(zlib, num, num2))
			{
				using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Decompress))
				{
					using (MemoryStream memoryStream2 = new MemoryStream())
					{
						deflateStream.CopyTo(memoryStream2);
						return memoryStream2.ToArray();
					}
				}
			}
		}

		private static byte[] Unfilter(byte[] raw, int width, int height, int bpp, int stride)
		{
			byte[] array = new byte[height * stride];
			int num = 0;
			for (int i = 0; i < height; i++)
			{
				int num2 = raw[num++];
				int num3 = i * stride;
				for (int j = 0; j < stride; j++)
				{
					byte b = raw[num++];
					byte b2 = (j >= bpp) ? array[num3 + j - bpp] : (byte)0;
					byte b3 = (i > 0) ? array[num3 - stride + j] : (byte)0;
					byte b4 = (i > 0 && j >= bpp) ? array[num3 - stride + j - bpp] : (byte)0;
					byte b5 = 0;
					if (num2 == 1)
					{
						b5 = b2;
					}
					else if (num2 == 2)
					{
						b5 = b3;
					}
					else if (num2 == 3)
					{
						b5 = (byte)((b2 + b3) / 2);
					}
					else if (num2 == 4)
					{
						b5 = Paeth(b2, b3, b4);
					}
					array[num3 + j] = (byte)((b + b5) & 255);
				}
			}
			return array;
		}

		private static byte Paeth(byte a, byte b, byte c)
		{
			int num = a + b - c;
			int num2 = Math.Abs(num - a);
			int num3 = Math.Abs(num - b);
			int num4 = Math.Abs(num - c);
			if (num2 <= num3 && num2 <= num4)
			{
				return a;
			}
			if (num3 <= num4)
			{
				return b;
			}
			return c;
		}

		private static byte[] ToRgba(byte[] raw, int width, int height, int colorType, int depth, int bpp)
		{
			byte[] array = new byte[width * height * 4];
			int num = depth > 8 ? 2 : 1;
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					int num2 = (i * width + j) * bpp;
					int num3 = (i * width + j) * 4;
					if (colorType == 6)
					{
						array[num3] = raw[num2];
						array[num3 + 1] = raw[num2 + num];
						array[num3 + 2] = raw[num2 + num * 2];
						array[num3 + 3] = raw[num2 + num * 3];
					}
					else if (colorType == 2)
					{
						array[num3] = raw[num2];
						array[num3 + 1] = raw[num2 + num];
						array[num3 + 2] = raw[num2 + num * 2];
						array[num3 + 3] = byte.MaxValue;
					}
					else if (colorType == 0)
					{
						array[num3] = raw[num2];
						array[num3 + 1] = raw[num2];
						array[num3 + 2] = raw[num2];
						array[num3 + 3] = byte.MaxValue;
					}
					else
					{
						array[num3] = raw[num2];
						array[num3 + 1] = raw[num2];
						array[num3 + 2] = raw[num2];
						array[num3 + 3] = raw[num2 + num];
					}
				}
			}
			return array;
		}

		private static int ReadInt(byte[] data, int offset)
		{
			return data[offset] << 24 | data[offset + 1] << 16 | data[offset + 2] << 8 | data[offset + 3];
		}
	}
}
