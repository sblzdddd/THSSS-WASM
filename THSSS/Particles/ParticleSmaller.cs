using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200016F RID: 367
	internal class ParticleSmaller : BaseParticle
	{
		// Token: 0x060008FC RID: 2300 RVA: 0x00056682 File Offset: 0x00054882
		public ParticleSmaller(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData, textureName, Position, Velocity, Direction)
		{
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00056694 File Offset: 0x00054894
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time > 0)
			{
				base.Scale -= 1f / (float)base.LifeTime;
			}
			if (base.Scale < 0.2f)
			{
				base.Scale = 0.2f;
			}
		}
	}
}
