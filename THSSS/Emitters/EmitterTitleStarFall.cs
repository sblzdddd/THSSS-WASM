using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000BB RID: 187
	internal class EmitterTitleStarFall : BaseEffect
	{
		// Token: 0x06000551 RID: 1361 RVA: 0x0002E16C File Offset: 0x0002C36C
		public EmitterTitleStarFall(StageDataPackage StageData)
			: base(StageData, null, new PointF(0f, 0f), 0f, 0.0)
		{
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width - 100), 0f);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0002E1C4 File Offset: 0x0002C3C4
		public override void Shoot()
		{
			if (base.Time % 80 == 0)
			{
				new EmitterStarFall01(base.StageData, new PointF(base.OriginalPosition.X + 50f + (float)base.Ran.Next(-200, 200), (float)base.Ran.Next(30)));
			}
		}
	}
}
