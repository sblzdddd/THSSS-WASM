using SlimDX.Multimedia;

namespace SlimDX.XAudio2
{
	public class SourceVoice : ComObject
	{
		public float Volume { get; set; }

		public VoiceState State
		{
			get
			{
				return default(VoiceState);
			}
		}

		public VoiceDetails VoiceDetails
		{
			get
			{
				return new VoiceDetails
				{
					InputChannels = 2
				};
			}
		}

		public FilterParameters FilterParameters { get; set; }

		public SourceVoice(XAudio2 device, WaveFormat format)
		{
			this.Volume = 1f;
		}

		public SourceVoice(XAudio2 device, WaveFormat format, VoiceFlags flags)
			: this(device, format)
		{
		}

		public void Start(PlayFlags flags)
		{
		}

		public void Stop()
		{
		}

		public void FlushSourceBuffers()
		{
		}

		public void SubmitSourceBuffer(AudioBuffer buffer)
		{
		}

		public void SubmitSourceBuffer(AudioBuffer buffer, int[] decodedPacketCumulativeBytes)
		{
		}

		public void SetOutputMatrix(int inputChannels, int outputChannels, float[] matrix)
		{
		}
	}
}
