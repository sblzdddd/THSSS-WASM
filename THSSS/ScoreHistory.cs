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

		public string Data2DrawStringSimple()
		{
			return this.PlayerName.PadRight(8) + this.Score.ToString().PadLeft(11) + this.Stage.PadLeft(5) + this.SlowRate.PadLeft(6);
		}

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

		public void Copy(ScoreHistory SH)
		{
			this.String2Data(SH.Data2String().Replace("\r\n", string.Empty));
		}
	}
}
