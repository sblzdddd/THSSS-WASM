using System.Drawing;

namespace Shooting
{
	public class Item_Line : BaseInterface
	{
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

		public int MaxValue { get; set; }

		public string Text { get; set; }

		public Color TextColorValue { get; set; }

		public string FontType { get; set; }

		public Item_Line(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, "ItemLine", OriginalPosition)
		{
			this.MaxValue = 1;
			base.TransparentValueF = 0f;
		}

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

		private int vl = 0;

		public int Delta = 13;
	}
}
