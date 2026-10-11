using System;
using System.Drawing;

namespace Shooting
{
	public class BulletRemover2 : BaseBackground
	{
		public BulletRemover2(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			base.OriginalPosition = OriginalPosition;
			base.LifeTime = 30;
			base.Region = 2;
			foreach (BaseEnemyPlane baseEnemyPlane in base.EnemyPlaneList)
			{
				baseEnemyPlane.GiveEndEffect();
			}
			base.EnemyPlaneList.Clear();
		}

		public override void Ctrl()
		{
			base.Ctrl();
			base.Region += 25;
			for (int i = base.BulletList.Count - 1; i >= 0; i--)
			{
				if (this.HitCheck(base.BulletList[i], (float)base.Region))
				{
					base.BulletList[i].GiveEndEffect();
					base.BulletList[i].GiveItems();
					base.BulletList.RemoveAt(i);
				}
			}
		}
	}
}
