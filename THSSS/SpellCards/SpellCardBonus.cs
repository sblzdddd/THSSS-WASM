using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000078 RID: 120
	internal class SpellCardBonus : BaseEffect
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0001BC68 File Offset: 0x00019E68
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x0001BC7F File Offset: 0x00019E7F
		private char[] BonusCharArray { get; set; }

		// Token: 0x06000411 RID: 1041 RVA: 0x0001BC88 File Offset: 0x00019E88
		public SpellCardBonus(StageDataPackage StageData, long Bonus)
			: base(StageData)
		{
			base.Layer = 1;
			base.LifeTime = 150;
			base.TxtureObject = base.TextureObjectDictionary["GetSpellCardBonus"];
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

		// Token: 0x06000412 RID: 1042 RVA: 0x0001BDC0 File Offset: 0x00019FC0
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

		// Token: 0x0400013F RID: 319
		private PointF ValuePoint;

		// Token: 0x04000140 RID: 320
		private int Index = 0;
	}
}
