using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001FA RID: 506
	internal class ParticleScore : BaseEffect
	{
		// Token: 0x06000B6B RID: 2923 RVA: 0x000718BD File Offset: 0x0006FABD
		public ParticleScore(StageDataPackage StageData, string textureName, PointF Position)
			: base(StageData, textureName, Position, 1f, -1.5707963705062866)
		{
			base.LifeTime = 50;
			base.Active = false;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x000718EC File Offset: 0x0006FAEC
		public override void Ctrl()
		{
			base.Angle = -base.Direction + 1.5707963267948966;
			base.Ctrl();
			if (base.Time > 0)
			{
				base.TransparentValueF -= (float)(255 / base.LifeTime);
			}
		}
	}
}
