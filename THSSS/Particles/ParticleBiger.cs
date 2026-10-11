using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200003D RID: 61
	internal class ParticleBiger : BaseEffect
	{
		// Token: 0x0600023B RID: 571 RVA: 0x0000E213 File Offset: 0x0000C413
		public ParticleBiger(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData, textureName, Position, Velocity, Direction)
		{
			base.LifeTime = 30;
			base.Scale = 0.5f;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000E23C File Offset: 0x0000C43C
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time > 0)
			{
				base.TransparentValueF -= (float)(base.MaxTransparent / base.LifeTime);
				base.Scale += 0.05f;
			}
		}
	}
}
