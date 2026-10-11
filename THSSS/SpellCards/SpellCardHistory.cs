using System;

namespace Shooting
{
	// Token: 0x02000125 RID: 293
	public class SpellCardHistory
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x00048318 File Offset: 0x00046518
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x0004832F File Offset: 0x0004652F
		public string MyPlaneFullName { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00048338 File Offset: 0x00046538
		// (set) Token: 0x060007CA RID: 1994 RVA: 0x0004834F File Offset: 0x0004654F
		public DifficultLevel Rank { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00048358 File Offset: 0x00046558
		// (set) Token: 0x060007CC RID: 1996 RVA: 0x0004836F File Offset: 0x0004656F
		public int Index { get; set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00048378 File Offset: 0x00046578
		// (set) Token: 0x060007CE RID: 1998 RVA: 0x0004838F File Offset: 0x0004658F
		public string Name { get; set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00048398 File Offset: 0x00046598
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x000483AF File Offset: 0x000465AF
		public int Recorded { get; set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x000483B8 File Offset: 0x000465B8
		// (set) Token: 0x060007D2 RID: 2002 RVA: 0x000483CF File Offset: 0x000465CF
		public int History { get; set; }

		// Token: 0x060007D3 RID: 2003 RVA: 0x000483D8 File Offset: 0x000465D8
		public string Data2String()
		{
			return string.Concat(new object[]
			{
				this.Index.ToString(),
				'\t',
				this.MyPlaneFullName,
				'\t',
				((int)this.Rank).ToString(),
				'\t',
				this.Recorded.ToString(),
				'\t',
				this.History.ToString(),
				"\r\n"
			});
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00048478 File Offset: 0x00046678
		public void String2Data(string Data)
		{
			char[] array = new char[] { '\t' };
			string[] array2 = Data.Split(array);
			this.Index = Convert.ToInt32(array2[0]);
			this.MyPlaneFullName = array2[1];
			this.Rank = (DifficultLevel)Convert.ToInt32(array2[2]);
			this.Recorded = Convert.ToInt32(array2[3]);
			this.History = Convert.ToInt32(array2[4]);
		}
	}
}
