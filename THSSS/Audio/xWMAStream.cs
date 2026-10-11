using System;
using System.IO;
using System.Text;
using SlimDX.Multimedia;

namespace Shooting
{
	internal class xWMAStream : Stream
	{
		public xWMAStream(Stream stream)
		{
			this.dataStream = stream;
			this.analyzeXWMAFileStream();
		}

		public xWMAStream(string path)
		{
			if (!File.Exists(path))
			{
				throw new FileNotFoundException(path + " Not Found");
			}
			this.dataStream = new FileStream(path, FileMode.Open);
			this.analyzeXWMAFileStream();
		}

		private void analyzeXWMAFileStream()
		{
			byte[] array = new byte[4];
			this.dataStream.Seek(0L, SeekOrigin.Begin);
			this.dataStream.Read(array, 0, 4);
			if (Encoding.ASCII.GetString(array) != "RIFF")
			{
				throw new InvalidDataException("Invalid xWMA file.");
			}
			this.dataStream.Read(array, 0, 4);
			int num = BitConverter.ToInt32(array, 0);
			if (this.dataStream.Length != (long)(num + 8))
			{
				throw new InvalidDataException("Invalid Format");
			}
			this.dataStream.Read(array, 0, 4);
			if (Encoding.ASCII.GetString(array) != "XWMA")
			{
				throw new InvalidDataException("Invalid xWMA file.");
			}
			long num2 = (long)this.dataStream.Read(array, 0, 4);
			while (num2 > 3L)
			{
				string @string = Encoding.ASCII.GetString(array);
				this.dataStream.Read(array, 0, 4);
				num = BitConverter.ToInt32(array, 0);
				string text = @string;
				if (text == null)
				{
					goto IL_0407;
				}
				if (!(text == "fmt "))
				{
					if (!(text == "dpds"))
					{
						if (!(text == "data"))
						{
							goto IL_0407;
						}
						this.dataOffset = this.dataStream.Position;
						this.dataSize = num;
						this.dataStream.Seek((long)num, SeekOrigin.Current);
					}
					else
					{
						if (num % 4 != 0)
						{
							throw new InvalidDataException("Invalid Format.");
						}
						int num3 = num / 4;
						this.decodedPacketCumulativeBytes = new int[num3];
						for (int i = 0; i < num3; i++)
						{
							this.dataStream.Read(array, 0, 4);
							this.decodedPacketCumulativeBytes[i] = BitConverter.ToInt32(array, 0);
						}
					}
				}
				else
				{
					this.dataStream.Read(array, 0, 2);
					ushort num4 = BitConverter.ToUInt16(array, 0);
					this.format = new WaveFormat();
					this.dataStream.Read(array, 0, 2);
					this.format.Channels = (short)BitConverter.ToUInt16(array, 0);
					this.dataStream.Read(array, 0, 4);
					this.format.SamplesPerSecond = BitConverter.ToInt32(array, 0);
					this.dataStream.Read(array, 0, 4);
					this.format.AverageBytesPerSecond = BitConverter.ToInt32(array, 0);
					this.dataStream.Read(array, 0, 2);
					this.format.BlockAlignment = BitConverter.ToInt16(array, 0);
					this.dataStream.Read(array, 0, 2);
					this.format.BitsPerSample = BitConverter.ToInt16(array, 0);
					if (num4 == 353)
					{
						this.dataStream.Seek((long)(num - 16), SeekOrigin.Current);
						this.format.FormatTag = WaveFormatTag.WMAudio2;
					}
					else
					{
						if (num4 != 65534)
						{
							throw new InvalidDataException("Invalid xWMA file.");
						}
						WaveFormatExtensible waveFormatExtensible = new WaveFormatExtensible();
						waveFormatExtensible.Channels = this.format.Channels;
						waveFormatExtensible.SamplesPerSecond = this.format.SamplesPerSecond;
						waveFormatExtensible.AverageBytesPerSecond = this.format.AverageBytesPerSecond;
						waveFormatExtensible.BlockAlignment = this.format.BlockAlignment;
						waveFormatExtensible.BitsPerSample = this.format.BitsPerSample;
						this.dataStream.Read(array, 0, 2);
						num = (int)BitConverter.ToInt16(array, 0);
						if (num != 22)
						{
							throw new InvalidDataException("Invalid Format.");
						}
						this.dataStream.Read(array, 0, 2);
						waveFormatExtensible.ValidBitsPerSample = BitConverter.ToInt16(array, 0);
						this.dataStream.Read(array, 0, 4);
						waveFormatExtensible.ChannelMask = (Speakers)BitConverter.ToUInt32(array, 0);
						byte[] array2 = new byte[16];
						this.dataStream.Read(array2, 0, 16);
						waveFormatExtensible.SubFormat = new Guid(array2);
						waveFormatExtensible.FormatTag = WaveFormatTag.Extensible;
						this.format = waveFormatExtensible;
					}
				}
				IL_0418:
				num2 = (long)this.dataStream.Read(array, 0, 4);
				continue;
				IL_0407:
				this.dataStream.Seek((long)num, SeekOrigin.Current);
				goto IL_0418;
			}
			this.dataStream.Seek(this.dataOffset, SeekOrigin.Begin);
		}

		public int[] DecodedPacketCumulativeBytes
		{
			get
			{
				return this.decodedPacketCumulativeBytes;
			}
		}

		public WaveFormat Format
		{
			get
			{
				return this.format;
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.dataStream.Read(buffer, offset, count);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("xWMA Stream objects cannot be written to.");
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException("xWMA Stream objects cannot be resized.");
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			long num = 0L;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = this.dataOffset + offset;
				break;
			case SeekOrigin.Current:
				num = offset;
				break;
			case SeekOrigin.End:
				num = this.dataOffset;
				break;
			}
			return this.dataStream.Seek(num, origin) - this.dataOffset;
		}

		public override void Flush()
		{
			throw new NotSupportedException("xWMA Stream objects cannot be flushed.");
		}

		public override int ReadByte()
		{
			int num;
			if (this.dataStream.Position > this.dataOffset + (long)this.dataSize)
			{
				num = -1;
			}
			else
			{
				num = this.dataStream.ReadByte();
			}
			return num;
		}

		public override void WriteByte(byte value)
		{
			throw new NotSupportedException("xWMA Stream objects cannot be written to.");
		}

		public override void Close()
		{
			this.dataStream.Close();
		}

		protected override void Dispose(bool disposing)
		{
			this.dataStream.Dispose();
		}

		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		public override long Position
		{
			get
			{
				return this.dataStream.Position - this.dataOffset;
			}
			set
			{
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		public override long Length
		{
			get
			{
				return (long)this.dataSize;
			}
		}

		public const ushort WAVE_FORMAT_xWMA = 353;

		public const ushort WAVE_FORMAT_EXTENSIBLE = 65534;

		protected Stream dataStream;

		protected long dataOffset;

		protected int dataSize;

		protected int[] decodedPacketCumulativeBytes;

		protected WaveFormat format;
	}
}
