using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000177 RID: 375
	public class Background2DRemover : BaseEffect
	{
		// Token: 0x06000920 RID: 2336 RVA: 0x00057DB7 File Offset: 0x00055FB7
		public Background2DRemover(StageDataPackage StageData)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			base.LifeTime = 20;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00057E08 File Offset: 0x00056008
		public override void Ctrl()
		{
			base.Ctrl();
			base.Background.BackgroundList.ForEach(delegate(BaseObject x)
			{
				x.TransparentValueF -= (float)(base.MaxTransparent / base.LifeTime);
			});
			if (base.Time >= base.LifeTime)
			{
				base.Background.BackgroundList.Clear();
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00057E5F File Offset: 0x0005605F
		public override void Show()
		{
		}
	}
}
