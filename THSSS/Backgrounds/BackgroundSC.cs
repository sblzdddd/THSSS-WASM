using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200009F RID: 159
	public class BackgroundSC : BaseObject
	{
		// Token: 0x060004C3 RID: 1219 RVA: 0x00024F8C File Offset: 0x0002318C
		public BackgroundSC(StageDataPackage StageData, string textureName)
			: base(StageData, textureName, new PointF(0f, 0f), 0f, 0.0)
		{
			base.Background.BackgroundList.Add(this);
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2));
			base.DirectionDegree = 90.0;
		}
	}
}
