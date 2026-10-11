using System;

namespace Shooting
{
	// Token: 0x02000030 RID: 48
	public class BaseBoss : BaseObject
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000BB40 File Offset: 0x00009D40
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x0000BB58 File Offset: 0x00009D58
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

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000BB64 File Offset: 0x00009D64
		public int UnmatchedTime
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000BB78 File Offset: 0x00009D78
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000BB8F File Offset: 0x00009D8F
		public int Life { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000BB98 File Offset: 0x00009D98
		// (set) Token: 0x060001FA RID: 506 RVA: 0x0000BBAF File Offset: 0x00009DAF
		public int MaxHP { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000BBB8 File Offset: 0x00009DB8
		// (set) Token: 0x060001FC RID: 508 RVA: 0x0000BBCF File Offset: 0x00009DCF
		public bool OnSpell { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000BBD8 File Offset: 0x00009DD8
		// (set) Token: 0x060001FE RID: 510 RVA: 0x0000BBEF File Offset: 0x00009DEF
		public int SpellTime { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000BBF8 File Offset: 0x00009DF8
		// (set) Token: 0x06000200 RID: 512 RVA: 0x0000BC10 File Offset: 0x00009E10
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

		// Token: 0x06000202 RID: 514 RVA: 0x0000BC84 File Offset: 0x00009E84
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

		// Token: 0x0400009D RID: 157
		private const int unmatchedTime = 100;

		// Token: 0x0400009E RID: 158
		private int spellcardHP = 2000;

		// Token: 0x0400009F RID: 159
		private float armor = 0f;
	}
}
