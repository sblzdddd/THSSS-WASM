using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200018E RID: 398
	public class CharacterMenuItem : BaseMenuItem
	{
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0005B6E0 File Offset: 0x000598E0
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x0005B6F7 File Offset: 0x000598F7
		public PointF DestPoint1 { get; set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x0005B700 File Offset: 0x00059900
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x0005B717 File Offset: 0x00059917
		public PointF DestPoint2 { get; set; }

		// Token: 0x0600098D RID: 2445 RVA: 0x0005B720 File Offset: 0x00059920
		public CharacterMenuItem(StageDataPackage StageData, string textureName)
			: base(StageData, textureName)
		{
			base.DestPoint = this.DestPoint1;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0005B73C File Offset: 0x0005993C
		public override void Ctrl()
		{
			if (base.Enabled)
			{
				base.Time++;
				this.Move();
				base.Velocity += base.Accelerate;
				base.TransparentValueF += base.TransparentVelocity;
				if (base.Time == 1)
				{
					base.OriginalPosition = this.DestPoint2;
				}
				if (base.Selected)
				{
					double num = 1.0 + Math.Sin((double)((float)base.Time / 6f));
					base.ColorValue = Color.White;
					base.TransparentVelocity = 25f;
					base.DestPoint = this.DestPoint1;
				}
				else
				{
					base.ColorValue = Color.Gray;
					base.TransparentVelocity = -25f;
					base.DestPoint = this.DestPoint2;
				}
				if (base.Time < base.TwinkleTime)
				{
					if (base.Time % 4 == 0)
					{
						base.ColorValue = Color.White;
					}
					else if (base.Time % 4 == 2)
					{
						base.ColorValue = Color.Gray;
					}
				}
				if (base.OnRemove)
				{
					base.MaxTransparent -= 15;
				}
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0005B8AA File Offset: 0x00059AAA
		public override void Select()
		{
			base.Time = 1;
			base.Selected = true;
			base.VibrateTime = 0;
			base.TwinkleTime = 0;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0005B8CD File Offset: 0x00059ACD
		public override void Click()
		{
			base.Time = 1;
			base.VibrateTime = 0;
			base.TwinkleTime = base.Time + 30;
		}
	}
}
