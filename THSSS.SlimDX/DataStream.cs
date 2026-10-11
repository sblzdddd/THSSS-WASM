using System;
using System.Runtime.InteropServices;

namespace SlimDX
{
	public class DataStream : IDisposable
	{
		private byte[] buffer;

		private int position;

		public DataStream(byte[] buffer)
		{
			this.buffer = buffer ?? new byte[0];
		}

		public byte[] Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		public void WriteRange<T>(T[] data) where T : struct
		{
			if (data == null || data.Length == 0)
			{
				return;
			}
			int num = Marshal.SizeOf<T>();
			int num2 = num * data.Length;
			this.Ensure(this.position + num2);
			GCHandle gchandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			try
			{
				Marshal.Copy(gchandle.AddrOfPinnedObject(), this.buffer, this.position, num2);
				this.position += num2;
			}
			finally
			{
				gchandle.Free();
			}
		}

		public void WriteRange(IntPtr source, long count)
		{
			int num = (int)count;
			this.Ensure(this.position + num);
			Marshal.Copy(source, this.buffer, this.position, num);
			this.position += num;
		}

		private void Ensure(int size)
		{
			if (this.buffer.Length < size)
			{
				byte[] array = new byte[size];
				System.Buffer.BlockCopy(this.buffer, 0, array, 0, this.buffer.Length);
				this.buffer = array;
			}
		}

		public void Dispose()
		{
		}
	}
}
