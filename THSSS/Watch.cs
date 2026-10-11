using System;
using System.Diagnostics;
using System.Threading;

namespace Shooting
{
	// Token: 0x020000B5 RID: 181
	public class Watch
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00029F48 File Offset: 0x00028148
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x00029F5F File Offset: 0x0002815F
		private long startTime { get; set; }

		// Token: 0x0600052D RID: 1325 RVA: 0x00029F68 File Offset: 0x00028168
		public void Start()
		{
			this.startTime = Stopwatch.GetTimestamp();
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00029F78 File Offset: 0x00028178
		public float GetDuration()
		{
			float num;
			if (this.startTime == 0L)
			{
				num = 0f;
			}
			else
			{
				num = 1000f * (float)(Stopwatch.GetTimestamp() - this.startTime) / (float)Stopwatch.Frequency;
			}
			return num;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00029FBF File Offset: 0x000281BF
		public void IncreaseOneFrame()
		{
			this.startTime += Stopwatch.Frequency / 60L;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00029FDC File Offset: 0x000281DC
		public void SleepTo(float Time)
		{
			Time -= this.GetDuration();
			if (Time > 0f)
			{
				Thread.Sleep((int)Time);
			}
		}
	}
}
