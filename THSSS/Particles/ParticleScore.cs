using System.Drawing;

namespace Shooting
{
	internal class ParticleScore : BaseEffect
	{
		public ParticleScore(StageDataPackage StageData, string textureName, PointF Position)
			: base(StageData, textureName, Position, 1f, -1.5707963705062866)
		{
			base.LifeTime = 50;
			base.Active = false;
		}

		public override void Ctrl()
		{
			base.Angle = -base.Direction + 1.5707963267948966;
			base.Ctrl();
			if (base.Time > 0)
			{
				base.TransparentValueF -= (float)(255 / base.LifeTime);
			}
		}
	}
}
