using System.Drawing;

namespace Shooting
{
	public class BulletRemover_Small : BaseBackground
	{
		public BulletRemover_Small(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			this.OriginalPosition = OriginalPosition;
			LifeTime = 10;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			for (int i = BulletList.Count - 1; i >= 0; i--)
			{
				if (HitCheck(BulletList[i], (float)Region))
				{
					if (!BulletList[i].UnRemoveable)
					{
						BulletList[i].GiveEndEffect();
						BulletList[i].GiveItems();
						BulletList.RemoveAt(i);
					}
				}
			}
		}
	}
}
