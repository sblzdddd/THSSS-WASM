using System;

namespace Shooting
{
	// Token: 0x02000056 RID: 86
	internal class BaseFSC : BaseSpellCard
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x0001231F File Offset: 0x0001051F
		public BaseFSC(StageDataPackage StageData)
			: base(StageData)
		{
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0001232C File Offset: 0x0001052C
		public override long GetSpellCardBonus()
		{
			long num;
			if (base.Bombed || base.Missed)
			{
				num = 0L;
			}
			else
			{
				long num2 = 99999990L;
				num = num2;
			}
			return num;
		}
	}
}
