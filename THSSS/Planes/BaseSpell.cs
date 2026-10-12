namespace Shooting
{
	public class BaseSpell : BaseObject
	{
		public BaseSpell()
		{
		}

		public BaseSpell(StageDataPackage StageData)
		{
			this.StageData = StageData;
			Position = MyPlane.Position;
			Damage = 12;
			LifeTime = 300;
			SpellList.Add(this);
			StageData.VibrateStart(LifeTime);
			new BackgroundBlack(StageData, Position);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time < 30)
			{
				for (var i = ItemList.Count - 1; i >= 0; i--)
				{
					if (HitCheck(ItemList[i]) && !(ItemList[i] is ShootingStarShard))
					{
						ItemList[i].Obtain = true;
						ItemList[i].Doubled = true;
					}
				}
			}
			MyPlane.DeadTime = -1;
			if (OutBoundary())
			{
				SpellList.Remove(this);
				MyPlane.Time = MyPlane.UnmatchedTime - 60;
			}
		}

		public override bool OutBoundary()
		{
			return Time > LifeTime;
		}

		public override void HitCheckAll()
		{
			for (var i = BulletList.Count - 1; i >= 0; i--)
			{
				if (HitCheck(BulletList[i]))
				{
					if (!BulletList[i].UnRemoveable)
					{
						BulletList[i].GiveEndEffect();
						BulletList[i].GiveItems();
						BulletList.RemoveAt(i);
					}
				}
			}
			for (var i = EnemyPlaneList.Count - 1; i >= 0; i--)
			{
				if (HitCheck(EnemyPlaneList[i]))
				{
					EnemyPlaneList[i].HealthPoint -= (float)Damage;
					if (EnemyPlaneList[i].HealthPoint <= 0f)
					{
						EnemyPlaneList[i].GiveEndEffect();
						EnemyPlaneList[i].GiveItems();
						EnemyPlaneList.RemoveAt(i);
						StageData.SoundPlay("se_enep00.wav");
						break;
					}
				}
			}
			if (Boss != null)
			{
				if (Boss.Time > Boss.UnmatchedTime && HitCheck(Boss))
				{
					Boss.HealthPoint -= (float)((int)((float)Damage * (1f - Boss.Armon)));
					if (Boss.HealthPoint <= 200f)
					{
						StageData.SoundPlay("se_damage01.wav");
					}
				}
			}
		}
	}
}
