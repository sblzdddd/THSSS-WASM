using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000073 RID: 115
	internal class EmitterStageTitleStar : BaseEffect
	{
		// Token: 0x06000400 RID: 1024 RVA: 0x0001B7C0 File Offset: 0x000199C0
		public EmitterStageTitleStar(StageDataPackage StageData)
			: base(StageData, null, new PointF(0f, 0f), 0f, 0.0)
		{
			base.OriginalPosition = new PointF(0f, (float)(base.BoundRect.Height / 2 - 50));
			base.Velocity = 4f;
			base.MaxVelocity = 8f;
			base.AccelerateCS = 0.08f;
			base.AccDirection = 0.0;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001B850 File Offset: 0x00019A50
		public override void Shoot()
		{
			if (base.Time % 1 == 0)
			{
				for (int i = 0; i < 4; i++)
				{
					float num = (float)base.Ran.Next(0, 15);
					double num2 = (double)base.Ran.Next(360) * 3.141592653589793 / 180.0;
					PointF pointF = new PointF(base.OriginalPosition.X + num * (float)Math.Sin(num2), base.OriginalPosition.Y + num * (float)Math.Cos(num2));
					Particle particle = new Particle(base.StageData, "Star", pointF, (float)base.StageData.Ran.Next(1, 110) / 100f, (double)base.Ran.Next(360) * 3.141592653589793 / 180.0);
					particle.Active = true;
					particle.Scale = (float)base.StageData.Ran.Next(15, 40) / 100f;
					particle.LifeTime = 80;
				}
			}
		}
	}
}
