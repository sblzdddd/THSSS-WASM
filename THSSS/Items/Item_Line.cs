using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000045 RID: 69
	public class Item_Line : BaseInterface
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000F928 File Offset: 0x0000DB28
		// (set) Token: 0x06000262 RID: 610 RVA: 0x0000F940 File Offset: 0x0000DB40
		public int Value
		{
			get
			{
				return this.vl;
			}
			set
			{
				if (value < 0)
				{
					this.vl = 0;
				}
				else if (value > this.MaxValue)
				{
					this.vl = this.MaxValue;
				}
				else
				{
					this.vl = value;
				}
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000F990 File Offset: 0x0000DB90
		// (set) Token: 0x06000264 RID: 612 RVA: 0x0000F9A7 File Offset: 0x0000DBA7
		public int MaxValue { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000F9B0 File Offset: 0x0000DBB0
		// (set) Token: 0x06000266 RID: 614 RVA: 0x0000F9C7 File Offset: 0x0000DBC7
		public string Text { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000F9D0 File Offset: 0x0000DBD0
		// (set) Token: 0x06000268 RID: 616 RVA: 0x0000F9E7 File Offset: 0x0000DBE7
		public Color TextColorValue { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000F9F0 File Offset: 0x0000DBF0
		// (set) Token: 0x0600026A RID: 618 RVA: 0x0000FA07 File Offset: 0x0000DC07
		public string FontType { get; set; }

		// Token: 0x0600026B RID: 619 RVA: 0x0000FA10 File Offset: 0x0000DC10
		public Item_Line(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, "ItemLine", OriginalPosition)
		{
			this.MaxValue = 1;
			base.TransparentValueF = 0f;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000FA48 File Offset: 0x0000DC48
		public virtual void DrawText(Point Pos, float Scale, Color color)
		{
			int num = 0;
			char[] array = this.Text.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == '.' || array[i] == ',')
				{
					num += (int)(8f * Scale);
				}
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == '.' || array[i] == ',')
				{
					num -= (int)(5f * Scale);
				}
				PointF pointF = new PointF((float)(Pos.X + 5 + num), (float)(Pos.Y + 6));
				if (array[i].ToString() != " ")
				{
					base.SpriteMain.Draw2D(base.TextureObjectDictionary[this.FontType + array[i].ToString()], Scale, 0f, pointF, Color.FromArgb(base.TransparentValue, color));
				}
				num += (int)((float)this.Delta * Scale);
				if (array[i] == '.' || array[i] == ',')
				{
					num -= (int)(3f * Scale);
				}
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000FB98 File Offset: 0x0000DD98
		public override void Show()
		{
			if (this.MaxValue != 0)
			{
				int num = base.TxtureObject.Width * this.Value / this.MaxValue;
				if (num > 0)
				{
					Size size = new Size(num, base.TxtureObject.Height);
					Rectangle rectangle = new Rectangle(new Point(0, 0), size);
					base.SpriteMain.Draw2D(base.TxtureObject.TXTure, base.TxtureObject.PosRect, size, base.TxtureObject.LeftTop, 0f, base.OriginalPosition, Color.FromArgb(base.TransparentValue * 7 / 10, base.ColorValue));
				}
			}
			Point point = new Point((int)base.OriginalPosition.X, (int)base.OriginalPosition.Y + 3);
			this.DrawText(point, 1f, base.ColorValue);
		}

		// Token: 0x040000B9 RID: 185
		private int vl = 0;

		// Token: 0x040000BA RID: 186
		public int Delta = 13;
	}
}
