using System;

namespace Shooting
{
	// Token: 0x02000087 RID: 135
	public class BaseSpell : BaseObject
	{
		// Token: 0x06000468 RID: 1128 RVA: 0x00020005 File Offset: 0x0001E205
		public BaseSpell()
		{
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00020010 File Offset: 0x0001E210
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

		// Token: 0x0600046A RID: 1130 RVA: 0x0002007C File Offset: 0x0001E27C
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

		// Token: 0x0600046B RID: 1131 RVA: 0x00020160 File Offset: 0x0001E360
		public override bool OutBoundary()
		{
			return base.Time > base.LifeTime;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00020190 File Offset: 0x0001E390
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
