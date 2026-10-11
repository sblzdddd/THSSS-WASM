using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200017E RID: 382
	public class BackgroundRotate : BaseObject_CS
	{
		// Token: 0x06000931 RID: 2353 RVA: 0x000586D4 File Offset: 0x000568D4
		public BackgroundRotate(StageDataPackage StageData, string textureName)
			: base(StageData, textureName, new PointF(0f, 0f), 0f, 0.0)
		{
			base.Background.BackgroundList.Add(this);
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2));
			base.Scale = 2.5f;
			base.AngularVelocityDegree = 0.5f;
		}
	}
}
