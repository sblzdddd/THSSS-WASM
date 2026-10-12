using System.Drawing;

namespace Shooting
{
	public class Particle3D2 : BaseParticle3D
	{
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
