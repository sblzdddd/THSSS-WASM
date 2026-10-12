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
			dataStream = stream;
			analyzeXWMAFileStream();
		}

		public xWMAStream(string path)
		{
			if (!File.Exists(path))
			{
				throw new FileNotFoundException(path + " Not Found");
			}
			dataStream = new FileStream(path, FileMode.Open);
			analyzeXWMAFileStream();
		}

		private static void ReadExact(Stream stream, byte[] buffer, int offset, int count)
		{
#if NET
			stream.ReadExactly(buffer, offset, count);
#else
			while (count > 0)
			{
				var read = stream.Read(buffer, offset, count);
				if (read == 0)
				{
					throw new EndOfStreamException();
				}
				offset += read;
				count -= read;
			}
#endif
		}

		private void analyzeXWMAFileStream()
		{
			var array = new byte[4];
			dataStream.Seek(0L, SeekOrigin.Begin);
			ReadExact(dataStream, array, 0, 4);
			if (Encoding.ASCII.GetString(array) != "RIFF")
			{
				throw new InvalidDataException("Invalid xWMA file.");
			}
			ReadExact(dataStream, array, 0, 4);
			var num = BitConverter.ToInt32(array, 0);
			if (dataStream.Length != (long)(num + 8))
			{
				throw new InvalidDataException("Invalid Format");
			}
			ReadExact(dataStream, array, 0, 4);
			if (Encoding.ASCII.GetString(array) != "XWMA")
			{
				throw new InvalidDataException("Invalid xWMA file.");
			}
			var num2 = (long)dataStream.Read(array, 0, 4);
			while (num2 > 3L)
			{
				var @string = Encoding.ASCII.GetString(array);
				ReadExact(dataStream, array, 0, 4);
				num = BitConverter.ToInt32(array, 0);
				var text = @string;
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
						dataOffset = dataStream.Position;
						dataSize = num;
						dataStream.Seek((long)num, SeekOrigin.Current);
					}
					else
					{
						if (num % 4 != 0)
						{
							throw new InvalidDataException("Invalid Format.");
						}
						var num3 = num / 4;
						decodedPacketCumulativeBytes = new int[num3];
						for (var i = 0; i < num3; i++)
						{
							ReadExact(dataStream, array, 0, 4);
							decodedPacketCumulativeBytes[i] = BitConverter.ToInt32(array, 0);
						}
					}
				}
				else
				{
					ReadExact(dataStream, array, 0, 2);
					var num4 = BitConverter.ToUInt16(array, 0);
					format = new WaveFormat();
					ReadExact(dataStream, array, 0, 2);
					format.Channels = (short)BitConverter.ToUInt16(array, 0);
					ReadExact(dataStream, array, 0, 4);
					format.SamplesPerSecond = BitConverter.ToInt32(array, 0);
					ReadExact(dataStream, array, 0, 4);
					format.AverageBytesPerSecond = BitConverter.ToInt32(array, 0);
					ReadExact(dataStream, array, 0, 2);
					format.BlockAlignment = BitConverter.ToInt16(array, 0);
					ReadExact(dataStream, array, 0, 2);
					format.BitsPerSample = BitConverter.ToInt16(array, 0);
					if (num4 == 353)
					{
						dataStream.Seek((long)(num - 16), SeekOrigin.Current);
						format.FormatTag = WaveFormatTag.WMAudio2;
					}
					else
					{
						if (num4 != 65534)
						{
							throw new InvalidDataException("Invalid xWMA file.");
						}
						var waveFormatExtensible = new WaveFormatExtensible();
						waveFormatExtensible.Channels = format.Channels;
						waveFormatExtensible.SamplesPerSecond = format.SamplesPerSecond;
						waveFormatExtensible.AverageBytesPerSecond = format.AverageBytesPerSecond;
						waveFormatExtensible.BlockAlignment = format.BlockAlignment;
						waveFormatExtensible.BitsPerSample = format.BitsPerSample;
						ReadExact(dataStream, array, 0, 2);
						num = (int)BitConverter.ToInt16(array, 0);
						if (num != 22)
						{
							throw new InvalidDataException("Invalid Format.");
						}
						ReadExact(dataStream, array, 0, 2);
						waveFormatExtensible.ValidBitsPerSample = BitConverter.ToInt16(array, 0);
						ReadExact(dataStream, array, 0, 4);
						waveFormatExtensible.ChannelMask = (Speakers)BitConverter.ToUInt32(array, 0);
						var array2 = new byte[16];
						ReadExact(dataStream, array2, 0, 16);
						waveFormatExtensible.SubFormat = new Guid(array2);
						waveFormatExtensible.FormatTag = WaveFormatTag.Extensible;
						format = waveFormatExtensible;
					}
				}
				IL_0418:
				num2 = (long)dataStream.Read(array, 0, 4);
				continue;
				IL_0407:
				dataStream.Seek((long)num, SeekOrigin.Current);
				goto IL_0418;
			}
			dataStream.Seek(dataOffset, SeekOrigin.Begin);
		}

		public int[] DecodedPacketCumulativeBytes
		{
			get
			{
				return decodedPacketCumulativeBytes;
			}
		}

		public WaveFormat Format
		{
			get
			{
				return format;
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return dataStream.Read(buffer, offset, count);
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
			var num = 0L;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = dataOffset + offset;
				break;
			case SeekOrigin.Current:
				num = offset;
				break;
			case SeekOrigin.End:
				num = dataOffset;
				break;
			}
			return dataStream.Seek(num, origin) - dataOffset;
		}

		public override void Flush()
		{
			throw new NotSupportedException("xWMA Stream objects cannot be flushed.");
		}

		public override int ReadByte()
		{
			int num;
			if (dataStream.Position > dataOffset + (long)dataSize)
			{
				num = -1;
			}
			else
			{
				num = dataStream.ReadByte();
			}
			return num;
		}

		public override void WriteByte(byte value)
		{
			throw new NotSupportedException("xWMA Stream objects cannot be written to.");
		}

		public override void Close()
		{
			dataStream.Close();
		}

		protected override void Dispose(bool disposing)
		{
			dataStream.Dispose();
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
				return dataStream.Position - dataOffset;
			}
			set
			{
				Seek(value, SeekOrigin.Begin);
			}
		}

		public override long Length
		{
			get
			{
				return (long)dataSize;
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
