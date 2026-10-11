using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000039 RID: 57
	public class BaseMyBullet_Touhou : BaseMyBullet
	{
		// Token: 0x0600022F RID: 559 RVA: 0x0000DA80 File Offset: 0x0000BC80
		public BaseMyBullet_Touhou(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, textureName, default(Point), Velocity, Direction)
		{
			base.Angle = 1.5707963705062866;
			base.TransparentValueF = 180f;
			base.OriginalPosition = OriginalPosition;
		}
	}
}
