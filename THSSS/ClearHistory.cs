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
				MyPlaneFullName,
				'\t',
				((int)Rank).ToString(),
				'\t',
				StartTimes.ToString(),
				'\t',
				PlayingTime.ToString(),
				'\t',
				ClearTimes.ToString(),
				'\t',
				NoContinueClearTimes.ToString(),
				'\t',
				PracticeLevel.ToString(),
				"\r\n"
			});
		}

		public void String2Data(string Data)
		{
			char[] array = new char[] { '\t' };
			string[] array2 = Data.Split(array);
			MyPlaneFullName = array2[0];
			Rank = (DifficultLevel)Convert.ToInt32(array2[1]);
			StartTimes = Convert.ToInt32(array2[2]);
			PlayingTime = (long)Convert.ToInt32(array2[3]);
			ClearTimes = Convert.ToInt32(array2[4]);
			NoContinueClearTimes = Convert.ToInt32(array2[5]);
			PracticeLevel = Convert.ToInt32(array2[6]);
		}
	}
}
