using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200002A RID: 42
	public class BaseBulleEmitter : BaseEnemyPlane
	{
		// Token: 0x060001DB RID: 475 RVA: 0x0000B190 File Offset: 0x00009390
		public BaseBulleEmitter(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.OriginalPosition = new PointF(0f, 0f);
			base.Velocity = 0f;
			base.Direction = 0.0;
			base.HealthPoint = 1000000f;
			base.HitEnabled = false;
			base.EnemyPlaneList.Add(this);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000B204 File Offset: 0x00009404
		public override bool HitCheck(BaseObject MyPlane)
		{
			return false;
		}
	}
}
