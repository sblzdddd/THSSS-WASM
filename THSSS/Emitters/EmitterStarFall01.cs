using System;
using System.Drawing;

namespace Shooting
{
	internal class EmitterStarFall01 : BaseEffect
	{
		public EmitterStarFall01(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, OriginalPosition, 0f, 0.0)
		{
			base.Velocity = 4f;
			base.MaxVelocity = 8f;
			base.LifeTime = base.Ran.Next(120, 160);
			base.DirectionDegree = (double)base.Ran.Next(140, 150);
			this.ScaleLengthVelocityDictionary.Add(base.LifeTime - 50, -0.02f);
		}

		public override void Shoot()
		{
			for (int i = 0; i < 3; i++)
			{
				float num = 0f;
				double num2 = (double)base.Ran.Next(360) * 3.141592653589793 / 180.0;
				PointF pointF = new PointF(base.OriginalPosition.X + num * (float)Math.Sin(num2), base.OriginalPosition.Y + num * (float)Math.Cos(num2));
				ParticleTitleStar particleTitleStar = new ParticleTitleStar(base.StageData, "Star", pointF, (float)base.StageData.Ran.Next(1, 200) / 1000f, (double)base.Ran.Next(360) * 3.141592653589793 / 180.0);
				particleTitleStar.Active = true;
				particleTitleStar.Scale = (float)base.StageData.Ran.Next(40, 200) / 1000f * base.Scale;
				particleTitleStar.LifeTime = 80;
			}
		}
	}
}
