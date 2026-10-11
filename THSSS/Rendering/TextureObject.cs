using System;
using System.Drawing;
using SlimDX.Direct3D9;

namespace Shooting
{
	// Token: 0x020001DA RID: 474
	public class TextureObject
	{
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0006B9CC File Offset: 0x00069BCC
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x0006B9E3 File Offset: 0x00069BE3
		public Texture TXTure { get; set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0006B9EC File Offset: 0x00069BEC
		public int Width
		{
			get
			{
				return this.PosRect.Width;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0006BA0C File Offset: 0x00069C0C
		public int Height
		{
			get
			{
				return this.PosRect.Height;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0006BA2C File Offset: 0x00069C2C
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x0006BA43 File Offset: 0x00069C43
		public int SrcWidth { get; set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x0006BA4C File Offset: 0x00069C4C
		// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x0006BA63 File Offset: 0x00069C63
		public int SrcHeight { get; set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x0006BA6C File Offset: 0x00069C6C
		// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x0006BA83 File Offset: 0x00069C83
		public Rectangle PosRect { get; set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0006BA8C File Offset: 0x00069C8C
		// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x0006BAA3 File Offset: 0x00069CA3
		public int OffsetX { get; set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x0006BAAC File Offset: 0x00069CAC
		// (set) Token: 0x06000AE7 RID: 2791 RVA: 0x0006BAC3 File Offset: 0x00069CC3
		public int OffsetY { get; set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x0006BACC File Offset: 0x00069CCC
		public PointF RotatingCenter
		{
			get
			{
				return new PointF((float)(this.PosRect.Width / 2 + this.OffsetX), (float)(this.PosRect.Height / 2 + this.OffsetY));
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0006BB14 File Offset: 0x00069D14
		public PointF LeftTop
		{
			get
			{
				return new PointF(0f, 0f);
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x0006BB38 File Offset: 0x00069D38
		public PointF RightTop
		{
			get
			{
				return new PointF((float)this.Width, 0f);
			}
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0006BB68 File Offset: 0x00069D68
		public void Dispose()
		{
			if (!this.TXTure.Disposed)
			{
				this.TXTure.Dispose();
			}
		}
	}
}
