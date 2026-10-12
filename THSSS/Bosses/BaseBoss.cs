namespace Shooting
{
	public class BaseBoss : BaseObject
	{
		public int SpellcardHP
		{
			get
			{
				return this.spellcardHP;
			}
			set
			{
				this.spellcardHP = value;
			}
		}

		public int UnmatchedTime
		{
			get
			{
				return 100;
			}
		}

		public int Life { get; set; }

		public int MaxHP { get; set; }

		public bool OnSpell { get; set; }

		public int SpellTime { get; set; }

		public float Armon
		{
			get
			{
				return this.armor;
			}
			set
			{
				if (value < 0f)
				{
					this.armor = 0f;
				}
				else if (value > 1f)
				{
					this.armor = 1f;
				}
				else
				{
					this.armor = value;
				}
			}
		}

		public override void HitCheckAll()
		{
			if (this.Life >= 0 && base.HitEnabled)
			{
				if (base.MyPlane.HitEnabled)
				{
					if (this.HitCheck(base.MyPlane))
					{
						base.MyPlane.PreMiss();
					}
				}
				for (int i = base.MyBulletList.Count - 1; i >= 0; i--)
				{
					if (this.HitCheck(base.MyBulletList[i]))
					{
						if (base.Time > this.UnmatchedTime)
						{
							if (base.MyPlane.EnchantmentState == EnchantmentType.Red)
							{
								base.HealthPoint -= (float)base.MyBulletList[i].Damage * 1.25f * (1f - this.Armon);
							}
							else
							{
								base.HealthPoint -= (float)base.MyBulletList[i].Damage * (1f - this.Armon);
							}
						}
						base.MyBulletList[i].GiveEndEffect();
						base.MyBulletList.RemoveAt(i);
						base.MyPlane.Score += 10L;
						if (base.HealthPoint <= 200f)
						{
							base.StageData.SoundPlay("se_damage01.wav");
						}
					}
				}
			}
		}

		private const int unmatchedTime = 100;

		private int spellcardHP = 2000;

		private float armor = 0f;
	}
}
