using System;

namespace Shooting
{
	public class ScoreHistory
	{
		public string MyPlaneFullName { get; set; }

		public DifficultLevel Rank { get; set; }

		public string PlayerName { get; set; }

		public long Score { get; set; }

		public string Date { get; set; }

		public string Time { get; set; }

		public string Stage { get; set; }

		public string SlowRate { get; set; }

		public string Data2String()
		{
			return string.Concat(new object[]
			{
				MyPlaneFullName,
				'\t',
				((int)Rank).ToString(),
				'\t',
				PlayerName,
				'\t',
				Score.ToString(),
				'\t',
				Date,
				'\t',
				Time,
				'\t',
				Stage,
				'\t',
				SlowRate,
				"\r\n"
			});
		}

		public string Data2DrawString()
		{
			return string.Concat(new string[]
			{
				PlayerName.PadRight(8),
				Score.ToString().PadLeft(11),
				Date.PadLeft(12),
				Time.PadLeft(6),
				Stage.PadLeft(4),
				SlowRate.PadLeft(5)
			});
		}

		public string Data2DrawStringSimple()
		{
			return PlayerName.PadRight(8) + Score.ToString().PadLeft(11) + Stage.PadLeft(5) + SlowRate.PadLeft(6);
		}

		public void String2Data(string Data)
		{
			char[] array = new char[] { '\t' };
			string[] array2 = Data.Split(array);
			MyPlaneFullName = array2[0];
			Rank = (DifficultLevel)Convert.ToInt32(array2[1]);
			PlayerName = array2[2];
			Score = Convert.ToInt64(array2[3]);
			Date = array2[4];
			Time = array2[5];
			Stage = array2[6];
			SlowRate = array2[7];
		}

		public void Copy(ScoreHistory SH)
		{
			String2Data(SH.Data2String().Replace("\r\n", string.Empty));
		}
	}
}
