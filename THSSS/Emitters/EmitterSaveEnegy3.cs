using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200006A RID: 106
	internal class EmitterSaveEnegy3 : BaseEffect
	{
		// Token: 0x060003C7 RID: 967 RVA: 0x0001A41D File Offset: 0x0001861D
		public EmitterSaveEnegy3(StageDataPackage StageData, PointF Position)
			: base(StageData)
		{
			base.Position = Position;
			base.Velocity = 0f;
			base.Direction = 0.0;
			base.LifeTime = 90;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0001A458 File Offset: 0x00018658
		public override void Shoot()
		{
			if (base.Time % 5 == 0)
			{
				for (int i = 0; i < 5; i++)
				{
					double num = (double)((float)base.Ran.Next(360) / 180f) * 3.141592653589793;
					int num2 = base.Ran.Next(10, 100);
					PointF pointF = new PointF((float)((double)base.Position.X + (double)num2 * Math.Cos(num)), (float)((double)base.Position.Y + (double)num2 * Math.Sin(num)));
					ParticleLongerByV particleLongerByV = new ParticleLongerByV(base.StageData, "590-1", pointF, 0f, num + 3.141592653589793)
					{
						TransparentValueF = 200f,
						LifeTime = 20,
						Scale = (float)base.Ran.Next(20, 50) / 100f
					};
					particleLongerByV.Velocity = (float)num2 / (float)particleLongerByV.LifeTime;
				}
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0001A579 File Offset: 0x00018779
		public override void Show()
		{
		}
	}
}
