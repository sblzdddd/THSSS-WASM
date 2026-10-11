using System;
using System.Runtime.InteropServices;

namespace Shooting
{
	public class MP3_Player
	{
		public MP3_Player()
		{
			this.Volumn = 100;
		}

		public string FileName
		{
			get
			{
				return this.mc.iName;
			}
			set
			{
				try
				{
					this.TemStr = "";
					this.TemStr = this.TemStr.PadLeft(127, Convert.ToChar(" "));
					this.Name = this.Name.PadLeft(260, Convert.ToChar(" "));
					this.mc.iName = value;
					this.ilong = APIClass.GetShortPathName(this.mc.iName, this.Name, this.Name.Length);
					this.Name = this.GetCurrPath(this.Name);
					this.Name = string.Concat(new object[]
					{
						"open ",
						Convert.ToChar(34),
						this.Name,
						Convert.ToChar(34),
						" alias media"
					});
					this.ilong = APIClass.mciSendString("close all", this.TemStr, this.TemStr.Length, 0);
					this.ilong = APIClass.mciSendString(this.Name, this.TemStr, this.TemStr.Length, 0);
					this.ilong = APIClass.mciSendString("set media time format milliseconds", this.TemStr, this.TemStr.Length, 0);
					this.mc.state = MP3_Player.State.mStop;
				}
				catch
				{
				}
			}
		}

		public void Play()
		{
			this.TemStr = "";
			this.TemStr = this.TemStr.PadLeft(127, Convert.ToChar(" "));
			APIClass.mciSendString("play media", this.TemStr, this.TemStr.Length, 0);
			this.mc.state = MP3_Player.State.mPlaying;
		}

		public void PlayRepeat()
		{
			this.TemStr = "";
			this.TemStr = this.TemStr.PadLeft(127, Convert.ToChar(" "));
			APIClass.mciSendString("play media repeat", this.TemStr, this.TemStr.Length, 0);
			this.mc.state = MP3_Player.State.mPlaying;
		}

		public void Stop()
		{
			this.TemStr = "";
			this.TemStr = this.TemStr.PadLeft(128, Convert.ToChar(" "));
			this.ilong = APIClass.mciSendString("close media", this.TemStr, 128, 0);
			this.ilong = APIClass.mciSendString("close all", this.TemStr, 128, 0);
			this.mc.state = MP3_Player.State.mStop;
		}

		public void Puase()
		{
			this.TemStr = "";
			this.TemStr = this.TemStr.PadLeft(128, Convert.ToChar(" "));
			this.ilong = APIClass.mciSendString("pause media", this.TemStr, this.TemStr.Length, 0);
			this.mc.state = MP3_Player.State.mPuase;
		}

		public void Resume()
		{
			if (this.mc.state == MP3_Player.State.mPuase)
			{
				this.TemStr = "";
				this.TemStr = this.TemStr.PadLeft(127, Convert.ToChar(" "));
				this.ilong = APIClass.mciSendString("resume media", this.TemStr, this.TemStr.Length, 0);
				this.mc.state = MP3_Player.State.mPlaying;
			}
		}

		public void SetStartEnd(int t1, int t2)
		{
			this.TemStr = "";
			this.TemStr = this.TemStr.PadLeft(128, Convert.ToChar(" "));
			string text = t1.ToString();
			string text2 = t2.ToString();
			string text3 = string.Concat(new string[] { "play media from ", text, " to ", text2, " repeat" });
			APIClass.mciSendString(text3, this.TemStr, this.TemStr.Length, 0);
		}

		public void SetStartEnd(float t1, float t2)
		{
			this.SetStartEnd((int)t1 * 1000, (int)t2 * 1000);
		}

		private string GetCurrPath(string name)
		{
			string text;
			if (name.Length < 1)
			{
				text = "";
			}
			else
			{
				name = name.Trim();
				name = name.Substring(0, name.Length - 1);
				text = name;
			}
			return text;
		}

		public int Duration
		{
			get
			{
				this.durLength = "";
				this.durLength = this.durLength.PadLeft(128, Convert.ToChar(" "));
				APIClass.mciSendString("status media length", this.durLength, this.durLength.Length, 0);
				this.durLength = this.durLength.Trim();
				int num;
				if (this.durLength == "")
				{
					num = 0;
				}
				else
				{
					num = (int)(Convert.ToDouble(this.durLength) / 1000.0);
				}
				return num;
			}
		}

		public int CurrentPosition
		{
			get
			{
				this.durLength = "";
				this.durLength = this.durLength.PadLeft(128, Convert.ToChar(" "));
				APIClass.mciSendString("status media position", this.durLength, this.durLength.Length, 0);
				this.mc.iPos = (int)Convert.ToDouble(this.durLength.Replace("\0", ""));
				return this.mc.iPos;
			}
		}

		public int Volumn
		{
			get
			{
				return this.volumn;
			}
			set
			{
				if (value < 0)
				{
					this.volumn = 0;
				}
				else if (value > 100)
				{
					this.volumn = 100;
				}
				else
				{
					this.volumn = value;
				}
				this.TemStr = "";
				this.TemStr = this.TemStr.PadLeft(127, Convert.ToChar(" "));
				APIClass.mciSendString("setaudio media volume to " + (value * 10).ToString(), this.TemStr, this.TemStr.Length, 0);
			}
		}

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		private string Name = "";

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		private string durLength = "";

		[MarshalAs(UnmanagedType.LPTStr)]
		private string TemStr = "";

		private int ilong;

		private int volumn;

		public MP3_Player.structMCI mc = default(MP3_Player.structMCI);

		public enum State
		{
			mPlaying = 1,
			mPuase,
			mStop
		}

		public struct structMCI
		{
			public bool bMut;

			public int iDur;

			public int iPos;

			public int iVol;

			public int iBal;

			public string iName;

			public MP3_Player.State state;
		}
	}
}
