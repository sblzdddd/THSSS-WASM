using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000BA RID: 186
	internal class EnemyPlane_TouhouBig : BaseEnemyPlane_Touhou
	{
		// Token: 0x0600054F RID: 1359 RVA: 0x0002E10A File Offset: 0x0002C30A
		public EnemyPlane_TouhouBig(StageDataPackage StageData, string EnemyName, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: base(StageData, EnemyName, OriginalPosition, Velocity, Direction, ColorType)
		{
			base.HealthPoint = 600f;
			base.ItemCount = 4;
			base.Region = 10;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0002E13B File Offset: 0x0002C33B
		public EnemyPlane_TouhouBig(StageDataPackage StageData, string EnemyName, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, EnemyName, OriginalPosition, Velocity, Direction)
		{
			base.HealthPoint = 600f;
			base.ItemCount = 4;
			base.Region = 10;
		}
	}
}
