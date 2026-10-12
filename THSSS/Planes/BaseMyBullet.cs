using System.Drawing;

namespace Shooting
{
	public class BaseMyBullet : BaseObject
	{
		public BaseMyBullet(StageDataPackage StageData, string textureName, PointF Position, float v, double drctn)
			: base(StageData, textureName, Position, v, drctn)
		{
			base.Damage = 21;
			base.Region = 15;
			base.MyBulletList.Add(this);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (this.OutBoundary())
			{
				base.MyBulletList.Remove(this);
			}
		}

		public override void GiveEndEffect()
		{
			new Emitter2(base.StageData, base.Position);
			ParticleBiger particleBiger = new ParticleBiger(base.StageData, null, base.Position, 3f, base.Direction);
			particleBiger.Scale = 1.2f;
			particleBiger.LifeTime = 15;
			particleBiger.TxtureObject = base.TxtureObject;
			particleBiger.Angle = base.Angle;
			particleBiger.MaxTransparent = 100;
		}
	}
}
