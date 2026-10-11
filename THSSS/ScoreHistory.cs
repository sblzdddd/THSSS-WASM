using System;

namespace Shooting
{
	// Token: 0x02000126 RID: 294
	public class ScoreHistory
	{
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x000484EC File Offset: 0x000466EC
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x00048503 File Offset: 0x00046703
		public string MyPlaneFullName { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0004850C File Offset: 0x0004670C
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x00048523 File Offset: 0x00046723
		public DifficultLevel Rank { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x0004852C File Offset: 0x0004672C
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x00048543 File Offset: 0x00046743
		public string PlayerName { get; set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x0004854C File Offset: 0x0004674C
		// (set) Token: 0x060007DD RID: 2013 RVA: 0x00048563 File Offset: 0x00046763
		public long Score { get; set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0004856C File Offset: 0x0004676C
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x00048583 File Offset: 0x00046783
		public string Date { get; set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0004858C File Offset: 0x0004678C
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x000485A3 File Offset: 0x000467A3
		public string Time { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x000485AC File Offset: 0x000467AC
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x000485C3 File Offset: 0x000467C3
		public string Stage { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x000485CC File Offset: 0x000467CC
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x000485E3 File Offset: 0x000467E3
		public string SlowRate { get; set; }

		// Token: 0x060007E6 RID: 2022 RVA: 0x000485EC File Offset: 0x000467EC
		public string Data2String()
		{
			return string.Concat(new object[]
			{
				this.MyPlaneFullName,
				'\t',
				((int)this.Rank).ToString(),
				'\t',
				this.PlayerName,
				'\t',
				this.Score.ToString(),
				'\t',
				this.Date,
				'\t',
				this.Time,
				'\t',
				this.Stage,
				'\t',
				this.SlowRate,
				"\r\n"
			});
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x000486BC File Offset: 0x000468BC
		public string Data2DrawString()
		{
			return string.Concat(new string[]
			{
				this.PlayerName.PadRight(8),
				this.Score.ToString().PadLeft(11),
				this.Date.PadLeft(12),
				this.Time.PadLeft(6),
				this.Stage.PadLeft(4),
				this.SlowRate.PadLeft(5)
			});
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00048744 File Offset: 0x00046944
		public string Data2DrawStringSimple()
		{
			return this.PlayerName.PadRight(8) + this.Score.ToString().PadLeft(11) + this.Stage.PadLeft(5) + this.SlowRate.PadLeft(6);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00048798 File Offset: 0x00046998
		public void String2Data(string Data)
		{
			char[] array = new char[] { '\t' };
			string[] array2 = Data.Split(array);
			this.MyPlaneFullName = array2[0];
			this.Rank = (DifficultLevel)Convert.ToInt32(array2[1]);
			this.PlayerName = array2[2];
			this.Score = Convert.ToInt64(array2[3]);
			this.Date = array2[4];
			this.Time = array2[5];
			this.Stage = array2[6];
			this.SlowRate = array2[7];
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00048816 File Offset: 0x00046A16
		public void Copy(ScoreHistory SH)
		{
			this.String2Data(SH.Data2String().Replace("\r\n", string.Empty));
		}
	}
}
