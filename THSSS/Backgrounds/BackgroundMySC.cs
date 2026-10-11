using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200020B RID: 523
	public class BackgroundMySC : BaseObject
	{
		// Token: 0x06000B9E RID: 2974 RVA: 0x00073694 File Offset: 0x00071894
		public BackgroundMySC(StageDataPackage StageData, string textureName)
			: base(StageData, textureName, new PointF(0f, 0f), 0f, 0.0)
		{
			base.Background.BackgroundList.Add(this);
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2));
			base.Scale = 2.5f;
			base.AngularVelocityDegree = 0.5f;
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00073724 File Offset: 0x00071924
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time >= base.LifeTime - 50)
			{
				base.TransparentVelocity = -5f;
			}
		}
	}
}
