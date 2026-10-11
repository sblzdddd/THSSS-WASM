using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000075 RID: 117
	public class BulletRemover_Small : BaseBackground
	{
		// Token: 0x06000407 RID: 1031 RVA: 0x0001BA2B File Offset: 0x00019C2B
		public BulletRemover_Small(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			base.OriginalPosition = OriginalPosition;
			base.LifeTime = 10;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001BA64 File Offset: 0x00019C64
		public override void Ctrl()
		{
			base.Ctrl();
			for (int i = base.BulletList.Count - 1; i >= 0; i--)
			{
				if (this.HitCheck(base.BulletList[i], (float)base.Region))
				{
					if (!base.BulletList[i].UnRemoveable)
					{
						base.BulletList[i].GiveEndEffect();
						base.BulletList[i].GiveItems();
						base.BulletList.RemoveAt(i);
					}
				}
			}
		}
	}
}
