using System;

namespace Shooting
{
	public class BaseSpell : BaseObject
	{
		public BaseSpell()
		{
		}

		public BaseSpell(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.Position = base.MyPlane.Position;
			base.Damage = 12;
			base.LifeTime = 300;
			base.SpellList.Add(this);
			StageData.VibrateStart(base.LifeTime);
			new BackgroundBlack(StageData, base.Position);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time < 30)
			{
				for (int i = base.ItemList.Count - 1; i >= 0; i--)
				{
					if (this.HitCheck(base.ItemList[i]) && !(base.ItemList[i] is ShootingStarShard))
					{
						base.ItemList[i].Obtain = true;
						base.ItemList[i].Doubled = true;
					}
				}
			}
			base.MyPlane.DeadTime = -1;
			if (this.OutBoundary())
			{
				base.SpellList.Remove(this);
				base.MyPlane.Time = base.MyPlane.UnmatchedTime - 60;
			}
		}

		public override bool OutBoundary()
		{
			return base.Time > base.LifeTime;
		}

		public override void HitCheckAll()
		{
			for (int i = base.BulletList.Count - 1; i >= 0; i--)
			{
				if (this.HitCheck(base.BulletList[i]))
				{
					if (!base.BulletList[i].UnRemoveable)
					{
						base.BulletList[i].GiveEndEffect();
						base.BulletList[i].GiveItems();
						base.BulletList.RemoveAt(i);
					}
				}
			}
			for (int i = base.EnemyPlaneList.Count - 1; i >= 0; i--)
			{
				if (this.HitCheck(base.EnemyPlaneList[i]))
				{
					base.EnemyPlaneList[i].HealthPoint -= (float)base.Damage;
					if (base.EnemyPlaneList[i].HealthPoint <= 0f)
					{
						base.EnemyPlaneList[i].GiveEndEffect();
						base.EnemyPlaneList[i].GiveItems();
						base.EnemyPlaneList.RemoveAt(i);
						base.StageData.SoundPlay("se_enep00.wav");
						break;
					}
				}
			}
			if (base.Boss != null)
			{
				if (base.Boss.Time > base.Boss.UnmatchedTime && this.HitCheck(base.Boss))
				{
					base.Boss.HealthPoint -= (float)((int)((float)base.Damage * (1f - base.Boss.Armon)));
					if (base.Boss.HealthPoint <= 200f)
					{
						base.StageData.SoundPlay("se_damage01.wav");
					}
				}
			}
		}
	}
}
