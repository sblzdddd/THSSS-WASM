using SlimDX.XAudio2;

namespace Shooting
{
	public class Wave_Player
	{
		public bool OnPause { get; set; }

		public string FileName
		{
			get
			{
				return fileName[(int)AudioIndex];
			}
			set
			{
				if (fileName[(int)(1 - AudioIndex)] == value)
				{
					AudioIndex = (byte)(1 - AudioIndex);
				}
				else
				{
					int num = 100;
					fileName[(int)AudioIndex] = value;
					if (x2p[(int)AudioIndex] != null)
					{
						num = x2p[(int)AudioIndex].Volume;
						x2p[(int)AudioIndex].Dispose();
					}
					x2p[(int)AudioIndex] = new XAudio2_PlayerV2(fileName[(int)AudioIndex], device);
					x2p[(int)AudioIndex].Volume = num;
				}
			}
		}

		public long CurrentPosition
		{
			get
			{
				return x2p[(int)AudioIndex].Position;
			}
		}

		public int Volume
		{
			get
			{
				int num;
				if (x2p[(int)AudioIndex] != null)
				{
					num = x2p[(int)AudioIndex].Volume;
				}
				else
				{
					num = 100;
				}
				return num;
			}
			set
			{
				if (x2p[(int)AudioIndex] != null)
				{
					x2p[(int)AudioIndex].Volume = value;
				}
			}
		}

		public Wave_Player(XAudio2 device)
		{
			Volume = 100;
			this.device = device;
		}

		public void Play()
		{
			x2p[(int)AudioIndex].Play();
		}

		public void PlayRepeat()
		{
			x2p[(int)AudioIndex].SetLoop(0, 0);
			x2p[(int)AudioIndex].Play();
		}

		public void PlayRepeat(int LoopBegin, int LoopEnd)
		{
			PlayRepeat(0, 0, 255, LoopBegin, LoopEnd);
		}

		public void PlayRepeat(int PlayBegin, int PlayLength, int LoopCount, int LoopBegin, int LoopEnd)
		{
			x2p[(int)AudioIndex].Stop();
			x2p[(int)AudioIndex].SetLoop(PlayBegin, PlayLength, LoopCount, LoopBegin, LoopEnd);
			x2p[(int)AudioIndex].Play();
		}

		public void Stop()
		{
			if (x2p[(int)AudioIndex] != null)
			{
				x2p[(int)AudioIndex].Stop();
			}
		}

		public void Puase()
		{
			OnPause = true;
			Stop();
		}

		public void Resume()
		{
			OnPause = false;
			x2p[(int)AudioIndex].Resume();
		}

		public void PreLoad(string FileName)
		{
			this.FileName = FileName;
			x2p[(int)AudioIndex].Reset();
		}

		public void FilterON()
		{
			x2p[(int)AudioIndex].FilterON();
		}

		public void FilterOFF()
		{
			x2p[(int)AudioIndex].FilterOFF();
		}

		public void Dispose()
		{
			for (int i = 0; i < x2p.Length; i++)
			{
				if (x2p[(int)AudioIndex] != null)
				{
					x2p[(int)AudioIndex].Dispose();
				}
			}
		}

		private XAudio2_PlayerV2[] x2p = new XAudio2_PlayerV2[2];

		private byte AudioIndex = 0;

		private XAudio2 device;

		private string[] fileName = new string[2];
	}
}
