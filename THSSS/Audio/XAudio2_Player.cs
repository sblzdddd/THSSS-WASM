using System;
using System.IO;
using SlimDX.Multimedia;
using SlimDX.XAudio2;

namespace Shooting
{
	public class XAudio2_Player
	{
		public float VoicePos { get; set; }

		public bool loadSuccess { get; private set; }

		public long Position
		{
			get
			{
#if BROWSER
				if (this.sourceVoice == null)
				{
					return 0L;
				}
#endif
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
				return (int)(this.sourceVoice.Volume * 200f);
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
					this.sourceVoice.Volume = (float)value / 200f;
				}
			}
		}

		public XAudio2_Player(string fileName, XAudio2 device)
		{
			this.device = device;
			this.loadSuccess = this.LoadSound(fileName);
		}

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

		private void Reset()
		{
			if (this.loadSuccess)
			{
				this.sourceVoice.Stop();
				this.sourceVoice.FlushSourceBuffers();
				this.sourceVoice.SubmitSourceBuffer(this.buffer);
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

		public void Dispose()
		{
			if (this.loadSuccess)
			{
				this.Stop();
				this.sourceVoice.Dispose();
				this.buffer.Dispose();
			}
		}

		private XAudio2 device;

		private SourceVoice sourceVoice;

		private AudioBuffer buffer;
	}
}
