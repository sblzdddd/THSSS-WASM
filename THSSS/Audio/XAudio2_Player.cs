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
				return (int)(sourceVoice.Volume * 200f);
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
					sourceVoice.Volume = (float)value / 200f;
				}
			}
		}

		public XAudio2_Player(string fileName, XAudio2 device)
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
				buffer = new AudioBuffer();
				buffer.Flags = BufferFlags.EndOfStream;
				sourceVoice = new SourceVoice(device, null);
				sourceVoice.Bind(fileName);
				Volume = 40;
				flag = true;
#else
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
				var num = VoicePos * 0.4f + 0.3f;
				var inputChannels = sourceVoice.VoiceDetails.InputChannels;
				var array = new float[]
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
				sourceVoice.SetOutputMatrix(inputChannels, 2, array);
				Reset();
				sourceVoice.Start(PlayFlags.None);
			}
		}

		public void Play(int Volume)
		{
			var volume = this.Volume;
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

		private void Reset()
		{
			if (loadSuccess)
			{
				sourceVoice.Stop();
				sourceVoice.FlushSourceBuffers();
				sourceVoice.SubmitSourceBuffer(buffer);
			}
		}

		public void SetLoop(int LoopBegin, int LoopEnd)
		{
			SetLoop(0, 0, 255, LoopBegin, LoopEnd);
		}

		public void SetLoop(int PlayBegin, int PlayLength, int LoopCount, int LoopBegin, int LoopEnd)
		{
			buffer.PlayBegin = PlayBegin;
			buffer.PlayLength = PlayLength;
			buffer.LoopCount = LoopCount;
			buffer.LoopBegin = LoopBegin;
			if (LoopEnd > LoopBegin)
			{
				buffer.LoopLength = LoopEnd - LoopBegin;
			}
		}

		public void Dispose()
		{
			if (loadSuccess)
			{
				Stop();
				sourceVoice.Dispose();
				buffer.Dispose();
			}
		}

		private XAudio2 device;

		private SourceVoice sourceVoice;

		private AudioBuffer buffer;
	}
}
