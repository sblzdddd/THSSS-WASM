using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000B3 RID: 179
	internal class EmitterSaveEnegy : BaseEffect
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x00029C0C File Offset: 0x00027E0C
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x00029C23 File Offset: 0x00027E23
		public int ColorType { get; set; }

		// Token: 0x06000526 RID: 1318 RVA: 0x00029C2C File Offset: 0x00027E2C
		public EmitterSaveEnegy(StageDataPackage StageData, PointF Position, int colorType)
			: base(StageData)
		{
			base.Position = Position;
			base.Velocity = 0f;
			base.Direction = 0.0;
			base.LifeTime = 200;
			this.ColorType = colorType;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00029C7C File Offset: 0x00027E7C
		public override void Shoot()
		{
			if (base.Time % 5 == 0)
			{
				for (int i = 0; i < 5; i++)
				{
					double num = (double)((float)base.Ran.Next(360) / 180f) * 3.141592653589793;
					int num2 = base.Ran.Next(10, 100);
					PointF pointF = new PointF((float)((double)base.Position.X + (double)num2 * Math.Cos(num)), (float)((double)base.Position.Y + (double)num2 * Math.Sin(num)));
					ParticleSmaller particleSmaller = new ParticleSmaller(base.StageData, "MyBullet000" + this.ColorType.ToString(), pointF, 0f, num + 3.141592653589793);
					particleSmaller.TransparentValueF = 200f;
					particleSmaller.LifeTime = 50;
					particleSmaller.Velocity = (float)num2 / (float)particleSmaller.LifeTime;
					particleSmaller.Scale = (float)base.Ran.Next(20, 50) / 100f;
				}
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00029DAB File Offset: 0x00027FAB
		public override void Show()
		{
		}
	}
}
