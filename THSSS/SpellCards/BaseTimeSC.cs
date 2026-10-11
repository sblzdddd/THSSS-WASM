using System;

namespace Shooting
{
	// Token: 0x0200011E RID: 286
	internal class BaseTimeSC : BaseSpellCard
	{
		// Token: 0x06000795 RID: 1941 RVA: 0x000464FD File Offset: 0x000446FD
		public BaseTimeSC(StageDataPackage StageData)
			: base(StageData)
		{
			base.Boss.Armon = 1f;
			base.Boss.HitEnabled = false;
			base.Boss.TransparentValueF = 200f;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00046538 File Offset: 0x00044738
		public override void GiveEndEffect()
		{
			if (!base.Bombed && !base.Missed)
			{
				base.StageData.SoundPlay("se_cardget.wav");
				new SpellCardBonus(base.StageData, this.GetSpellCardBonus());
				if (!base.StageData.OnReplay)
				{
					this.SC_Data.Recorded++;
				}
			}
			base.Background3D.Enabled3D = true;
			base.StageData.SCBstring = null;
			base.Boss.HitEnabled = true;
			base.Boss.TransparentValueF = 255f;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x000465DC File Offset: 0x000447DC
		public override long GetSpellCardBonus()
		{
			long num;
			if (base.Bombed || base.Missed)
			{
				num = 0L;
			}
			else
			{
				num = base.BaseScore + (long)base.Difficulty * 2000000L;
			}
			return num;
		}
	}
}
