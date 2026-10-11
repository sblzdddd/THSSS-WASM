using System;
using System.Drawing;

namespace Shooting
{
	public class BulletRemover_Small : BaseBackground
	{
		public BulletRemover_Small(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			base.OriginalPosition = OriginalPosition;
			base.LifeTime = 10;
		}

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
