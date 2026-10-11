using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200017C RID: 380
	internal class BackgroundTouhouWater : BackgroundTouhouStage01_3D
	{
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x000583C8 File Offset: 0x000565C8
		// (set) Token: 0x0600092D RID: 2349 RVA: 0x000583DF File Offset: 0x000565DF
		public float V1 { get; set; }

		// Token: 0x0600092E RID: 2350 RVA: 0x000583E8 File Offset: 0x000565E8
		public BackgroundTouhouWater(StageDataPackage StageData, string textureName)
			: base(StageData, textureName)
		{
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x000583F8 File Offset: 0x000565F8
		public override void Ctrl()
		{
			base.Ctrl();
			float v = this.V1;
			float num = 0.2f * (float)Math.Sin((double)(base.Time + 30) * 3.141592653589793 * 2.0 / 200.0);
			float num2 = base.OriginalPosition.X + v;
			float num3 = base.OriginalPosition.Y + num;
			if (num2 < -500f)
			{
				num2 += 1000f;
			}
			else if (num2 > 500f)
			{
				num2 -= 1000f;
			}
			base.OriginalPosition = new PointF(num2, num3);
		}
	}
}
