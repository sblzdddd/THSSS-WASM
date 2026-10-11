using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001EA RID: 490
	internal class EndBoss_FSC05 : EndBoss_Touhou_Small
	{
		// Token: 0x06000B34 RID: 2868 RVA: 0x0006EA6C File Offset: 0x0006CC6C
		public EndBoss_FSC05(StageDataPackage StageData, PointF OriginalPosition, Color Color1, Color Color2)
			: base(StageData, OriginalPosition, Color1, Color2)
		{
			base.LifeTime = 150;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0006EA88 File Offset: 0x0006CC88
		public override void Ctrl()
		{
			base.Ctrl();
			PointF pointF = new PointF((float)(base.BoundRect.Width / 2), 150f);
			if (base.Time == base.LifeTime - 60 || base.Time == base.LifeTime - 40 || base.Time == base.LifeTime - 20)
			{
				new EmitterSaveEnegy3D(base.StageData, pointF, Color.Red);
				base.StageData.SoundPlay("se_ch02.wav");
			}
			if (base.Time == base.LifeTime)
			{
				new Boss_Rakukun02(base.StageData);
				base.Background3D.WarpEnabled = true;
			}
		}
	}
}
