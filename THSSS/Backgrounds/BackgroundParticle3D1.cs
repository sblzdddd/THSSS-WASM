using System.Drawing;

namespace Shooting
{
	public class BackgroundParticle3D1 : BaseParticle3D
	{
		public BackgroundParticle3D1(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int LifeTime)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			base.Background3D.BackgroundParticleList.Add(this);
			base.TransparentValueF = 0f;
			base.LifeTime = LifeTime;
		}

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

		private float TransF = 0f;
	}
}
