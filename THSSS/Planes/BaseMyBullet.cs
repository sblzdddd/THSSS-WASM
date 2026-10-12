using System.Drawing;

namespace Shooting
{
	public class BaseMyBullet : BaseObject
	{
		public BaseMyBullet(StageDataPackage StageData, string textureName, PointF Position, float v, double drctn)
			: base(StageData, textureName, Position, v, drctn)
		{
			Damage = 21;
			Region = 15;
			MyBulletList.Add(this);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (OutBoundary())
			{
				MyBulletList.Remove(this);
			}
		}

		public override void GiveEndEffect()
		{
			new Emitter2(StageData, Position);
			var particleBiger = new ParticleBiger(StageData, null, Position, 3f, Direction);
			particleBiger.Scale = 1.2f;
			particleBiger.LifeTime = 15;
			particleBiger.TxtureObject = TxtureObject;
			particleBiger.Angle = Angle;
			particleBiger.MaxTransparent = 100;
		}
	}
}
