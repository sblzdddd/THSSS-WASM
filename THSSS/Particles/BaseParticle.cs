using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200016E RID: 366
	internal class BaseParticle : BaseEffect
	{
		// Token: 0x060008F9 RID: 2297 RVA: 0x0005661A File Offset: 0x0005481A
		public BaseParticle()
		{
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00056625 File Offset: 0x00054825
		public BaseParticle(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData, textureName, Position, Velocity, Direction)
		{
			base.LifeTime = 100;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00056640 File Offset: 0x00054840
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time > 0)
			{
				base.TransparentValueF -= (float)(255 / base.LifeTime);
			}
		}
	}
}
