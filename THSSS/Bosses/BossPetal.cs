using System;
using System.Drawing;

namespace Shooting
{
	public class BossPetal : BaseEffect
	{
		public BossPetal(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			this.OriginalPosition = OriginalPosition;
			LifeTime = 130;
		}

		public override void Shoot()
		{
			if (Time == 1)
			{
				var num = 110;
				var num2 = 1.5707963267948966;
				for (var i = 0; i < 3; i++)
				{
					var pointF = new PointF((float)((double)OriginalPosition.X + (double)num * Math.Cos(num2)), (float)((double)OriginalPosition.Y + (double)num * Math.Sin(num2)));
					var bossPetalEmitter = new BossPetalEmitter01(StageData, pointF);
					bossPetalEmitter.Velocity = 2f;
					bossPetalEmitter.DirectionDegree = num2 * 180.0 / 3.141592653589793 + 35.0;
					bossPetalEmitter.DirectionVelocityDegree = 2f;
					num2 += 2.0943951023931953;
				}
			}
			else if (Time == LifeTime)
			{
				var baseEffect = new BaseEffect(StageData, "591-1", Position, 0f, 0.0);
				baseEffect.LifeTime = 50;
				baseEffect.ScaleVelocity = 0.4f;
				baseEffect.TransparentVelocityDictionary.Add(20, -5f);
			}
		}
	}
}
