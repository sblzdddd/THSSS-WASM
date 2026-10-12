using System.Drawing;

namespace Shooting
{
	internal class EnchantmentBonus : BaseEffect
	{
		private char[] BonusCharArray { get; set; }

		public EnchantmentBonus(StageDataPackage StageData, long Bonus)
			: base(StageData)
		{
			base.Layer = 1;
			base.LifeTime = 150;
			base.MyPlane.Score += Bonus;
			base.TransparentVelocity = 100f;
			base.TransparentValueF = 0f;
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), 120f);
			base.AngleDegree = 90.0;
			this.BonusCharArray = Bonus.ToString().ToCharArray();
			int num = 0;
			for (int i = 0; i < this.BonusCharArray.Length; i++)
			{
				num += base.TextureObjectDictionary["Num" + this.BonusCharArray[i].ToString()].Width;
			}
			this.ValuePoint = new PointF(base.OriginalPosition.X - (float)(num / 2), base.OriginalPosition.Y + 40f);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == 100)
			{
				base.TransparentVelocity = -10f;
			}
			if (base.Time >= 20)
			{
				if (base.Time % 4 == 0)
				{
					if (this.Index < this.BonusCharArray.Length)
					{
						this.ValuePoint = new PointF(this.ValuePoint.X + (float)(base.TextureObjectDictionary["Num" + this.BonusCharArray[this.Index].ToString()].Width / 2), this.ValuePoint.Y);
						ParticleBonusNum particleBonusNum = new ParticleBonusNum(base.StageData, "Num" + this.BonusCharArray[this.Index].ToString(), this.ValuePoint);
						particleBonusNum.ColorValue = Color.FromArgb(255 - this.Index * 10, 255 - this.Index * 5, 255 - this.Index * 10);
						this.ValuePoint = new PointF(this.ValuePoint.X + (float)(base.TextureObjectDictionary["Num" + this.BonusCharArray[this.Index].ToString()].Width / 2), this.ValuePoint.Y);
						this.Index++;
					}
				}
			}
		}

		private PointF ValuePoint;

		private int Index = 0;
	}
}
