using System;

namespace Shooting
{
	public class SpellCardHistory
	{
		public string MyPlaneFullName { get; set; }

		public DifficultLevel Rank { get; set; }

		public int Index { get; set; }

		public string Name { get; set; }

		public int Recorded { get; set; }

		public int History { get; set; }

		public string Data2String()
		{
			return string.Concat(new object[]
			{
				this.Index.ToString(),
				'\t',
				this.MyPlaneFullName,
				'\t',
				((int)this.Rank).ToString(),
				'\t',
				this.Recorded.ToString(),
				'\t',
				this.History.ToString(),
				"\r\n"
			});
		}

		public void String2Data(string Data)
		{
			char[] array = new char[] { '\t' };
			string[] array2 = Data.Split(array);
			this.Index = Convert.ToInt32(array2[0]);
			this.MyPlaneFullName = array2[1];
			this.Rank = (DifficultLevel)Convert.ToInt32(array2[2]);
			this.Recorded = Convert.ToInt32(array2[3]);
			this.History = Convert.ToInt32(array2[4]);
		}
	}
}
