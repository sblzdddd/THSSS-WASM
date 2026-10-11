using System;
using SlimDX;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x02000199 RID: 409
	public class ScreenTexture
	{
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x0005C77C File Offset: 0x0005A97C
		// (set) Token: 0x060009AD RID: 2477 RVA: 0x0005C793 File Offset: 0x0005A993
		public Texture renderTexture { get; private set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x0005C79C File Offset: 0x0005A99C
		// (set) Token: 0x060009AF RID: 2479 RVA: 0x0005C7B3 File Offset: 0x0005A9B3
		private Surface oldRenderSurface { get; set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0005C7BC File Offset: 0x0005A9BC
		// (set) Token: 0x060009B1 RID: 2481 RVA: 0x0005C7D3 File Offset: 0x0005A9D3
		private Surface oldDepthStencilSurface { get; set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x0005C7DC File Offset: 0x0005A9DC
		// (set) Token: 0x060009B3 RID: 2483 RVA: 0x0005C7F3 File Offset: 0x0005A9F3
		private Surface renderSurface { get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x0005C7FC File Offset: 0x0005A9FC
		// (set) Token: 0x060009B5 RID: 2485 RVA: 0x0005C813 File Offset: 0x0005AA13
		private Surface renderDepthStencilSurface { get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0005C81C File Offset: 0x0005AA1C
		// (set) Token: 0x060009B7 RID: 2487 RVA: 0x0005C833 File Offset: 0x0005AA33
		private Device DeviceMain { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0005C83C File Offset: 0x0005AA3C
		// (set) Token: 0x060009B9 RID: 2489 RVA: 0x0005C853 File Offset: 0x0005AA53
		private int WindowWidth { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x0005C85C File Offset: 0x0005AA5C
		// (set) Token: 0x060009BB RID: 2491 RVA: 0x0005C873 File Offset: 0x0005AA73
		private int WindowHeight { get; set; }

		// Token: 0x060009BC RID: 2492 RVA: 0x0005C87C File Offset: 0x0005AA7C
		public ScreenTexture(Device DeviceMain, int WindowWidth, int WindowHeight)
		{
			this.DeviceMain = DeviceMain;
			this.WindowWidth = WindowWidth;
			this.WindowHeight = WindowHeight;
			while (!this.SetupRenderTexture())
			{
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0005C8BC File Offset: 0x0005AABC
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

		// Token: 0x060009BE RID: 2494 RVA: 0x0005C940 File Offset: 0x0005AB40
		public void Begin()
		{
			this.oldRenderSurface = this.DeviceMain.GetRenderTarget(0);
			this.oldDepthStencilSurface = this.DeviceMain.DepthStencilSurface;
			this.DeviceMain.SetRenderTarget(0, this.renderSurface);
			this.DeviceMain.DepthStencilSurface = this.renderDepthStencilSurface;
			this.DeviceMain.Clear(ClearFlags.ZBuffer | ClearFlags.Target, new Color4(0f, 0f, 0f, 0f), 1f, 0);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0005C9C4 File Offset: 0x0005ABC4
		public void End()
		{
			this.DeviceMain.SetRenderTarget(0, this.oldRenderSurface);
			this.DeviceMain.DepthStencilSurface = this.oldDepthStencilSurface;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0005C9EC File Offset: 0x0005ABEC
		public void Reset()
		{
			this.SetupRenderTexture();
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0005C9F8 File Offset: 0x0005ABF8
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
