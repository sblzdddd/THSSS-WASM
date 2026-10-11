using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000155 RID: 341
	internal class EnchantmentBonus : BaseEffect
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x00050EE8 File Offset: 0x0004F0E8
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x00050EFF File Offset: 0x0004F0FF
		private char[] BonusCharArray { get; set; }

		// Token: 0x060008BC RID: 2236 RVA: 0x00050F08 File Offset: 0x0004F108
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

		// Token: 0x060008BD RID: 2237 RVA: 0x00051028 File Offset: 0x0004F228
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

		// Token: 0x04000322 RID: 802
		private PointF ValuePoint;

		// Token: 0x04000323 RID: 803
		private int Index = 0;
	}
}
