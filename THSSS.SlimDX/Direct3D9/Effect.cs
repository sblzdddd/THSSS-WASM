namespace SlimDX.Direct3D9
{
	public class Effect : ComObject
	{
		public string Technique { get; set; }

		public static Effect FromFile(Device device, string fileName, ShaderFlags flags)
		{
			return new Effect();
		}

		public int Begin()
		{
			return 1;
		}

		public void BeginPass(int pass)
		{
		}

		public void EndPass()
		{
		}

		public void End()
		{
		}

		public void SetValue<T>(string name, T value) where T : struct
		{
		}

		public void OnLostDevice()
		{
		}

		public void OnResetDevice()
		{
		}
	}
}
