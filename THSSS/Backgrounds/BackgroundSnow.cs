using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000F3 RID: 243
	internal class BackgroundSnow : BaseObject
	{
		// Token: 0x06000686 RID: 1670 RVA: 0x0003A498 File Offset: 0x00038698
		public BackgroundSnow(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.Background.BackgroundList.Add(this);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0003A4BD File Offset: 0x000386BD
		public override void Ctrl()
		{
			base.Time++;
			this.Shoot();
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0003A4D8 File Offset: 0x000386D8
		public override void Shoot()
		{
			if (base.Time % 20 == 0)
			{
				for (int i = 0; i < 3; i++)
				{
					ParticleBack particleBack = new ParticleBack(base.StageData, "snow", new PointF((float)base.Ran.Next(base.BoundRect.Left, base.BoundRect.Right), (float)base.Ran.Next(base.BoundRect.Top, base.BoundRect.Bottom / 3)), (float)base.Ran.Next(10, 20) / 10f, 1.5707963267948966 + 0.5 * base.Ran.NextDouble());
					particleBack.LifeTime = 200;
					particleBack.AngularVelocity = (float)base.Ran.Next(20) / 1000f;
					particleBack.Scale = (float)base.Ran.Next(2, 10) / 10f;
					particleBack.Active = true;
				}
			}
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0003A5FE File Offset: 0x000387FE
		public override void Show()
		{
		}
	}
}
