using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000E4 RID: 228
	public class Particle3D3 : BaseParticle3D
	{
		// Token: 0x0600062D RID: 1581 RVA: 0x00037110 File Offset: 0x00035310
		public Particle3D3(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int LifeTime)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			base.Active = true;
			StageData.Particle3D.ParticleList.Add(this);
			base.MaxTransparent = 128;
			base.TransparentValueF = (float)base.MaxTransparent;
			base.LifeTime = LifeTime;
			base.Accelerate = -Velocity / (float)LifeTime;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00037178 File Offset: 0x00035378
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == 1)
			{
				this.maxScale = base.Scale;
			}
			else if (base.LifeTime / 2 < base.Time && base.Time <= base.LifeTime)
			{
				base.Scale -= this.maxScale * 2f / (float)base.LifeTime;
			}
		}

		// Token: 0x04000222 RID: 546
		private float maxScale;
	}
}
