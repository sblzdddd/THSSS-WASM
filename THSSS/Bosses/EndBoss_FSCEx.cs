using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000048 RID: 72
	internal class EndBoss_FSCEx : EndBoss_Touhou_Small
	{
		// Token: 0x06000277 RID: 631 RVA: 0x000100B5 File Offset: 0x0000E2B5
		public EndBoss_FSCEx(StageDataPackage StageData, PointF OriginalPosition, Color Color1, Color Color2)
			: base(StageData, OriginalPosition, Color1, Color2)
		{
			base.LifeTime = 150;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000100D4 File Offset: 0x0000E2D4
		public override void Ctrl()
		{
			base.Ctrl();
			PointF pointF = new PointF((float)(base.BoundRect.Width / 2), 150f);
			if (base.Time == base.LifeTime - 60 || base.Time == base.LifeTime - 40 || base.Time == base.LifeTime - 20)
			{
				new EmitterSaveEnegy3D(base.StageData, pointF, Color.SkyBlue);
				base.StageData.SoundPlay("se_ch02.wav");
			}
			if (base.Time == base.LifeTime)
			{
				new Boss_Rika02(base.StageData);
				base.Background3D.WarpEnabled = true;
			}
		}
	}
}
