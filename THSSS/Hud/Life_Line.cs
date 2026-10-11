using System;
using System.Drawing;

namespace Shooting
{
	public class Life_Line : BaseInterface
	{
		public int Life
		{
			get
			{
				return this.life;
			}
			set
			{
				if (value > this.maxLife)
				{
					this.life = this.maxLife;
				}
				else if (value < 0)
				{
					this.life = 0;
				}
				else
				{
					this.life = value;
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
			base.TxtureObject = TxtureObject;
		}

		public override void Show()
		{
			SizeF sizeF = new SizeF(this.size, this.size);
			for (int i = 0; i < this.life; i++)
			{
				PointF pointF = new PointF(base.OriginalPosition.X + (float)i * this.size, base.OriginalPosition.Y);
				base.SpriteMain.Draw2D(base.TxtureObject.TXTure, base.TxtureObject.PosRect, sizeF, new PointF(0f, 0f), 0f, pointF, Color.FromArgb(base.TransparentValue, Color.White));
			}
		}

		private int life;

		protected int maxLife = 9;

		public float size = 20f;
	}
}
