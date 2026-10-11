using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200018B RID: 395
	internal class Emitter : BaseEffect
	{
		// Token: 0x06000959 RID: 2393 RVA: 0x0005A8ED File Offset: 0x00058AED
		public Emitter(StageDataPackage StageData, PointF Position)
			: base(StageData)
		{
			base.Position = Position;
			base.Velocity = 0f;
			base.Direction = 0.0;
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0005A920 File Offset: 0x00058B20
		public override void Shoot()
		{
			for (int i = 0; i < 10; i++)
			{
				string text = ((base.MyPlane.Name == "Aya") ? "MyParticle" : "MyParticle01");
				ParticleSmaller particleSmaller = new ParticleSmaller(base.StageData, text, base.Position, (float)(base.Ran.Next(20, 200) / 10), (double)((float)base.Ran.Next(360) / 180f) * 3.141592653589793);
				particleSmaller.LifeTime = 20;
				particleSmaller.Scale = (float)base.Ran.Next(100) / 100f;
				particleSmaller.TransparentValueF = 200f;
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0005A9E8 File Offset: 0x00058BE8
		public override bool OutBoundary()
		{
			return base.Time >= 4;
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0005AA0E File Offset: 0x00058C0E
		public override void Show()
		{
		}
	}
}
