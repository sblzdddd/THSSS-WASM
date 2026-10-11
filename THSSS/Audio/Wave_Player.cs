using System;
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
				return this.fileName[(int)this.AudioIndex];
			}
			set
			{
				if (this.fileName[(int)(1 - this.AudioIndex)] == value)
				{
					this.AudioIndex = (byte)(1 - this.AudioIndex);
				}
				else
				{
					int num = 100;
					this.fileName[(int)this.AudioIndex] = value;
					if (this.x2p[(int)this.AudioIndex] != null)
					{
						num = this.x2p[(int)this.AudioIndex].Volume;
						this.x2p[(int)this.AudioIndex].Dispose();
					}
					this.x2p[(int)this.AudioIndex] = new XAudio2_PlayerV2(this.fileName[(int)this.AudioIndex], this.device);
					this.x2p[(int)this.AudioIndex].Volume = num;
				}
			}
		}

		public long CurrentPosition
		{
			get
			{
				return this.x2p[(int)this.AudioIndex].Position;
			}
		}

		public int Volume
		{
			get
			{
				int num;
				if (this.x2p[(int)this.AudioIndex] != null)
				{
					num = this.x2p[(int)this.AudioIndex].Volume;
				}
				else
				{
					num = 100;
				}
				return num;
			}
			set
			{
				if (this.x2p[(int)this.AudioIndex] != null)
				{
					this.x2p[(int)this.AudioIndex].Volume = value;
				}
			}
		}

		public Wave_Player(XAudio2 device)
		{
			this.Volume = 100;
			this.device = device;
		}

		public void Play()
		{
			this.x2p[(int)this.AudioIndex].Play();
		}

		public void PlayRepeat()
		{
			this.x2p[(int)this.AudioIndex].SetLoop(0, 0);
			this.x2p[(int)this.AudioIndex].Play();
		}

		public void PlayRepeat(int LoopBegin, int LoopEnd)
		{
			this.PlayRepeat(0, 0, 255, LoopBegin, LoopEnd);
		}

		public void PlayRepeat(int PlayBegin, int PlayLength, int LoopCount, int LoopBegin, int LoopEnd)
		{
			this.x2p[(int)this.AudioIndex].Stop();
			this.x2p[(int)this.AudioIndex].SetLoop(PlayBegin, PlayLength, LoopCount, LoopBegin, LoopEnd);
			this.x2p[(int)this.AudioIndex].Play();
		}

		public void Stop()
		{
			if (this.x2p[(int)this.AudioIndex] != null)
			{
				this.x2p[(int)this.AudioIndex].Stop();
			}
		}

		public void Puase()
		{
			this.OnPause = true;
			this.Stop();
		}

		public void Resume()
		{
			this.OnPause = false;
			this.x2p[(int)this.AudioIndex].Resume();
		}

		public void PreLoad(string FileName)
		{
			this.FileName = FileName;
			this.x2p[(int)this.AudioIndex].Reset();
		}

		public void FilterON()
		{
			this.x2p[(int)this.AudioIndex].FilterON();
		}

		public void FilterOFF()
		{
			this.x2p[(int)this.AudioIndex].FilterOFF();
		}

		public void Dispose()
		{
			for (int i = 0; i < this.x2p.Length; i++)
			{
				if (this.x2p[(int)this.AudioIndex] != null)
				{
					this.x2p[(int)this.AudioIndex].Dispose();
				}
			}
		}

		private XAudio2_PlayerV2[] x2p = new XAudio2_PlayerV2[2];

		private byte AudioIndex = 0;

		private XAudio2 device;

		private string[] fileName = new string[2];
	}
}
