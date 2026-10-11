using System;
using System.IO;
using SlimDX.Multimedia;
using SlimDX.XAudio2;

namespace Shooting
{
	// Token: 0x02000082 RID: 130
	public class XAudio2_PlayerV2
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0001F408 File Offset: 0x0001D608
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x0001F41F File Offset: 0x0001D61F
		public bool loadSuccess { get; private set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0001F428 File Offset: 0x0001D628
		public long Position
		{
			get
			{
				return this.sourceVoice.State.SamplesPlayed / 44L;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0001F454 File Offset: 0x0001D654
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x0001F478 File Offset: 0x0001D678
		public int Volume
		{
			get
			{
				return (int)(this.sourceVoice.Volume * 100f);
			}
			set
			{
				if (0 <= value && value <= 100)
				{
					this.sourceVoice.Volume = (float)value / 100f;
				}
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001F4AC File Offset: 0x0001D6AC
		public XAudio2_PlayerV2(string fileName, XAudio2 device)
		{
			this.device = device;
			this.loadSuccess = this.LoadSound(fileName);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001F4D4 File Offset: 0x0001D6D4
		private bool LoadSound(string fileName)
		{
			bool flag;
			try
			{
				FileInfo fileInfo = new FileInfo(fileName);
				if (fileInfo.Extension == ".xwma")
				{
					xWMAStream xWMAStream = new xWMAStream(fileName);
					this.buffer = new AudioBuffer();
					this.buffer.AudioData = xWMAStream;
					this.buffer.AudioBytes = (int)xWMAStream.Length;
					this.buffer.Flags = BufferFlags.EndOfStream;
					this.sourceVoice = new SourceVoice(this.device, xWMAStream.Format, VoiceFlags.Music | VoiceFlags.UseFilter);
					this.xwmaMode = true;
				}
				else if (fileInfo.Extension == ".wav")
				{
					WaveStream waveStream = new WaveStream(fileName);
					this.buffer = new AudioBuffer();
					this.buffer.AudioData = waveStream;
					this.buffer.AudioBytes = (int)waveStream.Length;
					this.buffer.Flags = BufferFlags.EndOfStream;
					this.sourceVoice = new SourceVoice(this.device, waveStream.Format, VoiceFlags.Music | VoiceFlags.UseFilter);
				}
				this.Volume = 100;
				flag = true;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0001F60C File Offset: 0x0001D80C
		public void Play()
		{
			if (this.loadSuccess)
			{
				this.Reset();
				this.sourceVoice.Start(PlayFlags.None);
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0001F63C File Offset: 0x0001D83C
		public void Play(int Volume)
		{
			int volume = this.Volume;
			this.Volume = Volume;
			this.Play();
			this.Volume = volume;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0001F668 File Offset: 0x0001D868
		public void Resume()
		{
			if (this.loadSuccess)
			{
				this.sourceVoice.Start(PlayFlags.None);
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001F690 File Offset: 0x0001D890
		public void Stop()
		{
			if (this.loadSuccess)
			{
				try
				{
					this.sourceVoice.Stop();
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001F6D4 File Offset: 0x0001D8D4
		public void Reset()
		{
			if (this.loadSuccess)
			{
				if (this.xwmaMode)
				{
					this.sourceVoice.SubmitSourceBuffer(this.buffer, ((xWMAStream)this.buffer.AudioData).DecodedPacketCumulativeBytes);
				}
				else
				{
					this.sourceVoice.Stop();
					this.sourceVoice.FlushSourceBuffers();
					this.sourceVoice.SubmitSourceBuffer(this.buffer);
				}
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001F753 File Offset: 0x0001D953
		public void SetLoop(int LoopBegin, int LoopEnd)
		{
			this.SetLoop(0, 0, 255, LoopBegin, LoopEnd);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0001F768 File Offset: 0x0001D968
		public void SetLoop(int PlayBegin, int PlayLength, int LoopCount, int LoopBegin, int LoopEnd)
		{
			this.buffer.PlayBegin = PlayBegin;
			this.buffer.PlayLength = PlayLength;
			this.buffer.LoopCount = LoopCount;
			this.buffer.LoopBegin = LoopBegin;
			if (LoopEnd > LoopBegin)
			{
				this.buffer.LoopLength = LoopEnd - LoopBegin;
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001F7CC File Offset: 0x0001D9CC
		public void FilterON()
		{
			FilterParameters filterParameters = default(FilterParameters);
			filterParameters.Type = FilterType.HighPassFilter;
			filterParameters.Frequency = 0.5f;
			filterParameters.OneOverQ = 2f;
			this.sourceVoice.FilterParameters = filterParameters;
			this.sourceVoice.SetOutputMatrix(2, 2, new float[] { 0f, 4f, 4f });
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001F838 File Offset: 0x0001DA38
		public void FilterOFF()
		{
			FilterParameters filterParameters = default(FilterParameters);
			filterParameters.Type = FilterType.HighPassFilter;
			filterParameters.Frequency = 0f;
			filterParameters.OneOverQ = 2f;
			this.sourceVoice.FilterParameters = filterParameters;
			this.sourceVoice.SetOutputMatrix(2, 2, new float[] { 0f, 1f, 1f });
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001F8A4 File Offset: 0x0001DAA4
		public void Dispose()
		{
			if (this.loadSuccess)
			{
				this.Stop();
				this.sourceVoice.Dispose();
				this.buffer.AudioData.Dispose();
				this.buffer.Dispose();
			}
		}

		// Token: 0x04000149 RID: 329
		private XAudio2 device;

		// Token: 0x0400014A RID: 330
		private SourceVoice sourceVoice;

		// Token: 0x0400014B RID: 331
		private AudioBuffer buffer;

		// Token: 0x0400014C RID: 332
		private bool xwmaMode;
	}
}
