using System.Drawing;

namespace Shooting
{
	internal class EmitterBossFire : BaseEffect
	{
		public EmitterBossFire(StageDataPackage StageData, PointF OriginalPosition, Color ColorValue)
			: base(StageData, null, OriginalPosition, 0f, 0.0)
		{
			this.OriginalPosition = OriginalPosition;
			this.ColorValue = ColorValue;
		}

		public override void Move()
		{
			if (Boss != null)
			{
				OriginalPosition = Boss.OriginalPosition;
			}
		}

		public override void Shoot()
		{
			if (Time % 10 == 0)
			{
				var particleBossFire = new ParticleBossFire(StageData, "Effect01_1", OriginalPosition, 0f, 0.0, 60);
				particleBossFire.ColorValue = ColorValue;
				var particleBossFire2 = new ParticleBossFire2(StageData, "Effect01_2", OriginalPosition, 0f, (double)Ran.Next(360) * 3.141592653589793 / 180.0, 80);
				particleBossFire2.ColorValue = ColorValue;
				var particleBossFire3 = new ParticleBossFire2(StageData, "Effect01_2", OriginalPosition, 0f, (double)Ran.Next(360) * 3.141592653589793 / 180.0, 100);
				particleBossFire3.ColorValue = ColorValue;
			}
		}

		public override bool OutBoundary()
		{
			return Boss == null;
		}
	}
}
