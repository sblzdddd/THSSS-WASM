using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000049 RID: 73
	internal class EndBoss_FSC02 : EndBoss_Touhou_Small
	{
		// Token: 0x06000279 RID: 633 RVA: 0x00010199 File Offset: 0x0000E399
		public EndBoss_FSC02(StageDataPackage StageData, PointF OriginalPosition, Color Color1, Color Color2)
			: base(StageData, OriginalPosition, Color1, Color2)
		{
			base.LifeTime = 150;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000101B8 File Offset: 0x0000E3B8
		public override void Ctrl()
		{
			base.Ctrl();
			PointF pointF = new PointF((float)(base.BoundRect.Width / 2), 150f);
			if (base.Time == base.LifeTime - 60 || base.Time == base.LifeTime - 40 || base.Time == base.LifeTime - 20)
			{
				new EmitterSaveEnegy3D(base.StageData, pointF, Color.Green);
				base.StageData.SoundPlay("se_ch02.wav");
			}
			if (base.Time == base.LifeTime)
			{
				new Boss_Rakuki03(base.StageData);
				base.Background3D.WarpEnabled = true;
			}
		}
	}
}
