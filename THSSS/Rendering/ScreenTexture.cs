using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	public class ScreenTexture
	{
		public Texture renderTexture { get; private set; }

		private Surface oldRenderSurface { get; set; }

		private Surface oldDepthStencilSurface { get; set; }

		private Surface renderSurface { get; set; }

		private Surface renderDepthStencilSurface { get; set; }

		private Device DeviceMain { get; set; }

		private int WindowWidth { get; set; }

		private int WindowHeight { get; set; }

		public ScreenTexture(Device DeviceMain, int WindowWidth, int WindowHeight)
		{
			this.DeviceMain = DeviceMain;
			this.WindowWidth = WindowWidth;
			this.WindowHeight = WindowHeight;
			while (!this.SetupRenderTexture())
			{
			}
		}

		private bool SetupRenderTexture()
		{
			bool flag;
			try
			{
				this.renderTexture = new Texture(this.DeviceMain, this.WindowWidth, this.WindowHeight, 1, Usage.RenderTarget, Format.X8R8G8B8, Pool.Default);
				this.renderSurface = this.renderTexture.GetSurfaceLevel(0);
				this.renderDepthStencilSurface = Surface.CreateDepthStencil(this.DeviceMain, this.WindowWidth, this.WindowHeight, Format.D16, MultisampleType.None, 0, true);
				flag = true;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		public void Begin()
		{
			this.oldRenderSurface = this.DeviceMain.GetRenderTarget(0);
			this.oldDepthStencilSurface = this.DeviceMain.DepthStencilSurface;
			this.DeviceMain.SetRenderTarget(0, this.renderSurface);
			this.DeviceMain.DepthStencilSurface = this.renderDepthStencilSurface;
			this.DeviceMain.Clear(ClearFlags.ZBuffer | ClearFlags.Target, new Color4(0f, 0f, 0f, 0f), 1f, 0);
		}

		public void End()
		{
			this.DeviceMain.SetRenderTarget(0, this.oldRenderSurface);
			this.DeviceMain.DepthStencilSurface = this.oldDepthStencilSurface;
		}

		public void Reset()
		{
			this.SetupRenderTexture();
		}

		public void Dispose()
		{
			if (this.renderSurface != null)
			{
				this.renderSurface.Dispose();
			}
			if (this.renderDepthStencilSurface != null)
			{
				this.renderDepthStencilSurface.Dispose();
			}
			if (this.renderTexture != null)
			{
				this.renderTexture.Dispose();
			}
			if (this.oldDepthStencilSurface != null)
			{
				this.oldDepthStencilSurface.Dispose();
			}
			if (this.oldRenderSurface != null)
			{
				this.oldRenderSurface.Dispose();
			}
		}
	}
}
