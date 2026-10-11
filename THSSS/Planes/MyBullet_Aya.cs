using System;
using System.Drawing;

namespace Shooting
{
	public class MyBullet_Aya : BaseMyBullet_Touhou
	{
		public MyBullet_Aya(StageDataPackage StageData, PointF p)
			: base(StageData, "AyaBullet00", p, 30f, -1.5707963705062866)
		{
			base.Damage = 20;
		}

		public override void GiveEndEffect()
		{
			new Emitter2(base.StageData, base.Position);
			new ParticleEndAyaBullet(base.StageData, "AyaBullet", base.OriginalPosition, 3f, base.Direction);
		}
	}
}
