using System.Drawing;

namespace Shooting
{
	public class BaseMyBullet_Touhou : BaseMyBullet
	{
		public BaseMyBullet_Touhou(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, textureName, default(Point), Velocity, Direction)
		{
			base.Angle = 1.5707963705062866;
			base.TransparentValueF = 180f;
			base.OriginalPosition = OriginalPosition;
		}
	}
}
