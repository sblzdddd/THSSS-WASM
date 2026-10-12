using System.Drawing;
using SlimDX.Direct3D9;

namespace Shooting.Planes.Story
{
	internal class DescriptionBox : BaseStoryItem
	{
		private SlimDX.Direct3D9.Font DXfont { get; set; }

		public Color FontColor { get; set; }

		public string Text { get; set; }

		public int OffsetX { get; set; }

		public int OffsetY { get; set; }

		public bool Shadowed { get; set; }

		public DescriptionBox(StageDataPackage StageData)
			: base(StageData)
		{
			TxtureObject = TextureObjectDictionary["对话框"];
			Position = new PointF((float)BoundRect.Left, (float)(BoundRect.Bottom - TxtureObject.Height * BoundRect.Width / TxtureObject.Width));
			Velocity = 0f;
			Direction = 0.0;
			TransparentValueF = 0f;
			MaxTransparent = 200;
			TransparentVelocity = 10f;
			OffsetX = 20;
			OffsetY = 20;
			sysfont = new System.Drawing.Font(StageData.GlobalData.FontType, 16f * GlobalDataPackage.FontScale, FontStyle.Bold);
			DXfont = new SlimDX.Direct3D9.Font(StageData.DeviceMain, sysfont);
			FontColor = Color.White;
		}

		public override void Ctrl()
		{
			if (StringTexture != null)
			{
				StringTexture.Dispose();
			}
			StringTexture = StageData.DrawString(Text, sysfont, Brushes.White, 1024, 256);
			base.Ctrl();
		}

		public override void Show()
		{
			var sizeF = new SizeF((float)BoundRect.Width, (float)(TxtureObject.Height * BoundRect.Width / TxtureObject.Width));
			SpriteMain.Draw2D(TxtureObject.TXTure, TxtureObject.PosRect, sizeF, new PointF(0f, 0f), 0f, Position, Color.FromArgb(TransparentValue, Color.White));
			if (Text != null)
			{
				if (StringTexture != null)
				{
					if (Shadowed)
					{
						SpriteMain.Draw2D(StringTexture, new Rectangle(0, 0, 1024, 256), new SizeF(1024f, 256f), new PointF((float)((int)Position.X + 1 + OffsetX), (float)((int)Position.Y + 1 + (int)((float)OffsetY * sizeF.Height / 90f))), Color.FromArgb(150, 0, 0, 0));
					}
					SpriteMain.Draw2D(StringTexture, new Rectangle(0, 0, 1024, 256), new SizeF(1024f, 256f), new PointF((float)((int)Position.X + OffsetX), (float)((int)Position.Y + (int)((float)OffsetY * sizeF.Height / 90f))), Color.White);
				}
			}
		}

		public override void Dispose()
		{
			DXfont.Dispose();
			if (StringTexture != null)
			{
				StringTexture.Dispose();
			}
		}

		private Texture StringTexture;

		private System.Drawing.Font sysfont;
	}
}
