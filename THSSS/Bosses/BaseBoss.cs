namespace Shooting
{
	public class BaseBoss : BaseObject
	{
		public int SpellcardHP
		{
			get
			{
				return spellcardHP;
			}
			set
			{
				spellcardHP = value;
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
				return armor;
			}
			set
			{
				if (value < 0f)
				{
					armor = 0f;
				}
				else if (value > 1f)
				{
					armor = 1f;
				}
				else
				{
					armor = value;
				}
			}
		}

		public override void HitCheckAll()
		{
			if (Life >= 0 && HitEnabled)
			{
				if (MyPlane.HitEnabled)
				{
					if (HitCheck(MyPlane))
					{
						MyPlane.PreMiss();
					}
				}
				for (var i = MyBulletList.Count - 1; i >= 0; i--)
				{
					if (HitCheck(MyBulletList[i]))
					{
						if (Time > UnmatchedTime)
						{
							if (MyPlane.EnchantmentState == EnchantmentType.Red)
							{
								HealthPoint -= (float)MyBulletList[i].Damage * 1.25f * (1f - Armon);
							}
							else
							{
								HealthPoint -= (float)MyBulletList[i].Damage * (1f - Armon);
							}
						}
						MyBulletList[i].GiveEndEffect();
						MyBulletList.RemoveAt(i);
						MyPlane.Score += 10L;
						if (HealthPoint <= 200f)
						{
							StageData.SoundPlay("se_damage01.wav");
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
