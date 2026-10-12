using System.Diagnostics;
using System.Threading;

namespace Shooting
{
	public class Watch
	{
		private long startTime { get; set; }

		public void Start()
		{
			startTime = Stopwatch.GetTimestamp();
		}

		public float GetDuration()
		{
			float num;
			if (startTime == 0L)
			{
				num = 0f;
			}
			else
			{
				num = 1000f * (float)(Stopwatch.GetTimestamp() - startTime) / (float)Stopwatch.Frequency;
			}
			return num;
		}

		public void IncreaseOneFrame()
		{
			startTime += Stopwatch.Frequency / 60L;
		}

		public void SleepTo(float Time)
		{
			Time -= GetDuration();
			if (Time > 0f)
			{
#if !BROWSER
				Thread.Sleep((int)Time);
#endif
			}
		}
	}
}
