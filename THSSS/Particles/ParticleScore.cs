using System.Drawing;

namespace Shooting
{
	internal class ParticleScore : BaseEffect
	{
		public ParticleScore(StageDataPackage StageData, string textureName, PointF Position)
			: base(StageData, textureName, Position, 1f, -1.5707963705062866)
		{
			LifeTime = 50;
			Active = false;
		}

		public override void Ctrl()
		{
			Angle = -Direction + 1.5707963267948966;
			base.Ctrl();
			if (Time > 0)
			{
				TransparentValueF -= (float)(255 / LifeTime);
			}
		}
	}
}
