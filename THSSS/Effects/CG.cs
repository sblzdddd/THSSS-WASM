using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200009D RID: 157
	internal class CG : BaseStoryItem
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x00024C74 File Offset: 0x00022E74
		public CG(StageDataPackage StageData, string textureName)
			: base(StageData)
		{
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2));
			base.Velocity = 0f;
			base.Direction = 0.0;
			base.Active = false;
			base.TransparentValueF = 0f;
			base.TransparentVelocity = 20f;
			base.Angle = (double)(-(double)((float)(base.Direction - 1.5707963267948966)));
		}
	}
}
