using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000A1 RID: 161
	public class MyBullet_Aya : BaseMyBullet_Touhou
	{
		// Token: 0x060004C8 RID: 1224 RVA: 0x00025324 File Offset: 0x00023524
		public MyBullet_Aya(StageDataPackage StageData, PointF p)
			: base(StageData, "AyaBullet00", p, 30f, -1.5707963705062866)
		{
			base.Damage = 20;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0002534D File Offset: 0x0002354D
		public override void GiveEndEffect()
		{
			new Emitter2(base.StageData, base.Position);
			new ParticleEndAyaBullet(base.StageData, "AyaBullet", base.OriginalPosition, 3f, base.Direction);
		}
	}
}
