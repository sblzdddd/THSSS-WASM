using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001F9 RID: 505
	public class MyBullet_AyaSub : BaseMyBullet_Touhou
	{
		// Token: 0x06000B69 RID: 2921 RVA: 0x00071834 File Offset: 0x0006FA34
		public MyBullet_AyaSub(StageDataPackage StageData, PointF p)
			: base(StageData, "AyaSubBullet00", p, 2f, -1.5707963705062866)
		{
			base.Damage = 25;
			base.Region = 21;
			base.Accelerate = 2f;
			base.Region = 25;
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00071886 File Offset: 0x0006FA86
		public override void GiveEndEffect()
		{
			new Emitter2(base.StageData, base.Position);
			new ParticleEndAyaBullet(base.StageData, "AyaSubBullet", base.OriginalPosition, 3f, base.Direction);
		}
	}
}
