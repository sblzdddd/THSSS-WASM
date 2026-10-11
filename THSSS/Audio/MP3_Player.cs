using System;
using System.Runtime.InteropServices;

namespace Shooting
{
	// Token: 0x0200014A RID: 330
	public class MP3_Player
	{
		// Token: 0x06000889 RID: 2185 RVA: 0x0004E8D8 File Offset: 0x0004CAD8
		public MP3_Player()
		{
			this.Volumn = 100;
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x0004E924 File Offset: 0x0004CB24
		// (set) Token: 0x0600088B RID: 2187 RVA: 0x0004E944 File Offset: 0x0004CB44
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

		// Token: 0x0600088C RID: 2188 RVA: 0x0004EAC4 File Offset: 0x0004CCC4
		public void Play()
		{
			this.TemStr = "";
			this.TemStr = this.TemStr.PadLeft(127, Convert.ToChar(" "));
			APIClass.mciSendString("play media", this.TemStr, this.TemStr.Length, 0);
			this.mc.state = MP3_Player.State.mPlaying;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0004EB24 File Offset: 0x0004CD24
		public void PlayRepeat()
		{
			this.TemStr = "";
			this.TemStr = this.TemStr.PadLeft(127, Convert.ToChar(" "));
			APIClass.mciSendString("play media repeat", this.TemStr, this.TemStr.Length, 0);
			this.mc.state = MP3_Player.State.mPlaying;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0004EB84 File Offset: 0x0004CD84
		public void Stop()
		{
			this.TemStr = "";
			this.TemStr = this.TemStr.PadLeft(128, Convert.ToChar(" "));
			this.ilong = APIClass.mciSendString("close media", this.TemStr, 128, 0);
			this.ilong = APIClass.mciSendString("close all", this.TemStr, 128, 0);
			this.mc.state = MP3_Player.State.mStop;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0004EC04 File Offset: 0x0004CE04
		public void Puase()
		{
			this.TemStr = "";
			this.TemStr = this.TemStr.PadLeft(128, Convert.ToChar(" "));
			this.ilong = APIClass.mciSendString("pause media", this.TemStr, this.TemStr.Length, 0);
			this.mc.state = MP3_Player.State.mPuase;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0004EC6C File Offset: 0x0004CE6C
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

		// Token: 0x06000891 RID: 2193 RVA: 0x0004ECE8 File Offset: 0x0004CEE8
		public void SetStartEnd(int t1, int t2)
		{
			this.TemStr = "";
			this.TemStr = this.TemStr.PadLeft(128, Convert.ToChar(" "));
			string text = t1.ToString();
			string text2 = t2.ToString();
			string text3 = string.Concat(new string[] { "play media from ", text, " to ", text2, " repeat" });
			APIClass.mciSendString(text3, this.TemStr, this.TemStr.Length, 0);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0004ED78 File Offset: 0x0004CF78
		public void SetStartEnd(float t1, float t2)
		{
			this.SetStartEnd((int)t1 * 1000, (int)t2 * 1000);
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0004ED94 File Offset: 0x0004CF94
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

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x0004EDD8 File Offset: 0x0004CFD8
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

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x0004EE74 File Offset: 0x0004D074
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

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x0004EF00 File Offset: 0x0004D100
		// (set) Token: 0x06000897 RID: 2199 RVA: 0x0004EF18 File Offset: 0x0004D118
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

		// Token: 0x04000305 RID: 773
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		private string Name = "";

		// Token: 0x04000306 RID: 774
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		private string durLength = "";

		// Token: 0x04000307 RID: 775
		[MarshalAs(UnmanagedType.LPTStr)]
		private string TemStr = "";

		// Token: 0x04000308 RID: 776
		private int ilong;

		// Token: 0x04000309 RID: 777
		private int volumn;

		// Token: 0x0400030A RID: 778
		public MP3_Player.structMCI mc = default(MP3_Player.structMCI);

		// Token: 0x0200014B RID: 331
		public enum State
		{
			// Token: 0x0400030C RID: 780
			mPlaying = 1,
			// Token: 0x0400030D RID: 781
			mPuase,
			// Token: 0x0400030E RID: 782
			mStop
		}

		// Token: 0x0200014C RID: 332
		public struct structMCI
		{
			// Token: 0x0400030F RID: 783
			public bool bMut;

			// Token: 0x04000310 RID: 784
			public int iDur;

			// Token: 0x04000311 RID: 785
			public int iPos;

			// Token: 0x04000312 RID: 786
			public int iVol;

			// Token: 0x04000313 RID: 787
			public int iBal;

			// Token: 0x04000314 RID: 788
			public string iName;

			// Token: 0x04000315 RID: 789
			public MP3_Player.State state;
		}
	}
}
