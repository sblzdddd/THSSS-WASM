using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000E3 RID: 227
	public class BackgroundFix : BaseObject
	{
		// Token: 0x0600062C RID: 1580 RVA: 0x0003707C File Offset: 0x0003527C
		public BackgroundFix(StageDataPackage StageData, string textureName)
			: base(StageData, textureName, new PointF(0f, 0f), 0f, 0.0)
		{
			base.Background.BackgroundList.Add(this);
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2));
			base.AngleDegree = 90.0;
			base.OutsideRegion = 1000;
		}
	}
}
