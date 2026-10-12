using System.Drawing;

namespace Shooting
{
	internal class BaseParticle : BaseEffect
	{
		public BaseParticle()
		{
		}

		public BaseParticle(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData, textureName, Position, Velocity, Direction)
		{
			base.LifeTime = 100;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time > 0)
			{
				base.TransparentValueF -= (float)(255 / base.LifeTime);
			}
		}
	}
}
