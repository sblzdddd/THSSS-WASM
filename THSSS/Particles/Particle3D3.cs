using System.Drawing;

namespace Shooting
{
	public class Particle3D3 : BaseParticle3D
	{
		public Particle3D3(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int LifeTime)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			Active = true;
			StageData.Particle3D.ParticleList.Add(this);
			MaxTransparent = 128;
			TransparentValueF = (float)MaxTransparent;
			this.LifeTime = LifeTime;
			Accelerate = -Velocity / (float)LifeTime;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time == 1)
			{
				maxScale = Scale;
			}
			else if (LifeTime / 2 < Time && Time <= LifeTime)
			{
				Scale -= maxScale * 2f / (float)LifeTime;
			}
		}

		private float maxScale;
	}
}
