using System;

namespace Shooting
{
	// Token: 0x0200005B RID: 91
	public interface IGameState
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002B5 RID: 693
		// (set) Token: 0x060002B6 RID: 694
		GlobalDataPackage GlobalData { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002B7 RID: 695
		// (set) Token: 0x060002B8 RID: 696
		StageDataPackage StageData { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002B9 RID: 697
		// (set) Token: 0x060002BA RID: 698
		StateSwitchDataPackage StateSwitchData { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002BB RID: 699
		// (set) Token: 0x060002BC RID: 700
		string StageName { get; set; }

		// Token: 0x060002BD RID: 701
		void Init();

		// Token: 0x060002BE RID: 702
		void UpdateData();

		// Token: 0x060002BF RID: 703
		void Render();

		// Token: 0x060002C0 RID: 704
		void BGM_ON();

		// Token: 0x060002C1 RID: 705
		void BGM_Pause();

		// Token: 0x060002C2 RID: 706
		void BGM_Resume();

		// Token: 0x060002C3 RID: 707
		void Dispose();
	}
}
