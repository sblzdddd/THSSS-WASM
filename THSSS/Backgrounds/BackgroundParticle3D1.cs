using System.Drawing;

namespace Shooting
{
	public class BackgroundParticle3D1 : BaseParticle3D
	{
		public BackgroundParticle3D1(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int LifeTime)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			Background3D.BackgroundParticleList.Add(this);
			TransparentValueF = 0f;
			this.LifeTime = LifeTime;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (0 < Time && Time < LifeTime / 3)
			{
				TransF += (float)MaxTransparent * 3f / (float)LifeTime;
				TransparentValueF = (float)((int)TransF);
			}
			else if (LifeTime * 2 / 3 < Time && Time < LifeTime)
			{
				TransF -= (float)MaxTransparent * 3f / (float)LifeTime;
				TransparentValueF = (float)((int)TransF);
			}
		}

		private float TransF = 0f;
	}
}
