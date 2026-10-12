using System;
using System.Drawing;

namespace Shooting
{
	internal class SpellCardAttack : BaseEffect
	{
		public SpellCardAttack(StageDataPackage StageData)
			: base(StageData)
		{
			LifeTime = 2;
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 6; j++)
				{
					PointF pointF = new PointF(-50f + (float)(j * 128) * (float)Math.Cos(0.5235987755982988), 200f - (float)(j * 128) * (float)Math.Sin(0.5235987755982988) + (float)(i * 35));
					BaseObject baseObject = new BaseObject(StageData, "SpellCircle06", new PointF(0f, 0f), 2f, -0.5235987755982988)
					{
						OriginalPosition = pointF,
						Angle = -3.141592653589793,
						LifeTime = 50
					};
					baseObject.TransparentVelocityDictionary.Add(30, -13f);
					if (i % 2 == 0)
					{
						baseObject.Velocity *= -1f;
					}
					Background2.BackgroundList.Add(baseObject);
				}
			}
		}
	}
}
