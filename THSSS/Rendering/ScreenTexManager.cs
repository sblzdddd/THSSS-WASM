using SlimDX.Direct3D9;

namespace Shooting
{
	public class ScreenTexManager
	{
		public ScreenTexture[] ScreenTex { get; private set; }

		private int ScreenLevel { get; set; }

		public Texture RenderTexture
		{
			get
			{
				return this.ScreenTex[this.ScreenLevel].renderTexture;
			}
		}

		public ScreenTexManager(Device DeviceMain)
		{
			this.ScreenTex = new ScreenTexture[5];
			for (int i = 0; i < this.ScreenTex.Length; i++)
			{
				this.ScreenTex[i] = new ScreenTexture(DeviceMain, 640, 480);
			}
		}

		public void Begin()
		{
			this.ScreenTex[this.ScreenLevel++].Begin();
		}

		public void End()
		{
			this.ScreenTex[--this.ScreenLevel].End();
		}

		public void Reset()
		{
			for (int i = 0; i < this.ScreenTex.Length; i++)
			{
				if (this.ScreenTex[i] != null)
				{
					this.ScreenTex[i].Reset();
				}
			}
		}

		public void Dispose()
		{
			for (int i = 0; i < this.ScreenTex.Length; i++)
			{
				if (this.ScreenTex[i] != null)
				{
					this.ScreenTex[i].Dispose();
				}
			}
		}
	}
}
