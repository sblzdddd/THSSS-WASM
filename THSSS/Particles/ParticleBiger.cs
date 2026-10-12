using System.Drawing;

namespace Shooting
{
	internal class ParticleBiger : BaseEffect
	{
		public ParticleBiger(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData, textureName, Position, Velocity, Direction)
		{
			base.LifeTime = 30;
			base.Scale = 0.5f;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time > 0)
			{
				base.TransparentValueF -= (float)(base.MaxTransparent / base.LifeTime);
				base.Scale += 0.05f;
			}
		}
	}
}
