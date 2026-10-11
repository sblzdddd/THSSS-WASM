using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200004A RID: 74
	public class BossPetal : BaseEffect
	{
		// Token: 0x0600027B RID: 635 RVA: 0x0001027D File Offset: 0x0000E47D
		public BossPetal(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			base.OriginalPosition = OriginalPosition;
			base.LifeTime = 130;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000102B8 File Offset: 0x0000E4B8
		public override void Shoot()
		{
			if (base.Time == 1)
			{
				int num = 110;
				double num2 = 1.5707963267948966;
				for (int i = 0; i < 3; i++)
				{
					PointF pointF = new PointF((float)((double)base.OriginalPosition.X + (double)num * Math.Cos(num2)), (float)((double)base.OriginalPosition.Y + (double)num * Math.Sin(num2)));
					BossPetalEmitter01 bossPetalEmitter = new BossPetalEmitter01(base.StageData, pointF);
					bossPetalEmitter.Velocity = 2f;
					bossPetalEmitter.DirectionDegree = num2 * 180.0 / 3.141592653589793 + 35.0;
					bossPetalEmitter.DirectionVelocityDegree = 2f;
					num2 += 2.0943951023931953;
				}
			}
			else if (base.Time == base.LifeTime)
			{
				BaseEffect baseEffect = new BaseEffect(base.StageData, "591-1", base.Position, 0f, 0.0);
				baseEffect.LifeTime = 50;
				baseEffect.ScaleVelocity = 0.4f;
				baseEffect.TransparentVelocityDictionary.Add(20, -5f);
			}
		}
	}
}
