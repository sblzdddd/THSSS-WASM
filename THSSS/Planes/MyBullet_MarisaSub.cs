using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200003A RID: 58
	public class MyBullet_MarisaSub : BaseMyBullet_Touhou
	{
		// Token: 0x06000230 RID: 560 RVA: 0x0000DACE File Offset: 0x0000BCCE
		public MyBullet_MarisaSub(StageDataPackage StageData, PointF p)
			: base(StageData, "MarisaMisile10", p, 4f, -1.5707963705062866)
		{
			base.Damage = 5;
			base.Region = 15;
			base.Accelerate = 0.5f;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000DB0B File Offset: 0x0000BD0B
		public override void GiveEndEffect()
		{
			new Emitter2(base.StageData, base.Position);
			new ParticleEndAyaBullet(base.StageData, "MarisaMisile", base.OriginalPosition, 3f, base.Direction);
		}
	}
}
