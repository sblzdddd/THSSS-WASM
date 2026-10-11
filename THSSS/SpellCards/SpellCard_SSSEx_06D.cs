using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001EF RID: 495
	internal class SpellCard_SSSEx_06D : BaseSpellCard
	{
		// Token: 0x06000B4C RID: 2892 RVA: 0x0006F9CC File Offset: 0x0006DBCC
		public SpellCard_SSSEx_06D(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "皎符「星月炮」";
			base.BaseScore = 40000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRika_ct3");
			new BackgroundBossEx(StageData);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0006FA68 File Offset: 0x0006DC68
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[11];
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\6符3.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
