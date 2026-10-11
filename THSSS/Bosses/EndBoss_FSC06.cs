using System;
using System.Drawing;

namespace Shooting
{
	internal class EndBoss_FSC06 : EndBoss_Touhou_Small
	{
		public EndBoss_FSC06(StageDataPackage StageData, PointF OriginalPosition, Color Color1, Color Color2)
			: base(StageData, OriginalPosition, Color1, Color2)
		{
			this.tempVolumn = base.BGM_Player.Volume;
			base.LifeTime = 300;
		}

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
				base.StageData.ChangeBGM(".\\BGM\\Boss06FSC.ogg", 0, 0, 255, 0, 0);
				new Boss_Tensei02(base.StageData);
				base.Background3D.WarpEnabled = true;
			}
		}

		private int tempVolumn;
	}
}
