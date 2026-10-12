using System;
using System.IO;
using System.Text;

namespace Shooting
{
	public class Replay
	{
		private FileStream ReplayData { get; set; }

		private string FileName { get; set; }

		public long DataPosition
		{
			get
			{
				return ReplayData.Position;
			}
			set
			{
				ReplayData.Position = value;
			}
		}

		public bool CanRead
		{
			get
			{
				return ReplayData.CanRead;
			}
		}

		public static ReplayInfo ReadTitle(string fileName)
		{
			var array = File.ReadAllLines(fileName, Encoding.Default);
			ReplayInfo replayInfo2;
			try
			{
				var num = 0;
				while (array[++num] != "ReplayInformation" && num < array.Length - 1)
				{
				}
				var replayInfo = new ReplayInfo
				{
					Version = array[num + 1],
					PlayerName = array[num + 2],
					Date = array[num + 3],
					Time = array[num + 4],
					MyPlaneName = array[num + 5],
					WeaponType = array[num + 6],
					Rank = (DifficultLevel)Convert.ToInt32(array[num + 7]),
					StartStage = array[num + 8],
					LastStage = array[num + 9],
					SlowRate = array[num + 10]
				};
				for (var i = num + 11; i < array.Length; i++)
				{
					var array2 = new char[] { '\t' };
					var array3 = array[i].Split(array2);
					var myPlaneInfo = new MyPlaneInfo
					{
						Life = Convert.ToInt32(array3[0]),
						Spell = Convert.ToInt32(array3[1]),
						Power = Convert.ToInt32(array3[2]),
						Score = Convert.ToInt64(array3[3]),
						Graze = Convert.ToInt32(array3[4]),
						PosX = (float)Convert.ToDouble(array3[5]),
						PosY = (float)Convert.ToDouble(array3[6]),
						LifeChip = Convert.ToInt32(array3[7]),
						SpellChip = Convert.ToInt32(array3[8]),
						LifeUpCount = Convert.ToInt32(array3[9]),
						StarPoint = Convert.ToInt32(array3[10]),
						HighItemScore = Convert.ToInt32(array3[11]),
						Rate = (float)Convert.ToDouble(array3[12]),
						LastColor = (EnchantmentType)Convert.ToInt32(array3[13]),
						DataPosition = Convert.ToInt64(array3[14])
					};
					replayInfo.MyPlaneData.Add(myPlaneInfo);
				}
				replayInfo2 = replayInfo;
			}
			catch
			{
				replayInfo2 = new ReplayInfo();
			}
			return replayInfo2;
		}

		public void WriteKey(int KeyValue)
		{
			var b = (byte)(KeyValue & 255);
			ReplayData.WriteByte(b);
			b = (byte)(KeyValue >> 8);
			b &= byte.MaxValue;
			ReplayData.WriteByte(b);
		}

		public int ReadKey()
		{
			var num = ReplayData.ReadByte();
			return num + (ReplayData.ReadByte() << 8);
		}

		public void CreatRpy()
		{
			ReplayData = new FileStream(".\\Replay\\AutoSave.rpy", FileMode.Create);
		}

		public void SaveRpy(string fileName, ReplayInfo repInfo)
		{
			if (ReplayData.CanRead)
			{
				ReplayData.WriteByte(240);
				ReplayData.WriteByte(15);
				string tmp = null;
				tmp += "\r\n";
				tmp += "ReplayInformation";
				tmp += "\r\n";
				tmp += "ver 1.00";
				tmp += "\r\n";
				tmp += repInfo.PlayerName;
				tmp += "\r\n";
				tmp += DateTime.Now.Date.ToString("yy'/'MM'/'dd");
				tmp += "\r\n";
				tmp += DateTime.Now.ToShortTimeString();
				tmp += "\r\n";
				tmp += repInfo.MyPlaneName;
				tmp += "\r\n";
				tmp += repInfo.WeaponType.ToString();
				tmp += "\r\n";
				tmp += ((int)repInfo.Rank).ToString();
				tmp += "\r\n";
				tmp += repInfo.StartStage;
				tmp += "\r\n";
				tmp += repInfo.LastStage;
				tmp += "\r\n";
				tmp += repInfo.SlowRate;
				repInfo.MyPlaneData.ForEach(delegate(MyPlaneInfo x)
				{
					tmp += "\r\n";
					object tmp2 = tmp;
					var array = new object[30];
					array[0] = tmp2;
					array[1] = x.Life.ToString();
					array[2] = '\t';
					array[3] = x.Spell.ToString();
					array[4] = '\t';
					array[5] = x.Power.ToString();
					array[6] = '\t';
					array[7] = x.Score.ToString();
					array[8] = '\t';
					array[9] = x.Graze.ToString();
					array[10] = '\t';
					array[11] = x.PosX.ToString();
					array[12] = '\t';
					array[13] = x.PosY.ToString();
					array[14] = '\t';
					array[15] = x.LifeChip.ToString();
					array[16] = '\t';
					array[17] = x.SpellChip.ToString();
					array[18] = '\t';
					array[19] = x.LifeUpCount.ToString();
					array[20] = '\t';
					array[21] = x.StarPoint.ToString();
					array[22] = '\t';
					array[23] = x.HighItemScore.ToString();
					array[24] = '\t';
					array[25] = x.Rate.ToString();
					array[26] = '\t';
					var array2 = array;
					var num = 27;
					var lastColor = (int)x.LastColor;
					array2[num] = lastColor.ToString();
					array[28] = '\t';
					array[29] = x.DataPosition.ToString();
					tmp = string.Concat(array);
				});
				var streamWriter = new StreamWriter(ReplayData);
				streamWriter.Write(tmp);
				streamWriter.Close();
				ReplayData.Close();
			}
			if (fileName != ".\\Replay\\AutoSave.rpy")
			{
				File.Copy(".\\Replay\\AutoSave.rpy", fileName, true);
			}
		}

		public void LoadRpy(string fileName)
		{
			FileName = fileName;
			var fileInfo = new FileInfo(fileName);
			ReplayData = fileInfo.OpenRead();
		}

		public void ReloadRpy()
		{
			var fileInfo = new FileInfo(FileName);
			ReplayData = fileInfo.OpenRead();
		}

		public void CloseRpy()
		{
			if (ReplayData != null)
			{
				ReplayData.Close();
			}
		}

		public void Dispose()
		{
			if (ReplayData != null)
			{
				ReplayData.Close();
				ReplayData.Dispose();
			}
		}
	}
}
