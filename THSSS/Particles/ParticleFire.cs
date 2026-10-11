using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200003E RID: 62
	internal class ParticleFire : BaseEffect
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000E294 File Offset: 0x0000C494
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000E2AB File Offset: 0x0000C4AB
		public int ColorType { get; set; }

		// Token: 0x0600023F RID: 575 RVA: 0x0000E2B4 File Offset: 0x0000C4B4
		public ParticleFire(StageDataPackage StageData, PointF Position, float Velocity, double Direcity)
			: base(StageData, null, Position, Velocity, Direcity)
		{
			base.LifeTime = 31;
			base.Time = base.Ran.Next(32);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000E2E4 File Offset: 0x0000C4E4
		public override void Ctrl()
		{
			base.Ctrl();
			switch (this.ColorType)
			{
			case 0:
				base.TxtureObject = base.TextureObjectDictionary["FY_Particle0_" + (base.Time % 32 / 4).ToString()];
				break;
			case 1:
				base.TxtureObject = base.TextureObjectDictionary["FY_Particle1_" + (base.Time % 32 / 4).ToString()];
				break;
			case 2:
				base.TxtureObject = base.TextureObjectDictionary["FY_Particle2_" + (base.Time % 32 / 4).ToString()];
				break;
			}
		}
	}
}
