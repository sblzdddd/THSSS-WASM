using System;

namespace Shooting
{
	// Token: 0x02000127 RID: 295
	public class ClearHistory
	{
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x00048840 File Offset: 0x00046A40
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x00048857 File Offset: 0x00046A57
		public string MyPlaneFullName { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x00048860 File Offset: 0x00046A60
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x00048877 File Offset: 0x00046A77
		public DifficultLevel Rank { get; set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x00048880 File Offset: 0x00046A80
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x00048897 File Offset: 0x00046A97
		public int StartTimes { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x000488A0 File Offset: 0x00046AA0
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x000488B7 File Offset: 0x00046AB7
		public long PlayingTime { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x000488C0 File Offset: 0x00046AC0
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x000488D7 File Offset: 0x00046AD7
		public int ClearTimes { get; set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x000488E0 File Offset: 0x00046AE0
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x000488F7 File Offset: 0x00046AF7
		public int NoContinueClearTimes { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00048900 File Offset: 0x00046B00
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x00048917 File Offset: 0x00046B17
		public int PracticeLevel { get; set; }

		// Token: 0x060007FA RID: 2042 RVA: 0x00048920 File Offset: 0x00046B20
		public string Data2String()
		{
			return string.Concat(new object[]
			{
				this.MyPlaneFullName,
				'\t',
				((int)this.Rank).ToString(),
				'\t',
				this.StartTimes.ToString(),
				'\t',
				this.PlayingTime.ToString(),
				'\t',
				this.ClearTimes.ToString(),
				'\t',
				this.NoContinueClearTimes.ToString(),
				'\t',
				this.PracticeLevel.ToString(),
				"\r\n"
			});
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x000489FC File Offset: 0x00046BFC
		public void String2Data(string Data)
		{
			char[] array = new char[] { '\t' };
			string[] array2 = Data.Split(array);
			this.MyPlaneFullName = array2[0];
			this.Rank = (DifficultLevel)Convert.ToInt32(array2[1]);
			this.StartTimes = Convert.ToInt32(array2[2]);
			this.PlayingTime = (long)Convert.ToInt32(array2[3]);
			this.ClearTimes = Convert.ToInt32(array2[4]);
			this.NoContinueClearTimes = Convert.ToInt32(array2[5]);
			this.PracticeLevel = Convert.ToInt32(array2[6]);
		}
	}
}
