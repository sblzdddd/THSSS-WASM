using System;

namespace Shooting
{
	public class ClearHistory
	{
		public string MyPlaneFullName { get; set; }

		public DifficultLevel Rank { get; set; }

		public int StartTimes { get; set; }

		public long PlayingTime { get; set; }

		public int ClearTimes { get; set; }

		public int NoContinueClearTimes { get; set; }

		public int PracticeLevel { get; set; }

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
