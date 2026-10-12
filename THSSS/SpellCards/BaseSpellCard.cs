using System;

namespace Shooting
{
	internal class BaseSpellCard : BaseBulleEmitter
	{
		public bool Bombed { get; set; }

		public bool Missed { get; set; }

		public long BaseScore { get; set; }

		public int SpellTime { get; set; }

		public string SC_Name { get; set; }

		public BaseSpellCard(StageDataPackage StageData)
			: base(StageData)
		{
			if (Boss != null)
			{
				BaseScore = 10000000L;
				OriginalPosition = Boss.OriginalPosition;
				SpellTime = Boss.SpellTime - Boss.Time;
				Background3D.Enabled3D = false;
				SC_Name = null;
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time == 1)
			{
				SC_Data = StageData.PData.SC_History.Find(new Predicate<SpellCardHistory>(FindSC));
				if (!StageData.OnReplay)
				{
					SC_Data.History++;
				}
			}
			if (KClass.Key_X && MyPlane.Spell > 0)
			{
				Bombed = true;
			}
			if (MyPlane.Time <= 1)
			{
				Missed = true;
			}
			if (Time > 210 && SpellList.Count > 0)
			{
				Bombed = true;
			}
			var spellCardBonus = GetSpellCardBonus();
			if (spellCardBonus > 0L)
			{
				StageData.SCBstring = GetSpellCardBonus().ToString().PadRight(14, ' ') + (SC_Data.Recorded.ToString() + "/" + SC_Data.History.ToString()).PadLeft(6);
			}
			else
			{
				StageData.SCBstring = "Failed".PadRight(14, ' ') + (SC_Data.Recorded.ToString() + "/" + SC_Data.History.ToString()).PadLeft(6);
			}
		}

		public override void GiveEndEffect()
		{
			if (!Bombed && !Missed)
			{
				if (Time < SpellTime)
				{
					StageData.SoundPlay("se_cardget.wav");
					new SpellCardBonus(StageData, GetSpellCardBonus());
					if (!StageData.OnReplay)
					{
						SC_Data.Recorded++;
					}
				}
			}
			Background3D.Enabled3D = true;
			StageData.SCBstring = null;
		}

		public virtual long GetSpellCardBonus()
		{
			long num;
			if (Bombed || Missed)
			{
				num = 0L;
			}
			else
			{
				var num2 = BaseScore - BaseScore * (long)Time / 2L / (long)SpellTime;
				num2 /= 10L;
				num2 *= 10L;
				num = num2;
			}
			return num;
		}

		public bool FindSC(SpellCardHistory SCH)
		{
			return SCH.Rank == Difficulty && SCH.MyPlaneFullName == MyPlane.FullName && SCH.Name == SC_Name;
		}

		public SpellCardHistory SC_Data;
	}
}
