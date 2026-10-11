using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001E1 RID: 481
	internal class ParticleLongerByV : BaseEffect
	{
		// Token: 0x06000B13 RID: 2835 RVA: 0x0006DC69 File Offset: 0x0006BE69
		public ParticleLongerByV(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData, textureName, Position, Velocity, Direction)
		{
			base.LifeTime = 30;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0006DC84 File Offset: 0x0006BE84
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time > 0)
			{
				base.TransparentValueF -= (float)(255 / base.LifeTime);
				base.ScaleLength = base.ScaleWidth + base.ScaleWidth * Math.Abs(base.Velocity) / 10f;
			}
		}
	}
}
