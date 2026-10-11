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
			if (base.Boss != null)
			{
				this.BaseScore = 10000000L;
				base.OriginalPosition = base.Boss.OriginalPosition;
				this.SpellTime = base.Boss.SpellTime - base.Boss.Time;
				base.Background3D.Enabled3D = false;
				this.SC_Name = null;
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == 1)
			{
				this.SC_Data = base.StageData.PData.SC_History.Find(new Predicate<SpellCardHistory>(this.FindSC));
				if (!base.StageData.OnReplay)
				{
					this.SC_Data.History++;
				}
			}
			if (base.KClass.Key_X && base.MyPlane.Spell > 0)
			{
				this.Bombed = true;
			}
			if (base.MyPlane.Time <= 1)
			{
				this.Missed = true;
			}
			if (base.Time > 210 && base.SpellList.Count > 0)
			{
				this.Bombed = true;
			}
			long spellCardBonus = this.GetSpellCardBonus();
			if (spellCardBonus > 0L)
			{
				base.StageData.SCBstring = this.GetSpellCardBonus().ToString().PadRight(14, ' ') + (this.SC_Data.Recorded.ToString() + "/" + this.SC_Data.History.ToString()).PadLeft(6);
			}
			else
			{
				base.StageData.SCBstring = "Failed".PadRight(14, ' ') + (this.SC_Data.Recorded.ToString() + "/" + this.SC_Data.History.ToString()).PadLeft(6);
			}
		}

		public override void GiveEndEffect()
		{
			if (!this.Bombed && !this.Missed)
			{
				if (base.Time < this.SpellTime)
				{
					base.StageData.SoundPlay("se_cardget.wav");
					new SpellCardBonus(base.StageData, this.GetSpellCardBonus());
					if (!base.StageData.OnReplay)
					{
						this.SC_Data.Recorded++;
					}
				}
			}
			base.Background3D.Enabled3D = true;
			base.StageData.SCBstring = null;
		}

		public virtual long GetSpellCardBonus()
		{
			long num;
			if (this.Bombed || this.Missed)
			{
				num = 0L;
			}
			else
			{
				long num2 = this.BaseScore - this.BaseScore * (long)base.Time / 2L / (long)this.SpellTime;
				num2 /= 10L;
				num2 *= 10L;
				num = num2;
			}
			return num;
		}

		public bool FindSC(SpellCardHistory SCH)
		{
			return SCH.Rank == base.Difficulty && SCH.MyPlaneFullName == base.MyPlane.FullName && SCH.Name == this.SC_Name;
		}

		public SpellCardHistory SC_Data;
	}
}
