using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000105 RID: 261
	public class Particle3D2 : BaseParticle3D
	{
		// Token: 0x0600072A RID: 1834 RVA: 0x00041208 File Offset: 0x0003F408
		public Particle3D2(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int LifeTime)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			base.Active = true;
			StageData.Particle3D.ParticleList.Add(this);
			base.MaxTransparent = 128;
			base.TransparentValueF = (float)base.MaxTransparent;
			base.LifeTime = LifeTime;
			base.Accelerate = -Velocity / (float)LifeTime;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00041270 File Offset: 0x0003F470
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.LifeTime * 2 / 3 < base.Time && base.Time <= base.LifeTime)
			{
				base.TransparentValueF -= (float)(base.MaxTransparent * 3 / base.LifeTime);
			}
			if (base.Time == base.LifeTime * 2 / 3)
			{
				base.ScaleVelocity = -0.1f;
			}
		}
	}
}
