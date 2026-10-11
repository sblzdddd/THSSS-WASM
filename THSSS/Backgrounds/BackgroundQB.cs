using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200008D RID: 141
	internal class BackgroundQB : BaseObject
	{
		// Token: 0x0600047E RID: 1150 RVA: 0x00020D84 File Offset: 0x0001EF84
		public BackgroundQB(StageDataPackage StageData, string textureName)
			: base(StageData, textureName, new PointF(0f, 0f), 0f, 1.5707963267948966)
		{
			base.Background.BackgroundList.Add(this);
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), 200f);
			base.TransparentValueF = 0f;
			base.Scale = 0.9f;
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00020E08 File Offset: 0x0001F008
		public override void Ctrl()
		{
			base.Time++;
			if (base.TransparentValueF < 200f)
			{
				base.TransparentValueF += 5f;
			}
		}
	}
}
