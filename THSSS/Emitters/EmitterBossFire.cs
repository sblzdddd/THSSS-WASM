using System;
using System.Drawing;

namespace Shooting
{
	internal class EmitterBossFire : BaseEffect
	{
		public EmitterBossFire(StageDataPackage StageData, PointF OriginalPosition, Color ColorValue)
			: base(StageData, null, OriginalPosition, 0f, 0.0)
		{
			base.OriginalPosition = OriginalPosition;
			base.ColorValue = ColorValue;
		}

		public override void Move()
		{
			if (base.Boss != null)
			{
				base.OriginalPosition = base.Boss.OriginalPosition;
			}
		}

		public override void Shoot()
		{
			if (base.Time % 10 == 0)
			{
				ParticleBossFire particleBossFire = new ParticleBossFire(base.StageData, "Effect01_1", base.OriginalPosition, 0f, 0.0, 60);
				particleBossFire.ColorValue = base.ColorValue;
				ParticleBossFire2 particleBossFire2 = new ParticleBossFire2(base.StageData, "Effect01_2", base.OriginalPosition, 0f, (double)base.Ran.Next(360) * 3.141592653589793 / 180.0, 80);
				particleBossFire2.ColorValue = base.ColorValue;
				ParticleBossFire2 particleBossFire3 = new ParticleBossFire2(base.StageData, "Effect01_2", base.OriginalPosition, 0f, (double)base.Ran.Next(360) * 3.141592653589793 / 180.0, 100);
				particleBossFire3.ColorValue = base.ColorValue;
			}
		}

		public override bool OutBoundary()
		{
			return base.Boss == null;
		}
	}
}
