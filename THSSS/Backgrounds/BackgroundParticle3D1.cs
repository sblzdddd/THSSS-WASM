using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200013C RID: 316
	public class BackgroundParticle3D1 : BaseParticle3D
	{
		// Token: 0x06000845 RID: 2117 RVA: 0x0004CB10 File Offset: 0x0004AD10
		public BackgroundParticle3D1(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int LifeTime)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			base.Background3D.BackgroundParticleList.Add(this);
			base.TransparentValueF = 0f;
			base.LifeTime = LifeTime;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0004CB60 File Offset: 0x0004AD60
		public override void Ctrl()
		{
			base.Ctrl();
			if (0 < base.Time && base.Time < base.LifeTime / 3)
			{
				this.TransF += (float)base.MaxTransparent * 3f / (float)base.LifeTime;
				base.TransparentValueF = (float)((int)this.TransF);
			}
			else if (base.LifeTime * 2 / 3 < base.Time && base.Time < base.LifeTime)
			{
				this.TransF -= (float)base.MaxTransparent * 3f / (float)base.LifeTime;
				base.TransparentValueF = (float)((int)this.TransF);
			}
		}

		// Token: 0x040002F2 RID: 754
		private float TransF = 0f;
	}
}
