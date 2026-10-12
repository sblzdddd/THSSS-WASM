using System;
using System.Drawing;

namespace Shooting
{
	public class BossStar : BaseEffect
	{
		public BossStar(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			this.OriginalPosition = OriginalPosition;
			LifeTime = 130;
		}

		public override void Shoot()
		{
			if (Time == 1)
			{
				int num = 110;
				double num2 = 1.5707963267948966;
				for (int i = 0; i < 3; i++)
				{
					PointF pointF = new PointF((float)((double)OriginalPosition.X + (double)num * Math.Cos(num2)), (float)((double)OriginalPosition.Y + (double)num * Math.Sin(num2)));
					BossStarEmitter01 bossStarEmitter = new BossStarEmitter01(StageData, pointF);
					bossStarEmitter.Velocity = 2.8f;
					bossStarEmitter.DirectionDegree = num2 * 180.0 / 3.141592653589793 + 45.0;
					bossStarEmitter.DirectionVelocityDegree = 2f;
					bossStarEmitter.ColorType = ColorType;
					num2 += 2.0943951023931953;
				}
			}
			else if (Time == LifeTime)
			{
				BaseEffect baseEffect = new BaseEffect(StageData, "Light0" + ColorType.ToString(), Position, 0f, 0.0);
				baseEffect.LifeTime = 50;
				baseEffect.ScaleVelocity = 0.4f;
				baseEffect.TransparentVelocityDictionary.Add(20, -5f);
			}
		}

		public int ColorType;
	}
}
