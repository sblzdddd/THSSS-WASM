using System;
using System.Drawing;

namespace Shooting
{
	public class MyBullet_MarisaSub : BaseMyBullet_Touhou
	{
		public MyBullet_MarisaSub(StageDataPackage StageData, PointF p)
			: base(StageData, "MarisaMisile10", p, 4f, -1.5707963705062866)
		{
			base.Damage = 5;
			base.Region = 15;
			base.Accelerate = 0.5f;
		}

		public override void GiveEndEffect()
		{
			new Emitter2(base.StageData, base.Position);
			new ParticleEndAyaBullet(base.StageData, "MarisaMisile", base.OriginalPosition, 3f, base.Direction);
		}
	}
}
