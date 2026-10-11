using System;
using System.Diagnostics;
using System.Threading;

namespace Shooting
{
	public class Watch
	{
		private long startTime { get; set; }

		public void Start()
		{
			this.startTime = Stopwatch.GetTimestamp();
		}

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

		public void IncreaseOneFrame()
		{
			this.startTime += Stopwatch.Frequency / 60L;
		}

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
