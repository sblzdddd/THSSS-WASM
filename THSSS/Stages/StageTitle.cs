using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000116 RID: 278
	public class StageTitle : BaseEffect
	{
		// Token: 0x0600077C RID: 1916 RVA: 0x00044070 File Offset: 0x00042270
		public StageTitle(StageDataPackage StageData, string TextureName)
			: base(StageData, TextureName, new Point(0, 0), 0f, 1.5707963267948966)
		{
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2 - 80));
			base.LifeTime = 200;
			base.TransparentValueF = 0f;
			base.Active = false;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x000440F4 File Offset: 0x000422F4
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time < base.LifeTime / 3)
			{
				base.TransparentValueF += (float)(base.MaxTransparent * 3 / base.LifeTime);
			}
			else if (base.Time > base.LifeTime * 2 / 3)
			{
				base.TransparentValueF -= (float)(base.MaxTransparent * 3 / base.LifeTime);
			}
		}
	}
}
