using System.Drawing;

namespace Shooting
{
	public class Item_Line : BaseInterface
	{
		public int Value
		{
			get
			{
				return vl;
			}
			set
			{
				if (value < 0)
				{
					vl = 0;
				}
				else if (value > MaxValue)
				{
					vl = MaxValue;
				}
				else
				{
					vl = value;
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
			MaxValue = 1;
			TransparentValueF = 0f;
		}

		public virtual void DrawText(Point Pos, float Scale, Color color)
		{
			int num = 0;
			char[] array = Text.ToCharArray();
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
					SpriteMain.Draw2D(TextureObjectDictionary[FontType + array[i].ToString()], Scale, 0f, pointF, Color.FromArgb(TransparentValue, color));
				}
				num += (int)((float)Delta * Scale);
				if (array[i] == '.' || array[i] == ',')
				{
					num -= (int)(3f * Scale);
				}
			}
		}

		public override void Show()
		{
			if (MaxValue != 0)
			{
				int num = TxtureObject.Width * Value / MaxValue;
				if (num > 0)
				{
					Size size = new Size(num, TxtureObject.Height);
					Rectangle rectangle = new Rectangle(new Point(0, 0), size);
					SpriteMain.Draw2D(TxtureObject.TXTure, TxtureObject.PosRect, size, TxtureObject.LeftTop, 0f, OriginalPosition, Color.FromArgb(TransparentValue * 7 / 10, ColorValue));
				}
			}
			Point point = new Point((int)OriginalPosition.X, (int)OriginalPosition.Y + 3);
			DrawText(point, 1f, ColorValue);
		}

		private int vl = 0;

		public int Delta = 13;
	}
}
