using System;
using System.Drawing;
using SlimDX.Gl;

namespace SlimDX.Direct3D9
{
	public class Font : ComObject
	{
		private readonly System.Drawing.Font font;

		private Texture texture;

		public Font(Device device, System.Drawing.Font font)
		{
			this.font = font ?? new System.Drawing.Font("sans-serif", 12f, FontStyle.Regular);
		}

		public int DrawString(Sprite sprite, string text, int x, int y, Color color)
		{
			if (sprite == null || string.IsNullOrEmpty(text))
			{
				return 0;
			}
			float num = this.font.Size > 0f ? this.font.Size : 12f;
			int num2 = Math.Max(8, (int)(text.Length * num) + 8);
			int num3 = Math.Max(8, (int)num + 8);
			byte[] array = GlHost.Need().RasterText(text, num2, num3, num, this.font.Bold, this.font.Italic);
			if (array == null || array.Length < num2 * num3 * 4)
			{
				return num3;
			}
			if (this.texture == null || this.texture.Width != num2 || this.texture.Height != num3)
			{
				if (this.texture != null)
				{
					this.texture.Dispose();
				}
				this.texture = new Texture(Device.Active, num2, num3, 1, Usage.None, Format.A8R8G8B8, Pool.Managed);
			}
			GlHost.Need().UpdateTexture(this.texture.GlId, num2, num3, array);
			sprite.Draw(this.texture, new Vector3?(new Vector3(0f, 0f, 0f)), new Vector3?(new Vector3((float)x, (float)y, 0f)), new Color4(color));
			return num3;
		}

		public System.Drawing.Rectangle MeasureString(Sprite sprite, string text, DrawTextFormat format)
		{
			if (string.IsNullOrEmpty(text))
			{
				return new System.Drawing.Rectangle(0, 0, 0, 0);
			}
			float num = this.font.Size > 0f ? this.font.Size : 16f;
			string[] array = text.Replace("\r\n", "\n").Split('\n');
			int num2 = 0;
			for (int i = 0; i < array.Length; i++)
			{
				float num3 = 0f;
				string text2 = array[i];
				for (int j = 0; j < text2.Length; j++)
				{
					char c = text2[j];
					if (c == '\r')
					{
						continue;
					}
					num3 += (c > '\u007f' ? num : num * 0.55f) * 1.15f;
				}
				if ((int)num3 > num2)
				{
					num2 = (int)num3;
				}
			}
			return new System.Drawing.Rectangle(0, 0, num2, (int)(array.Length * num * 1.35f));
		}
	}
}
