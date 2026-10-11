using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000175 RID: 373
	public class BossStar : BaseEffect
	{
		// Token: 0x0600091C RID: 2332 RVA: 0x00057A8F File Offset: 0x00055C8F
		public BossStar(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			base.OriginalPosition = OriginalPosition;
			base.LifeTime = 130;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00057ACC File Offset: 0x00055CCC
		public override void Shoot()
		{
			if (base.Time == 1)
			{
				int num = 110;
				double num2 = 1.5707963267948966;
				for (int i = 0; i < 3; i++)
				{
					PointF pointF = new PointF((float)((double)base.OriginalPosition.X + (double)num * Math.Cos(num2)), (float)((double)base.OriginalPosition.Y + (double)num * Math.Sin(num2)));
					BossStarEmitter01 bossStarEmitter = new BossStarEmitter01(base.StageData, pointF);
					bossStarEmitter.Velocity = 2.8f;
					bossStarEmitter.DirectionDegree = num2 * 180.0 / 3.141592653589793 + 45.0;
					bossStarEmitter.DirectionVelocityDegree = 2f;
					bossStarEmitter.ColorType = this.ColorType;
					num2 += 2.0943951023931953;
				}
			}
			else if (base.Time == base.LifeTime)
			{
				BaseEffect baseEffect = new BaseEffect(base.StageData, "Light0" + this.ColorType.ToString(), base.Position, 0f, 0.0);
				baseEffect.LifeTime = 50;
				baseEffect.ScaleVelocity = 0.4f;
				baseEffect.TransparentVelocityDictionary.Add(20, -5f);
			}
		}

		// Token: 0x0400035E RID: 862
		public int ColorType;
	}
}
