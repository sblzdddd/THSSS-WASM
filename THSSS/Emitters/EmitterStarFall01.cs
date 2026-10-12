using System;
using System.Drawing;

namespace Shooting
{
	internal class EmitterStarFall01 : BaseEffect
	{
		public EmitterStarFall01(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, OriginalPosition, 0f, 0.0)
		{
			Velocity = 4f;
			MaxVelocity = 8f;
			LifeTime = Ran.Next(120, 160);
			DirectionDegree = (double)Ran.Next(140, 150);
			ScaleLengthVelocityDictionary.Add(LifeTime - 50, -0.02f);
		}

		public override void Shoot()
		{
			for (var i = 0; i < 3; i++)
			{
				var num = 0f;
				var num2 = (double)Ran.Next(360) * 3.141592653589793 / 180.0;
				var pointF = new PointF(OriginalPosition.X + num * (float)Math.Sin(num2), OriginalPosition.Y + num * (float)Math.Cos(num2));
				var particleTitleStar = new ParticleTitleStar(StageData, "Star", pointF, (float)StageData.Ran.Next(1, 200) / 1000f, (double)Ran.Next(360) * 3.141592653589793 / 180.0);
				particleTitleStar.Active = true;
				particleTitleStar.Scale = (float)StageData.Ran.Next(40, 200) / 1000f * Scale;
				particleTitleStar.LifeTime = 80;
			}
		}
	}
}
