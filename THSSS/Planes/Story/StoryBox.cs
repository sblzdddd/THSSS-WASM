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
				return text;
			}
			set
			{
				if (value != text)
				{
					text = value;
					TextArray = text.ToCharArray();
					DrawText = "";
					Time = 0;
				}
			}
		}

		public string Text2
		{
			get
			{
				return text2;
			}
			set
			{
				if (value != text2)
				{
					text2 = value;
					TextArray2 = text2.ToCharArray();
					DrawText2 = "";
					Time = 0;
				}
			}
		}

		public StoryBox(StageDataPackage StageData)
			: base(StageData)
		{
			TxtureObject = TextureObjectDictionary["对话框"];
			Position = new PointF((float)BoundRect.Left, (float)(BoundRect.Bottom - TxtureObject.Height * BoundRect.Width / TxtureObject.Width - 8));
			Velocity = 0f;
			Direction = 0.0;
			TransparentValueF = 0f;
			TransparentVelocity = 10f;
			OffsetX = 20;
			OffsetY = 21;
			Shadowed = true;
			sysfont = new System.Drawing.Font(StageData.GlobalData.FontType, 16f * GlobalDataPackage.FontScale, FontStyle.Bold);
			DXfont = new SlimDX.Direct3D9.Font(StageData.DeviceMain, sysfont);
			FontColor = Color.White;
		}

		public override void Ctrl()
		{
			if (DrawText != null)
			{
				if (DrawText.Replace("\r\n", "") == text)
				{
					TransparentValueF = (float)MaxTransparent;
					return;
				}
			}
			if (TextArray != null)
			{
				if (Time < TextArray.Length)
				{
					if ((float)DXfont.MeasureString(SpriteMain.sprite, DrawText + TextArray[Time].ToString(), DrawTextFormat.Left).Width / 1.15f > (float)(TxtureObject.Width - OffsetX))
					{
						DrawText += "\r\n";
					}
					DrawText += TextArray[Time].ToString();
				}
			}
			if (StringTexture != null)
			{
				StringTexture.Dispose();
			}
			StringTexture = StageData.DrawString(DrawText, sysfont, Brushes.White, 512, 256);
			if (TextArray2 != null)
			{
				if (Time < TextArray2.Length)
				{
					if ((float)DXfont.MeasureString(SpriteMain.sprite, DrawText2 + TextArray2[Time].ToString(), DrawTextFormat.Left).Width / 1.15f > (float)(TxtureObject.Width - OffsetX))
					{
						DrawText2 += "\r\n";
					}
					DrawText2 += TextArray2[Time].ToString();
				}
			}
			if (StringTexture2 != null)
			{
				StringTexture2.Dispose();
			}
			StringTexture2 = StageData.DrawString(DrawText2, sysfont, Brushes.White, 512, 256);
			base.Ctrl();
		}

		public void DrawFullText()
		{
			if (TextArray != null)
			{
				while (Time < TextArray.Length)
				{
					if ((float)DXfont.MeasureString(SpriteMain.sprite, DrawText + TextArray[Time].ToString(), DrawTextFormat.Left).Width / 1.15f > (float)(TxtureObject.Width - OffsetX))
					{
						DrawText += "\r\n";
					}
					DrawText += TextArray[Time].ToString();
					Time++;
				}
			}
			if (StringTexture != null)
			{
				StringTexture.Dispose();
			}
			StringTexture = StageData.DrawString(DrawText, sysfont, Brushes.White, 512, 256);
			if (TextArray2 != null)
			{
				while (Time < TextArray2.Length)
				{
					if ((float)DXfont.MeasureString(SpriteMain.sprite, DrawText2 + TextArray2[Time].ToString(), DrawTextFormat.Left).Width / 1.15f > (float)(TxtureObject.Width - OffsetX))
					{
						DrawText2 += "\r\n";
					}
					DrawText2 += TextArray2[Time].ToString();
					Time++;
				}
			}
			if (StringTexture2 != null)
			{
				StringTexture2.Dispose();
			}
			StringTexture2 = StageData.DrawString(DrawText2, sysfont, Brushes.White, 512, 256);
		}

		public void SetFont(float FontSize)
		{
			if (sysfont != null)
			{
				FontSize *= GlobalDataPackage.FontScale;
				if (sysfont.Size != FontSize)
				{
					sysfont = new System.Drawing.Font(StageData.GlobalData.FontType, FontSize, FontStyle.Bold);
					DXfont = new SlimDX.Direct3D9.Font(StageData.DeviceMain, sysfont);
				}
			}
		}

		public override void Show()
		{
			var sizeF = new SizeF((float)TxtureObject.Width, 116f);
			SpriteMain.Draw2D(TxtureObject.TXTure, TxtureObject.PosRect, sizeF, TxtureObject.LeftTop, 0f, Position, Color.FromArgb(TransparentValue, Color.White));
			if (Text != null)
			{
				if (StringTexture != null)
				{
					if (Shadowed)
					{
						SpriteMain.Draw2D(StringTexture, new Rectangle(0, 0, 512, 256), new SizeF(512f, 256f), new PointF((float)((int)Position.X + 2 + OffsetX), (float)((int)Position.Y + 2 + (int)((float)OffsetY * sizeF.Height / 90f))), Color.FromArgb(150, 0, 0, 0));
					}
					SpriteMain.Draw2D(StringTexture, new Rectangle(0, 0, 512, 256), new SizeF(512f, 256f), new PointF((float)((int)Position.X + OffsetX), (float)((int)Position.Y + (int)((float)OffsetY * sizeF.Height / 90f))), FontColor);
					if (StringTexture2 != null)
					{
						SpriteMain.Draw2D(StringTexture2, new Rectangle(0, 0, 512, 256), new SizeF(512f, 256f), new PointF((float)((int)Position.X + OffsetX), (float)((int)Position.Y + (int)((float)OffsetY * sizeF.Height / 90f))), FontColor2);
					}
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

		private string text;

		private Texture StringTexture;

		private System.Drawing.Font sysfont;

		private char[] TextArray;

		private string text2;

		private Texture StringTexture2;

		public Color FontColor2 = default(Color);

		public string DrawText2;

		private char[] TextArray2;
	}
}
