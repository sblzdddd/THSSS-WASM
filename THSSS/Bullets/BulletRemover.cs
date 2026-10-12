using System.Drawing;

namespace Shooting
{
	public class BulletRemover : BaseBackground
	{
		public BulletRemover(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			this.OriginalPosition = OriginalPosition;
			LifeTime = 30;
			Region = 2;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			Region += 20;
			for (int i = BulletList.Count - 1; i >= 0; i--)
			{
				if (HitCheck(BulletList[i], (float)Region))
				{
					if (!BulletList[i].UnRemoveable)
					{
						BulletList[i].GiveEndEffect();
						BulletList.RemoveAt(i);
					}
				}
			}
			for (int i = EnemyPlaneList.Count - 1; i >= 0; i--)
			{
				if (HitCheck(EnemyPlaneList[i], (float)Region))
				{
					EnemyPlaneList[i].HealthPoint -= 20f;
					if (EnemyPlaneList[i].HealthPoint <= 0f)
					{
						EnemyPlaneList[i].GiveEndEffect();
						EnemyPlaneList[i].GiveItems();
						EnemyPlaneList.RemoveAt(i);
						StageData.SoundPlay("se_enep00.wav");
					}
				}
			}
		}
	}
}
