using System;
using System.Drawing;

namespace Shooting
{
	internal class ParticleSmaller : BaseParticle
	{
		public ParticleSmaller(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData, textureName, Position, Velocity, Direction)
		{
		}

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
