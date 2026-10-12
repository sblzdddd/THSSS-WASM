using System.Drawing;

namespace Shooting
{
	public class Particle3D1 : BaseParticle3D
	{
		public Particle3D1(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int LifeTime)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			StageData.Particle3D.ParticleList.Add(this);
			base.Active = true;
			base.MaxTransparent = 128;
			base.TransparentValueF = 0f;
			base.LifeTime = LifeTime;
		}

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
