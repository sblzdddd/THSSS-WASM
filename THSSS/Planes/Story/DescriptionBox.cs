using System;
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
			base.TxtureObject = base.TextureObjectDictionary["对话框"];
			base.Position = new PointF((float)base.BoundRect.Left, (float)(base.BoundRect.Bottom - base.TxtureObject.Height * base.BoundRect.Width / base.TxtureObject.Width));
			base.Velocity = 0f;
			base.Direction = 0.0;
			base.TransparentValueF = 0f;
			base.MaxTransparent = 200;
			base.TransparentVelocity = 10f;
			this.OffsetX = 20;
			this.OffsetY = 20;
			this.sysfont = new global::System.Drawing.Font(StageData.GlobalData.FontType, 16f, FontStyle.Bold);
			this.DXfont = new SlimDX.Direct3D9.Font(StageData.DeviceMain, this.sysfont);
			this.FontColor = Color.White;
		}

		public override void Ctrl()
		{
			if (this.StringTexture != null)
			{
				this.StringTexture.Dispose();
			}
			this.StringTexture = base.StageData.DrawString(this.Text, this.sysfont, Brushes.White, 1024, 256);
			base.Ctrl();
		}

		public override void Show()
		{
			SizeF sizeF = new SizeF((float)base.BoundRect.Width, (float)(base.TxtureObject.Height * base.BoundRect.Width / base.TxtureObject.Width));
			base.SpriteMain.Draw2D(base.TxtureObject.TXTure, base.TxtureObject.PosRect, sizeF, new PointF(0f, 0f), 0f, base.Position, Color.FromArgb(base.TransparentValue, Color.White));
			if (this.Text != null)
			{
				if (this.StringTexture != null)
				{
					if (this.Shadowed)
					{
						base.SpriteMain.Draw2D(this.StringTexture, new Rectangle(0, 0, 1024, 256), new SizeF(1024f, 256f), new PointF((float)((int)base.Position.X + 1 + this.OffsetX), (float)((int)base.Position.Y + 1 + (int)((float)this.OffsetY * sizeF.Height / 90f))), Color.FromArgb(150, 0, 0, 0));
					}
					base.SpriteMain.Draw2D(this.StringTexture, new Rectangle(0, 0, 1024, 256), new SizeF(1024f, 256f), new PointF((float)((int)base.Position.X + this.OffsetX), (float)((int)base.Position.Y + (int)((float)this.OffsetY * sizeF.Height / 90f))), Color.White);
				}
			}
		}

		public override void Dispose()
		{
			this.DXfont.Dispose();
			if (this.StringTexture != null)
			{
				this.StringTexture.Dispose();
			}
		}

		private Texture StringTexture;

		private global::System.Drawing.Font sysfont;
	}
}
