using System;
using System.IO;
using System.Text;
using SlimDX.Multimedia;

namespace Shooting
{
	// Token: 0x020000AB RID: 171
	internal class xWMAStream : Stream
	{
		// Token: 0x060004FB RID: 1275 RVA: 0x00028416 File Offset: 0x00026616
		public xWMAStream(Stream stream)
		{
			this.dataStream = stream;
			this.analyzeXWMAFileStream();
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00028430 File Offset: 0x00026630
		public xWMAStream(string path)
		{
			if (!File.Exists(path))
			{
				throw new FileNotFoundException(path + " Not Found");
			}
			this.dataStream = new FileStream(path, FileMode.Open);
			this.analyzeXWMAFileStream();
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00028478 File Offset: 0x00026678
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

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x000288D0 File Offset: 0x00026AD0
		public int[] DecodedPacketCumulativeBytes
		{
			get
			{
				return this.decodedPacketCumulativeBytes;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x000288E8 File Offset: 0x00026AE8
		public WaveFormat Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00028900 File Offset: 0x00026B00
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.dataStream.Read(buffer, offset, count);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00028920 File Offset: 0x00026B20
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("xWMA Stream objects cannot be written to.");
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0002892D File Offset: 0x00026B2D
		public override void SetLength(long value)
		{
			throw new NotSupportedException("xWMA Stream objects cannot be resized.");
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0002893C File Offset: 0x00026B3C
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

		// Token: 0x06000504 RID: 1284 RVA: 0x00028993 File Offset: 0x00026B93
		public override void Flush()
		{
			throw new NotSupportedException("xWMA Stream objects cannot be flushed.");
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x000289A0 File Offset: 0x00026BA0
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

		// Token: 0x06000506 RID: 1286 RVA: 0x000289E3 File Offset: 0x00026BE3
		public override void WriteByte(byte value)
		{
			throw new NotSupportedException("xWMA Stream objects cannot be written to.");
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x000289F0 File Offset: 0x00026BF0
		public override void Close()
		{
			this.dataStream.Close();
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x000289FF File Offset: 0x00026BFF
		protected override void Dispose(bool disposing)
		{
			this.dataStream.Dispose();
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x00028A10 File Offset: 0x00026C10
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x00028A24 File Offset: 0x00026C24
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x00028A38 File Offset: 0x00026C38
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x00028A4C File Offset: 0x00026C4C
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x00028A70 File Offset: 0x00026C70
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

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x00028A7C File Offset: 0x00026C7C
		public override long Length
		{
			get
			{
				return (long)this.dataSize;
			}
		}

		// Token: 0x0400019D RID: 413
		public const ushort WAVE_FORMAT_xWMA = 353;

		// Token: 0x0400019E RID: 414
		public const ushort WAVE_FORMAT_EXTENSIBLE = 65534;

		// Token: 0x0400019F RID: 415
		protected Stream dataStream;

		// Token: 0x040001A0 RID: 416
		protected long dataOffset;

		// Token: 0x040001A1 RID: 417
		protected int dataSize;

		// Token: 0x040001A2 RID: 418
		protected int[] decodedPacketCumulativeBytes;

		// Token: 0x040001A3 RID: 419
		protected WaveFormat format;
	}
}
