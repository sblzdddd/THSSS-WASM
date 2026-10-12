using System.Drawing;

namespace Shooting
{
	public class BulletRemover2 : BaseBackground
	{
		public BulletRemover2(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			this.OriginalPosition = OriginalPosition;
			LifeTime = 30;
			Region = 2;
			foreach (var baseEnemyPlane in EnemyPlaneList)
			{
				baseEnemyPlane.GiveEndEffect();
			}
			EnemyPlaneList.Clear();
		}

		public override void Ctrl()
		{
			base.Ctrl();
			Region += 25;
			for (var i = BulletList.Count - 1; i >= 0; i--)
			{
				if (HitCheck(BulletList[i], (float)Region))
				{
					BulletList[i].GiveEndEffect();
					BulletList[i].GiveItems();
					BulletList.RemoveAt(i);
				}
			}
		}
	}
}
