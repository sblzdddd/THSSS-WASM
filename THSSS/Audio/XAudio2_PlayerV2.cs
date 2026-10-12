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
#if BROWSER
				if (sourceVoice == null)
				{
					return 0L;
				}
#endif
				return sourceVoice.State.SamplesPlayed / 44L;
			}
		}

		public int Volume
		{
			get
			{
#if BROWSER
				if (sourceVoice == null)
				{
					return 0;
				}
#endif
				return (int)(sourceVoice.Volume * 100f);
			}
			set
			{
#if BROWSER
				if (sourceVoice == null)
				{
					return;
				}
#endif
				if (0 <= value && value <= 100)
				{
					sourceVoice.Volume = (float)value / 100f;
				}
			}
		}

		public XAudio2_PlayerV2(string fileName, XAudio2 device)
		{
			this.device = device;
			loadSuccess = LoadSound(fileName);
		}

		private bool LoadSound(string fileName)
		{
			bool flag;
			try
			{
#if BROWSER
				if (string.IsNullOrEmpty(fileName))
				{
					return false;
				}
				FileInfo fileInfo = new FileInfo(fileName);
				xwmaMode = string.Equals(fileInfo.Extension, ".xwma", StringComparison.OrdinalIgnoreCase);
				buffer = new AudioBuffer();
				buffer.Flags = BufferFlags.EndOfStream;
				sourceVoice = new SourceVoice(device, null, VoiceFlags.Music | VoiceFlags.UseFilter);
				sourceVoice.Bind(fileName);
				Volume = 100;
				flag = true;
#else
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
#endif
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		public void Play()
		{
			if (loadSuccess)
			{
				Reset();
				sourceVoice.Start(PlayFlags.None);
			}
		}

		public void Play(int Volume)
		{
			int volume = this.Volume;
			this.Volume = Volume;
			Play();
			this.Volume = volume;
		}

		public void Resume()
		{
			if (loadSuccess)
			{
				sourceVoice.Start(PlayFlags.None);
			}
		}

		public void Stop()
		{
			if (loadSuccess)
			{
				try
				{
					sourceVoice.Stop();
				}
				catch
				{
				}
			}
		}

		public void Reset()
		{
			if (loadSuccess)
			{
				if (xwmaMode)
				{
					sourceVoice.SubmitSourceBuffer(buffer, ((xWMAStream)buffer.AudioData).DecodedPacketCumulativeBytes);
				}
				else
				{
					sourceVoice.Stop();
					sourceVoice.FlushSourceBuffers();
					sourceVoice.SubmitSourceBuffer(buffer);
				}
			}
		}

		public void SetLoop(int LoopBegin, int LoopEnd)
		{
			SetLoop(0, 0, 255, LoopBegin, LoopEnd);
		}

		public void SetLoop(int PlayBegin, int PlayLength, int LoopCount, int LoopBegin, int LoopEnd)
		{
#if BROWSER
			if (buffer == null)
			{
				return;
			}
#endif
			buffer.PlayBegin = PlayBegin;
			buffer.PlayLength = PlayLength;
			buffer.LoopCount = LoopCount;
			buffer.LoopBegin = LoopBegin;
			if (LoopEnd > LoopBegin)
			{
				buffer.LoopLength = LoopEnd - LoopBegin;
			}
		}

		public void FilterON()
		{
#if BROWSER
			if (sourceVoice == null)
			{
				return;
			}
#endif
			FilterParameters filterParameters = default(FilterParameters);
			filterParameters.Type = FilterType.HighPassFilter;
			filterParameters.Frequency = 0.5f;
			filterParameters.OneOverQ = 2f;
			sourceVoice.FilterParameters = filterParameters;
			sourceVoice.SetOutputMatrix(2, 2, new float[] { 0f, 4f, 4f });
		}

		public void FilterOFF()
		{
#if BROWSER
			if (sourceVoice == null)
			{
				return;
			}
#endif
			FilterParameters filterParameters = default(FilterParameters);
			filterParameters.Type = FilterType.HighPassFilter;
			filterParameters.Frequency = 0f;
			filterParameters.OneOverQ = 2f;
			sourceVoice.FilterParameters = filterParameters;
			sourceVoice.SetOutputMatrix(2, 2, new float[] { 0f, 1f, 1f });
		}

		public void Dispose()
		{
			if (loadSuccess)
			{
				Stop();
				sourceVoice.Dispose();
#if BROWSER
				if (buffer.AudioData != null)
				{
					buffer.AudioData.Dispose();
				}
#else
				this.buffer.AudioData.Dispose();
#endif
				buffer.Dispose();
			}
		}

		private XAudio2 device;

		private SourceVoice sourceVoice;

		private AudioBuffer buffer;

		private bool xwmaMode;
	}
}
