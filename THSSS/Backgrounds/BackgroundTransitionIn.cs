using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001B9 RID: 441
	internal class BackgroundTransitionIn : BaseObject
	{
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x00063088 File Offset: 0x00061288
		// (set) Token: 0x06000A52 RID: 2642 RVA: 0x0006309F File Offset: 0x0006129F
		public int Delay { get; set; }

		// Token: 0x06000A53 RID: 2643 RVA: 0x000630A8 File Offset: 0x000612A8
		public BackgroundTransitionIn(StageDataPackage StageData)
			: base(StageData, "bullet21_7", new PointF(0f, 0f), 0f, 0.0)
		{
			base.Background.BackgroundList.Add(this);
			base.Position = new PointF((float)(StageData.BoundRect.X + StageData.BoundRect.Width / 2), (float)(StageData.BoundRect.Y + StageData.BoundRect.Height / 2));
			base.Scale = 30f;
			base.LifeTime = 480;
			this.Delay = 400;
			base.TransparentValueF = 255f;
			base.ColorValue = Color.Black;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00063170 File Offset: 0x00061370
		public override void Ctrl()
		{
			base.Time++;
			if (base.Time > this.Delay)
			{
				base.TransparentValueF -= (float)(255 / (base.LifeTime - this.Delay) + 1);
			}
		}
	}
}
