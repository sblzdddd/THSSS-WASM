using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000091 RID: 145
	public class EDPetal_Emitter : BaseEffect
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x00023210 File Offset: 0x00021410
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x00023227 File Offset: 0x00021427
		public int Hold { get; set; }

		// Token: 0x0600049B RID: 1179 RVA: 0x00023230 File Offset: 0x00021430
		public EDPetal_Emitter(StageDataPackage StageData, PointF OriginalPosition, string EDText)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			base.OriginalPosition = OriginalPosition;
			base.LifeTime = 130;
			this.EDText = EDText;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00023280 File Offset: 0x00021480
		public override void Shoot()
		{
			if (base.Time == 10)
			{
				BaseEffect baseEffect = new BaseEffect(base.StageData, this.EDText, base.OriginalPosition, 0f, 0.0);
				baseEffect.Scale = base.Scale;
				baseEffect.Direction = 1.5707963267948966;
				baseEffect.OriginalPosition = base.OriginalPosition;
				baseEffect.Active = false;
				baseEffect.TransparentValueF = 0f;
				baseEffect.TransparentVelocity = 10f;
				baseEffect.LifeTime = 322 + this.Hold;
				baseEffect.TransparentVelocityDictionary.Add(300 + this.Hold, -12f);
			}
			for (int i = 0; i < 5; i++)
			{
				if (base.Time == 1 + i * 3)
				{
					EDPetal edpetal = new EDPetal(base.StageData, "ED_Petal", base.OriginalPosition, 0f, 0.0);
					edpetal.DestAngleDegree = (double)(72 * i);
					edpetal.LifeTime = 300 + this.Hold;
				}
			}
		}

		// Token: 0x0400016E RID: 366
		private string EDText;
	}
}
