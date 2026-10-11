using System;
using System.Drawing;

namespace Shooting
{
	internal class ClearBonus : BaseEffect
	{
		private char[] BonusCharArray { get; set; }

		public ClearBonus(StageDataPackage StageData, int Bonus)
			: base(StageData)
		{
			base.Layer = 1;
			base.LifeTime = 150;
			base.TxtureObject = base.TextureObjectDictionary["StageClear"];
			base.MyPlane.Score += (long)Bonus;
			base.TransparentVelocity = 6f;
			base.TransparentValueF = 0f;
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), 150f);
			base.AngleDegree = 90.0;
			this.BonusCharArray = Bonus.ToString().ToCharArray();
			int num = 0;
			for (int i = 0; i < this.BonusCharArray.Length; i++)
			{
				num += base.TextureObjectDictionary["Num" + this.BonusCharArray[i].ToString()].Width;
			}
			this.ValuePoint = new PointF(base.OriginalPosition.X - (float)(num / 2), base.OriginalPosition.Y + 36f);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == 100)
			{
				base.TransparentVelocity = -6f;
			}
			if (base.Time == 20)
			{
				while (this.Index < this.BonusCharArray.Length)
				{
					this.ValuePoint = new PointF(this.ValuePoint.X + (float)(base.TextureObjectDictionary["Num" + this.BonusCharArray[this.Index].ToString()].Width / 2), this.ValuePoint.Y);
					BaseEffect baseEffect = new BaseEffect(base.StageData, "Num" + this.BonusCharArray[this.Index].ToString(), new PointF(0f, 0f), 0f, 1.5707963267948966)
					{
						Active = false,
						OriginalPosition = this.ValuePoint,
						ColorValue = Color.FromArgb(255 - this.Index * 10, 255 - this.Index * 5, 255 - this.Index * 10),
						LifeTime = 100,
						TransparentValueF = 0f
					};
					baseEffect.TransparentVelocityDictionary.Add(1, 13f);
					baseEffect.TransparentVelocityDictionary.Add(80, -13f);
					this.ValuePoint = new PointF(this.ValuePoint.X + (float)(base.TextureObjectDictionary["Num" + this.BonusCharArray[this.Index].ToString()].Width / 2), this.ValuePoint.Y);
					this.Index++;
				}
			}
		}

		private PointF ValuePoint;

		private int Index = 0;
	}
}
