using System.Drawing;
using SlimDX.Direct3D9;

namespace Shooting
{
	internal class StoryBox : BaseStoryItem
	{
		private SlimDX.Direct3D9.Font DXfont { get; set; }

		public Color FontColor { get; set; }

		public int OffsetX { get; set; }

		public int OffsetY { get; set; }

		public bool Shadowed { get; set; }

		public string DrawText { get; set; }

		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (value != this.text)
				{
					this.text = value;
					this.TextArray = this.text.ToCharArray();
					this.DrawText = "";
					base.Time = 0;
				}
			}
		}

		public string Text2
		{
			get
			{
				return this.text2;
			}
			set
			{
				if (value != this.text2)
				{
					this.text2 = value;
					this.TextArray2 = this.text2.ToCharArray();
					this.DrawText2 = "";
					base.Time = 0;
				}
			}
		}

		public StoryBox(StageDataPackage StageData)
			: base(StageData)
		{
			base.TxtureObject = base.TextureObjectDictionary["对话框"];
			base.Position = new PointF((float)base.BoundRect.Left, (float)(base.BoundRect.Bottom - base.TxtureObject.Height * base.BoundRect.Width / base.TxtureObject.Width - 8));
			base.Velocity = 0f;
			base.Direction = 0.0;
			base.TransparentValueF = 0f;
			base.TransparentVelocity = 10f;
			this.OffsetX = 20;
			this.OffsetY = 21;
			this.Shadowed = true;
			this.sysfont = new global::System.Drawing.Font(StageData.GlobalData.FontType, 16f * GlobalDataPackage.FontScale, FontStyle.Bold);
			this.DXfont = new SlimDX.Direct3D9.Font(StageData.DeviceMain, this.sysfont);
			this.FontColor = Color.White;
		}

		public override void Ctrl()
		{
			if (this.DrawText != null)
			{
				if (this.DrawText.Replace("\r\n", "") == this.text)
				{
					base.TransparentValueF = (float)base.MaxTransparent;
					return;
				}
			}
			if (this.TextArray != null)
			{
				if (base.Time < this.TextArray.Length)
				{
					if ((float)this.DXfont.MeasureString(base.SpriteMain.sprite, this.DrawText + this.TextArray[base.Time].ToString(), DrawTextFormat.Left).Width / 1.15f > (float)(base.TxtureObject.Width - this.OffsetX))
					{
						this.DrawText += "\r\n";
					}
					this.DrawText += this.TextArray[base.Time].ToString();
				}
			}
			if (this.StringTexture != null)
			{
				this.StringTexture.Dispose();
			}
			this.StringTexture = base.StageData.DrawString(this.DrawText, this.sysfont, Brushes.White, 512, 256);
			if (this.TextArray2 != null)
			{
				if (base.Time < this.TextArray2.Length)
				{
					if ((float)this.DXfont.MeasureString(base.SpriteMain.sprite, this.DrawText2 + this.TextArray2[base.Time].ToString(), DrawTextFormat.Left).Width / 1.15f > (float)(base.TxtureObject.Width - this.OffsetX))
					{
						this.DrawText2 += "\r\n";
					}
					this.DrawText2 += this.TextArray2[base.Time].ToString();
				}
			}
			if (this.StringTexture2 != null)
			{
				this.StringTexture2.Dispose();
			}
			this.StringTexture2 = base.StageData.DrawString(this.DrawText2, this.sysfont, Brushes.White, 512, 256);
			base.Ctrl();
		}

		public void DrawFullText()
		{
			if (this.TextArray != null)
			{
				while (base.Time < this.TextArray.Length)
				{
					if ((float)this.DXfont.MeasureString(base.SpriteMain.sprite, this.DrawText + this.TextArray[base.Time].ToString(), DrawTextFormat.Left).Width / 1.15f > (float)(base.TxtureObject.Width - this.OffsetX))
					{
						this.DrawText += "\r\n";
					}
					this.DrawText += this.TextArray[base.Time].ToString();
					base.Time++;
				}
			}
			if (this.StringTexture != null)
			{
				this.StringTexture.Dispose();
			}
			this.StringTexture = base.StageData.DrawString(this.DrawText, this.sysfont, Brushes.White, 512, 256);
			if (this.TextArray2 != null)
			{
				while (base.Time < this.TextArray2.Length)
				{
					if ((float)this.DXfont.MeasureString(base.SpriteMain.sprite, this.DrawText2 + this.TextArray2[base.Time].ToString(), DrawTextFormat.Left).Width / 1.15f > (float)(base.TxtureObject.Width - this.OffsetX))
					{
						this.DrawText2 += "\r\n";
					}
					this.DrawText2 += this.TextArray2[base.Time].ToString();
					base.Time++;
				}
			}
			if (this.StringTexture2 != null)
			{
				this.StringTexture2.Dispose();
			}
			this.StringTexture2 = base.StageData.DrawString(this.DrawText2, this.sysfont, Brushes.White, 512, 256);
		}

		public void SetFont(float FontSize)
		{
			if (this.sysfont != null)
			{
				FontSize *= GlobalDataPackage.FontScale;
				if (this.sysfont.Size != FontSize)
				{
					this.sysfont = new global::System.Drawing.Font(base.StageData.GlobalData.FontType, FontSize, FontStyle.Bold);
					this.DXfont = new SlimDX.Direct3D9.Font(base.StageData.DeviceMain, this.sysfont);
				}
			}
		}

		public override void Show()
		{
			SizeF sizeF = new SizeF((float)base.TxtureObject.Width, 116f);
			base.SpriteMain.Draw2D(base.TxtureObject.TXTure, base.TxtureObject.PosRect, sizeF, base.TxtureObject.LeftTop, 0f, base.Position, Color.FromArgb(base.TransparentValue, Color.White));
			if (this.Text != null)
			{
				if (this.StringTexture != null)
				{
					if (this.Shadowed)
					{
						base.SpriteMain.Draw2D(this.StringTexture, new Rectangle(0, 0, 512, 256), new SizeF(512f, 256f), new PointF((float)((int)base.Position.X + 2 + this.OffsetX), (float)((int)base.Position.Y + 2 + (int)((float)this.OffsetY * sizeF.Height / 90f))), Color.FromArgb(150, 0, 0, 0));
					}
					base.SpriteMain.Draw2D(this.StringTexture, new Rectangle(0, 0, 512, 256), new SizeF(512f, 256f), new PointF((float)((int)base.Position.X + this.OffsetX), (float)((int)base.Position.Y + (int)((float)this.OffsetY * sizeF.Height / 90f))), this.FontColor);
					if (this.StringTexture2 != null)
					{
						base.SpriteMain.Draw2D(this.StringTexture2, new Rectangle(0, 0, 512, 256), new SizeF(512f, 256f), new PointF((float)((int)base.Position.X + this.OffsetX), (float)((int)base.Position.Y + (int)((float)this.OffsetY * sizeF.Height / 90f))), this.FontColor2);
					}
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

		private string text;

		private Texture StringTexture;

		private global::System.Drawing.Font sysfont;

		private char[] TextArray;

		private string text2;

		private Texture StringTexture2;

		public Color FontColor2;

		public string DrawText2;

		private char[] TextArray2;
	}
}
