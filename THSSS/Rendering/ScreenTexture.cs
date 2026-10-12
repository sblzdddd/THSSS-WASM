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
			while (!SetupRenderTexture())
			{
			}
		}

		private bool SetupRenderTexture()
		{
			bool flag;
			try
			{
				renderTexture = new Texture(DeviceMain, WindowWidth, WindowHeight, 1, Usage.RenderTarget, Format.X8R8G8B8, Pool.Default);
				renderSurface = renderTexture.GetSurfaceLevel(0);
				renderDepthStencilSurface = Surface.CreateDepthStencil(DeviceMain, WindowWidth, WindowHeight, Format.D16, MultisampleType.None, 0, true);
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
			oldRenderSurface = DeviceMain.GetRenderTarget(0);
			oldDepthStencilSurface = DeviceMain.DepthStencilSurface;
			DeviceMain.SetRenderTarget(0, renderSurface);
			DeviceMain.DepthStencilSurface = renderDepthStencilSurface;
			DeviceMain.Clear(ClearFlags.ZBuffer | ClearFlags.Target, new Color4(0f, 0f, 0f, 0f), 1f, 0);
		}

		public void End()
		{
			DeviceMain.SetRenderTarget(0, oldRenderSurface);
			DeviceMain.DepthStencilSurface = oldDepthStencilSurface;
		}

		public void Reset()
		{
			SetupRenderTexture();
		}

		public void Dispose()
		{
			if (renderSurface != null)
			{
				renderSurface.Dispose();
			}
			if (renderDepthStencilSurface != null)
			{
				renderDepthStencilSurface.Dispose();
			}
			if (renderTexture != null)
			{
				renderTexture.Dispose();
			}
			if (oldDepthStencilSurface != null)
			{
				oldDepthStencilSurface.Dispose();
			}
			if (oldRenderSurface != null)
			{
				oldRenderSurface.Dispose();
			}
		}
	}
}
