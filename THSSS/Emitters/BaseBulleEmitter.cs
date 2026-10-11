using System;
using System.Drawing;

namespace Shooting
{
	public class BaseBulleEmitter : BaseEnemyPlane
	{
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

		public override bool HitCheck(BaseObject MyPlane)
		{
			return false;
		}
	}
}
