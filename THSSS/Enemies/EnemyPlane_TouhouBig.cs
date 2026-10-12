using System.Drawing;

namespace Shooting
{
	internal class EnemyPlane_TouhouBig : BaseEnemyPlane_Touhou
	{
		public EnemyPlane_TouhouBig(StageDataPackage StageData, string EnemyName, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: base(StageData, EnemyName, OriginalPosition, Velocity, Direction, ColorType)
		{
			base.HealthPoint = 600f;
			base.ItemCount = 4;
			base.Region = 10;
		}

		public EnemyPlane_TouhouBig(StageDataPackage StageData, string EnemyName, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, EnemyName, OriginalPosition, Velocity, Direction)
		{
			base.HealthPoint = 600f;
			base.ItemCount = 4;
			base.Region = 10;
		}
	}
}
