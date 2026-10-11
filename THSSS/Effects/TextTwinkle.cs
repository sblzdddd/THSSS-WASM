using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000023 RID: 35
	public class TextTwinkle : BaseEffect
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00009F38 File Offset: 0x00008138
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00009F4F File Offset: 0x0000814F
		public int Circle { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00009F58 File Offset: 0x00008158
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00009F6F File Offset: 0x0000816F
		public Color TwinkleColor { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00009F78 File Offset: 0x00008178
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00009F8F File Offset: 0x0000818F
		public Color OriginalColor { get; set; }

		// Token: 0x060001AB RID: 427 RVA: 0x00009F98 File Offset: 0x00008198
		public TextTwinkle(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData, textureName, Position, Velocity, Direction)
		{
			base.Active = false;
			base.LifeTime = 150;
			this.Circle = 30;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00009FC8 File Offset: 0x000081C8
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time <= 1)
			{
				this.OriginalColor = base.ColorValue;
			}
			else if (base.Time < base.LifeTime * 3 / 5)
			{
				if (base.Time % this.Circle == 0)
				{
					base.ColorValue = this.TwinkleColor;
				}
				else if (base.Time % this.Circle == this.Circle / 2)
				{
					base.ColorValue = this.OriginalColor;
				}
			}
			else if (base.LifeTime * 4 / 5 < base.Time && base.Time < base.LifeTime)
			{
				base.TransparentValueF -= (float)(base.MaxTransparent * 5 / base.LifeTime);
			}
		}
	}
}
