using System;

namespace Shooting
{
	// Token: 0x0200002B RID: 43
	internal class BaseSpellCard : BaseBulleEmitter
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000B218 File Offset: 0x00009418
		// (set) Token: 0x060001DE RID: 478 RVA: 0x0000B22F File Offset: 0x0000942F
		public bool Bombed { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000B238 File Offset: 0x00009438
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000B24F File Offset: 0x0000944F
		public bool Missed { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000B258 File Offset: 0x00009458
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x0000B26F File Offset: 0x0000946F
		public long BaseScore { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000B278 File Offset: 0x00009478
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x0000B28F File Offset: 0x0000948F
		public int SpellTime { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000B298 File Offset: 0x00009498
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000B2AF File Offset: 0x000094AF
		public string SC_Name { get; set; }

		// Token: 0x060001E7 RID: 487 RVA: 0x0000B2B8 File Offset: 0x000094B8
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

		// Token: 0x060001E8 RID: 488 RVA: 0x0000B334 File Offset: 0x00009534
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

		// Token: 0x060001E9 RID: 489 RVA: 0x0000B4EC File Offset: 0x000096EC
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

		// Token: 0x060001EA RID: 490 RVA: 0x0000B58C File Offset: 0x0000978C
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

		// Token: 0x060001EB RID: 491 RVA: 0x0000B5EC File Offset: 0x000097EC
		public bool FindSC(SpellCardHistory SCH)
		{
			return SCH.Rank == base.Difficulty && SCH.MyPlaneFullName == base.MyPlane.FullName && SCH.Name == this.SC_Name;
		}

		// Token: 0x04000097 RID: 151
		public SpellCardHistory SC_Data;
	}
}
