using System.Drawing;

namespace Shooting
{
	public class Life_Line : BaseInterface
	{
		public int Life
		{
			get
			{
				return life;
			}
			set
			{
				if (value > maxLife)
				{
					life = maxLife;
				}
				else if (value < 0)
				{
					life = 0;
				}
				else
				{
					life = value;
				}
			}
		}

		public Life_Line(StageDataPackage StageData, string textureName, PointF OriginalPosition)
			: base(StageData, textureName, OriginalPosition)
		{
		}

		public Life_Line(StageDataPackage StageData, TextureObject TxtureObject, PointF OriginalPosition)
			: base(StageData, null, OriginalPosition)
		{
			this.TxtureObject = TxtureObject;
		}

		public override void Show()
		{
			SizeF sizeF = new SizeF(size, size);
			for (int i = 0; i < life; i++)
			{
				PointF pointF = new PointF(OriginalPosition.X + (float)i * size, OriginalPosition.Y);
				SpriteMain.Draw2D(TxtureObject.TXTure, TxtureObject.PosRect, sizeF, new PointF(0f, 0f), 0f, pointF, Color.FromArgb(TransparentValue, Color.White));
			}
		}

		private int life;

		protected int maxLife = 9;

		public float size = 20f;
	}
}
