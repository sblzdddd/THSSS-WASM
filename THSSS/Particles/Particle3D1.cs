using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000121 RID: 289
	public class Particle3D1 : BaseParticle3D
	{
		// Token: 0x060007A6 RID: 1958 RVA: 0x0004713C File Offset: 0x0004533C
		public Particle3D1(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int LifeTime)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			StageData.Particle3D.ParticleList.Add(this);
			base.Active = true;
			base.MaxTransparent = 128;
			base.TransparentValueF = 0f;
			base.LifeTime = LifeTime;
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00047194 File Offset: 0x00045394
		public override void Ctrl()
		{
			base.Ctrl();
			if (0 < base.Time && base.Time < base.LifeTime / 3)
			{
				base.TransparentValueF += (float)(base.MaxTransparent * 3 / base.LifeTime);
			}
			else if (base.LifeTime * 2 / 3 < base.Time && base.Time < base.LifeTime)
			{
				base.TransparentValueF -= (float)(base.MaxTransparent * 3 / base.LifeTime);
			}
		}
	}
}
