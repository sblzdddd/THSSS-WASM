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
				Index.ToString(),
				'\t',
				MyPlaneFullName,
				'\t',
				((int)Rank).ToString(),
				'\t',
				Recorded.ToString(),
				'\t',
				History.ToString(),
				"\r\n"
			});
		}

		public void String2Data(string Data)
		{
			var array = new char[] { '\t' };
			var array2 = Data.Split(array);
			Index = Convert.ToInt32(array2[0]);
			MyPlaneFullName = array2[1];
			Rank = (DifficultLevel)Convert.ToInt32(array2[2]);
			Recorded = Convert.ToInt32(array2[3]);
			History = Convert.ToInt32(array2[4]);
		}
	}
}
