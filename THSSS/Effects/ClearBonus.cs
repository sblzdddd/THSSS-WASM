using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000D3 RID: 211
	internal class ClearBonus : BaseEffect
	{
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0003434C File Offset: 0x0003254C
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x00034363 File Offset: 0x00032563
		private char[] BonusCharArray { get; set; }

		// Token: 0x060005D6 RID: 1494 RVA: 0x0003436C File Offset: 0x0003256C
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

		// Token: 0x060005D7 RID: 1495 RVA: 0x000344A4 File Offset: 0x000326A4
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

		// Token: 0x04000204 RID: 516
		private PointF ValuePoint;

		// Token: 0x04000205 RID: 517
		private int Index = 0;
	}
}
