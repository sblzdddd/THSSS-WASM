using System;
using System.IO;
using SlimDX.Multimedia;
using SlimDX.XAudio2;

namespace Shooting
{
	// Token: 0x02000171 RID: 369
	public class XAudio2_Player
	{
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x00056778 File Offset: 0x00054978
		// (set) Token: 0x06000905 RID: 2309 RVA: 0x0005678F File Offset: 0x0005498F
		public float VoicePos { get; set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x00056798 File Offset: 0x00054998
		// (set) Token: 0x06000907 RID: 2311 RVA: 0x000567AF File Offset: 0x000549AF
		public bool loadSuccess { get; private set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x000567B8 File Offset: 0x000549B8
		public long Position
		{
			get
			{
				return this.sourceVoice.State.SamplesPlayed / 44L;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x000567E4 File Offset: 0x000549E4
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x00056808 File Offset: 0x00054A08
		public int Volume
		{
			get
			{
				return (int)(this.sourceVoice.Volume * 200f);
			}
			set
			{
				if (0 <= value && value <= 100)
				{
					this.sourceVoice.Volume = (float)value / 200f;
				}
			}
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x0005683C File Offset: 0x00054A3C
		public XAudio2_Player(string fileName, XAudio2 device)
		{
			this.device = device;
			this.loadSuccess = this.LoadSound(fileName);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00056864 File Offset: 0x00054A64
		private bool LoadSound(string fileName)
		{
			bool flag;
			try
			{
				FileStream fileStream = File.OpenRead(fileName);
				WaveStream waveStream = new WaveStream(fileStream);
				fileStream.Close();
				this.buffer = new AudioBuffer();
				this.buffer.AudioData = waveStream;
				this.buffer.AudioBytes = (int)waveStream.Length;
				this.buffer.Flags = BufferFlags.EndOfStream;
				this.sourceVoice = new SourceVoice(this.device, waveStream.Format);
				this.Volume = 40;
				flag = true;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00056900 File Offset: 0x00054B00
		public void Play()
		{
			if (this.loadSuccess)
			{
				float num = this.VoicePos * 0.4f + 0.3f;
				int inputChannels = this.sourceVoice.VoiceDetails.InputChannels;
				float[] array = new float[]
				{
					1f - num,
					num
				};
				if (inputChannels == 2)
				{
					array = new float[]
					{
						0f,
						1f - num,
						num
					};
				}
				this.sourceVoice.SetOutputMatrix(inputChannels, 2, array);
				this.Reset();
				this.sourceVoice.Start(PlayFlags.None);
			}
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x000569AC File Offset: 0x00054BAC
		public void Play(int Volume)
		{
			int volume = this.Volume;
			this.Volume = Volume;
			this.Play();
			this.Volume = volume;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x000569D8 File Offset: 0x00054BD8
		public void Resume()
		{
			if (this.loadSuccess)
			{
				this.sourceVoice.Start(PlayFlags.None);
			}
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00056A00 File Offset: 0x00054C00
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

		// Token: 0x06000911 RID: 2321 RVA: 0x00056A44 File Offset: 0x00054C44
		private void Reset()
		{
			if (this.loadSuccess)
			{
				this.sourceVoice.Stop();
				this.sourceVoice.FlushSourceBuffers();
				this.sourceVoice.SubmitSourceBuffer(this.buffer);
			}
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00056A89 File Offset: 0x00054C89
		public void SetLoop(int LoopBegin, int LoopEnd)
		{
			this.SetLoop(0, 0, 255, LoopBegin, LoopEnd);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00056A9C File Offset: 0x00054C9C
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

		// Token: 0x06000914 RID: 2324 RVA: 0x00056B00 File Offset: 0x00054D00
		public void Dispose()
		{
			if (this.loadSuccess)
			{
				this.Stop();
				this.sourceVoice.Dispose();
				this.buffer.Dispose();
			}
		}

		// Token: 0x04000357 RID: 855
		private XAudio2 device;

		// Token: 0x04000358 RID: 856
		private SourceVoice sourceVoice;

		// Token: 0x04000359 RID: 857
		private AudioBuffer buffer;
	}
}
