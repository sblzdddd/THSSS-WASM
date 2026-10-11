using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000072 RID: 114
	internal class EndBoss_FSC01 : EndBoss_Touhou_Small
	{
		// Token: 0x060003FE RID: 1022 RVA: 0x0001B6DC File Offset: 0x000198DC
		public EndBoss_FSC01(StageDataPackage StageData, PointF OriginalPosition, Color Color1, Color Color2)
			: base(StageData, OriginalPosition, Color1, Color2)
		{
			base.LifeTime = 150;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001B6F8 File Offset: 0x000198F8
		public override void Ctrl()
		{
			base.Ctrl();
			PointF pointF = new PointF((float)(base.BoundRect.Width / 2), 150f);
			if (base.Time == base.LifeTime - 60 || base.Time == base.LifeTime - 40 || base.Time == base.LifeTime - 20)
			{
				new EmitterSaveEnegy3D(base.StageData, pointF, Color.Yellow);
				base.StageData.SoundPlay("se_ch02.wav");
			}
			if (base.Time == base.LifeTime)
			{
				new Boss_Ami03(base.StageData);
				base.Background3D.WarpEnabled = true;
			}
		}
	}
}
