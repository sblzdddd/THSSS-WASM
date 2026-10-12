using System.Drawing;

namespace Shooting
{
	public class MyBullet_AyaSub : BaseMyBullet_Touhou
	{
		public MyBullet_AyaSub(StageDataPackage StageData, PointF p)
			: base(StageData, "AyaSubBullet00", p, 2f, -1.5707963705062866)
		{
			base.Damage = 25;
			base.Region = 21;
			base.Accelerate = 2f;
			base.Region = 25;
		}

		public override void GiveEndEffect()
		{
			new Emitter2(base.StageData, base.Position);
			new ParticleEndAyaBullet(base.StageData, "AyaSubBullet", base.OriginalPosition, 3f, base.Direction);
		}
	}
}
