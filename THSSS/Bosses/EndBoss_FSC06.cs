using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001CB RID: 459
	internal class EndBoss_FSC06 : EndBoss_Touhou_Small
	{
		// Token: 0x06000A92 RID: 2706 RVA: 0x00066750 File Offset: 0x00064950
		public EndBoss_FSC06(StageDataPackage StageData, PointF OriginalPosition, Color Color1, Color Color2)
			: base(StageData, OriginalPosition, Color1, Color2)
		{
			this.tempVolumn = base.BGM_Player.Volume;
			base.LifeTime = 300;
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00066780 File Offset: 0x00064980
		public override void Ctrl()
		{
			base.Ctrl();
			int num = base.LifeTime - 90;
			base.BGM_Player.Volume = (int)((float)this.tempVolumn * (1f - (float)base.Time / (float)num));
			PointF pointF = new PointF((float)(base.BoundRect.Width / 2), 150f);
			if (base.Time == base.LifeTime - 60 || base.Time == base.LifeTime - 40 || base.Time == base.LifeTime - 20)
			{
				new EmitterSaveEnegy3D(base.StageData, pointF, Color.SkyBlue);
				base.StageData.SoundPlay("se_ch02.wav");
			}
			if (base.Time == base.LifeTime)
			{
				base.StageData.ChangeBGM(".\\BGM\\Boss06FSC.wav", 0, 0, 255, 0, 0);
				new Boss_Tensei02(base.StageData);
				base.Background3D.WarpEnabled = true;
			}
		}

		// Token: 0x040003E9 RID: 1001
		private int tempVolumn;
	}
}
