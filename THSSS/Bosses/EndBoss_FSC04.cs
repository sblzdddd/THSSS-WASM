using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001FE RID: 510
	internal class EndBoss_FSC04 : EndBoss_Touhou_Small
	{
		// Token: 0x06000B7B RID: 2939 RVA: 0x000721C7 File Offset: 0x000703C7
		public EndBoss_FSC04(StageDataPackage StageData, PointF OriginalPosition, Color Color1, Color Color2)
			: base(StageData, OriginalPosition, Color1, Color2)
		{
			base.LifeTime = 150;
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x000721E4 File Offset: 0x000703E4
		public override void Ctrl()
		{
			base.Ctrl();
			PointF pointF = new PointF((float)(base.BoundRect.Width / 2), 150f);
			if (base.Time == base.LifeTime - 60 || base.Time == base.LifeTime - 40 || base.Time == base.LifeTime - 20)
			{
				new EmitterSaveEnegy3D(base.StageData, pointF, Color.Purple);
				base.StageData.SoundPlay("se_ch02.wav");
			}
			if (base.Time == base.LifeTime)
			{
				new Boss_Kage02(base.StageData);
				base.Background3D.WarpEnabled = true;
			}
		}
	}
}
