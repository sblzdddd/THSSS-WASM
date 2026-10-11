using System;
using System.IO;
using SlimDX.Multimedia;
using SlimDX.XAudio2;

namespace Shooting
{
	public class XAudio2_PlayerV2
	{
		public bool loadSuccess { get; private set; }

		public long Position
		{
			get
			{
				return this.sourceVoice.State.SamplesPlayed / 44L;
			}
		}

		public int Volume
		{
			get
			{
#if BROWSER
				if (this.sourceVoice == null)
				{
					return 0;
				}
#endif
				return (int)(this.sourceVoice.Volume * 100f);
			}
			set
			{
#if BROWSER
				if (this.sourceVoice == null)
				{
					return;
				}
#endif
				if (0 <= value && value <= 100)
				{
					this.sourceVoice.Volume = (float)value / 100f;
				}
			}
		}

		public XAudio2_PlayerV2(string fileName, XAudio2 device)
		{
			this.device = device;
			this.loadSuccess = this.LoadSound(fileName);
		}

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

		public void Play()
		{
			if (this.loadSuccess)
			{
				this.Reset();
				this.sourceVoice.Start(PlayFlags.None);
			}
		}

		public void Play(int Volume)
		{
			int volume = this.Volume;
			this.Volume = Volume;
			this.Play();
			this.Volume = volume;
		}

		public void Resume()
		{
			if (this.loadSuccess)
			{
				this.sourceVoice.Start(PlayFlags.None);
			}
		}

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

		public void SetLoop(int LoopBegin, int LoopEnd)
		{
			this.SetLoop(0, 0, 255, LoopBegin, LoopEnd);
		}

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

		public void FilterON()
		{
#if BROWSER
			if (this.sourceVoice == null)
			{
				return;
			}
#endif
			FilterParameters filterParameters = default(FilterParameters);
			filterParameters.Type = FilterType.HighPassFilter;
			filterParameters.Frequency = 0.5f;
			filterParameters.OneOverQ = 2f;
			this.sourceVoice.FilterParameters = filterParameters;
			this.sourceVoice.SetOutputMatrix(2, 2, new float[] { 0f, 4f, 4f });
		}

		public void FilterOFF()
		{
#if BROWSER
			if (this.sourceVoice == null)
			{
				return;
			}
#endif
			FilterParameters filterParameters = default(FilterParameters);
			filterParameters.Type = FilterType.HighPassFilter;
			filterParameters.Frequency = 0f;
			filterParameters.OneOverQ = 2f;
			this.sourceVoice.FilterParameters = filterParameters;
			this.sourceVoice.SetOutputMatrix(2, 2, new float[] { 0f, 1f, 1f });
		}

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

		private XAudio2 device;

		private SourceVoice sourceVoice;

		private AudioBuffer buffer;

		private bool xwmaMode;
	}
}
