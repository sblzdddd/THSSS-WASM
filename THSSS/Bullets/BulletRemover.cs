using System;
using System.Drawing;

namespace Shooting
{
	public class BulletRemover : BaseBackground
	{
		public BulletRemover(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			base.OriginalPosition = OriginalPosition;
			base.LifeTime = 30;
			base.Region = 2;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			base.Region += 20;
			for (int i = base.BulletList.Count - 1; i >= 0; i--)
			{
				if (this.HitCheck(base.BulletList[i], (float)base.Region))
				{
					if (!base.BulletList[i].UnRemoveable)
					{
						base.BulletList[i].GiveEndEffect();
						base.BulletList.RemoveAt(i);
					}
				}
			}
			for (int i = base.EnemyPlaneList.Count - 1; i >= 0; i--)
			{
				if (this.HitCheck(base.EnemyPlaneList[i], (float)base.Region))
				{
					base.EnemyPlaneList[i].HealthPoint -= 20f;
					if (base.EnemyPlaneList[i].HealthPoint <= 0f)
					{
						base.EnemyPlaneList[i].GiveEndEffect();
						base.EnemyPlaneList[i].GiveItems();
						base.EnemyPlaneList.RemoveAt(i);
						base.StageData.SoundPlay("se_enep00.wav");
					}
				}
			}
		}
	}
}
