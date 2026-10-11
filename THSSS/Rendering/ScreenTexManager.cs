using System;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x02000061 RID: 97
	public class ScreenTexManager
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600033C RID: 828 RVA: 0x000162F0 File Offset: 0x000144F0
		// (set) Token: 0x0600033D RID: 829 RVA: 0x00016307 File Offset: 0x00014507
		public ScreenTexture[] ScreenTex { get; private set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00016310 File Offset: 0x00014510
		// (set) Token: 0x0600033F RID: 831 RVA: 0x00016327 File Offset: 0x00014527
		private int ScreenLevel { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00016330 File Offset: 0x00014530
		public Texture RenderTexture
		{
			get
			{
				return this.ScreenTex[this.ScreenLevel].renderTexture;
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00016354 File Offset: 0x00014554
		public ScreenTexManager(Device DeviceMain)
		{
			this.ScreenTex = new ScreenTexture[5];
			for (int i = 0; i < this.ScreenTex.Length; i++)
			{
				this.ScreenTex[i] = new ScreenTexture(DeviceMain, 640, 480);
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000163A8 File Offset: 0x000145A8
		public void Begin()
		{
			this.ScreenTex[this.ScreenLevel++].Begin();
		}

		// Token: 0x06000343 RID: 835 RVA: 0x000163D8 File Offset: 0x000145D8
		public void End()
		{
			this.ScreenTex[--this.ScreenLevel].End();
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00016408 File Offset: 0x00014608
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

		// Token: 0x06000345 RID: 837 RVA: 0x00016450 File Offset: 0x00014650
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
