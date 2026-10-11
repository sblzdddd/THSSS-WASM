using System;
using System.IO;

namespace SlimDX.Imaging
{
	public static class BmpDecoder
	{
		public static byte[] Decode(byte[] file, out int width, out int height)
		{
			if (file == null || file.Length < 54 || file[0] != 66 || file[1] != 77)
			{
				throw new InvalidDataException("Not a BMP.");
			}
			int num = BitConverter.ToInt32(file, 10);
			int num2 = BitConverter.ToInt32(file, 18);
			int num3 = BitConverter.ToInt32(file, 22);
			int num4 = BitConverter.ToInt16(file, 28);
			bool flag = num3 < 0;
			int num5 = Math.Abs(num3);
			width = num2;
			height = num5;
			byte[] array = new byte[num2 * num5 * 4];
			int num6 = (num4 == 32) ? num2 * 4 : (num2 * 3 + 3) / 4 * 4;
			for (int i = 0; i < num5; i++)
			{
				int num7 = flag ? i : num5 - 1 - i;
				int num8 = num + num7 * num6;
				for (int j = 0; j < num2; j++)
				{
					int num9 = (i * num2 + j) * 4;
					int num10 = num8 + j * (num4 / 8);
					if (num10 + 2 >= file.Length)
					{
						continue;
					}
					array[num9] = file[num10 + 2];
					array[num9 + 1] = file[num10 + 1];
					array[num9 + 2] = file[num10];
					array[num9 + 3] = (num4 == 32) ? file[num10 + 3] : byte.MaxValue;
				}
			}
			return array;
		}
	}
}
