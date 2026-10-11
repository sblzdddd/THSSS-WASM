using System;
using System.Drawing;

namespace Shooting
{
	public class Particle3D4 : BaseParticle3D
	{
		public Particle3D4(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int LifeTime)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			base.Active = true;
			StageData.Particle3D.ParticleList.Add(this);
			base.MaxTransparent = 128;
			base.TransparentValueF = (float)base.MaxTransparent;
			base.LifeTime = LifeTime;
			base.Accelerate = (float)base.Ran.Next(10) / 100f;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == 1)
			{
				this.maxScale = base.Scale;
			}
			else if (base.LifeTime * 3 / 4 < base.Time && base.Time <= base.LifeTime)
			{
				base.Scale -= this.maxScale * 4f / (float)base.LifeTime;
			}
			float num = 8f / base.Vy;
			base.Vx = 10f * (float)Math.Sin((double)((float)base.Time / num));
			base.Depth += 10f * (float)Math.Cos((double)((float)base.Time / num));
		}

		private float maxScale;
	}
}
