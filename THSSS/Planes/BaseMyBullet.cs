using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000038 RID: 56
	public class BaseMyBullet : BaseObject
	{
		// Token: 0x0600022C RID: 556 RVA: 0x0000D9A2 File Offset: 0x0000BBA2
		public BaseMyBullet(StageDataPackage StageData, string textureName, PointF Position, float v, double drctn)
			: base(StageData, textureName, Position, v, drctn)
		{
			base.Damage = 21;
			base.Region = 15;
			base.MyBulletList.Add(this);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000D9D4 File Offset: 0x0000BBD4
		public override void Ctrl()
		{
			base.Ctrl();
			if (this.OutBoundary())
			{
				base.MyBulletList.Remove(this);
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000DA08 File Offset: 0x0000BC08
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
