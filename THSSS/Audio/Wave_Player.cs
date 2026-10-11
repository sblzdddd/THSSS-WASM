using System;
using SlimDX.XAudio2;

namespace Shooting
{
	// Token: 0x020000E6 RID: 230
	public class Wave_Player
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x00037314 File Offset: 0x00035514
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x0003732B File Offset: 0x0003552B
		public bool OnPause { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x00037334 File Offset: 0x00035534
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x00037354 File Offset: 0x00035554
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

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0003741C File Offset: 0x0003561C
		public long CurrentPosition
		{
			get
			{
				return this.x2p[(int)this.AudioIndex].Position;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00037440 File Offset: 0x00035640
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x00037480 File Offset: 0x00035680
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

		// Token: 0x06000640 RID: 1600 RVA: 0x000374B8 File Offset: 0x000356B8
		public Wave_Player(XAudio2 device)
		{
			this.Volume = 100;
			this.device = device;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x000374F2 File Offset: 0x000356F2
		public void Play()
		{
			this.x2p[(int)this.AudioIndex].Play();
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00037508 File Offset: 0x00035708
		public void PlayRepeat()
		{
			this.x2p[(int)this.AudioIndex].SetLoop(0, 0);
			this.x2p[(int)this.AudioIndex].Play();
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00037533 File Offset: 0x00035733
		public void PlayRepeat(int LoopBegin, int LoopEnd)
		{
			this.PlayRepeat(0, 0, 255, LoopBegin, LoopEnd);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00037548 File Offset: 0x00035748
		public void PlayRepeat(int PlayBegin, int PlayLength, int LoopCount, int LoopBegin, int LoopEnd)
		{
			this.x2p[(int)this.AudioIndex].Stop();
			this.x2p[(int)this.AudioIndex].SetLoop(PlayBegin, PlayLength, LoopCount, LoopBegin, LoopEnd);
			this.x2p[(int)this.AudioIndex].Play();
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00037598 File Offset: 0x00035798
		public void Stop()
		{
			if (this.x2p[(int)this.AudioIndex] != null)
			{
				this.x2p[(int)this.AudioIndex].Stop();
			}
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x000375CF File Offset: 0x000357CF
		public void Puase()
		{
			this.OnPause = true;
			this.Stop();
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x000375E1 File Offset: 0x000357E1
		public void Resume()
		{
			this.OnPause = false;
			this.x2p[(int)this.AudioIndex].Resume();
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x000375FF File Offset: 0x000357FF
		public void PreLoad(string FileName)
		{
			this.FileName = FileName;
			this.x2p[(int)this.AudioIndex].Reset();
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0003761D File Offset: 0x0003581D
		public void FilterON()
		{
			this.x2p[(int)this.AudioIndex].FilterON();
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00037633 File Offset: 0x00035833
		public void FilterOFF()
		{
			this.x2p[(int)this.AudioIndex].FilterOFF();
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0003764C File Offset: 0x0003584C
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

		// Token: 0x04000224 RID: 548
		private XAudio2_PlayerV2[] x2p = new XAudio2_PlayerV2[2];

		// Token: 0x04000225 RID: 549
		private byte AudioIndex = 0;

		// Token: 0x04000226 RID: 550
		private XAudio2 device;

		// Token: 0x04000227 RID: 551
		private string[] fileName = new string[2];
	}
}
