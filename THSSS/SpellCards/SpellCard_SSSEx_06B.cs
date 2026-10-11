using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000052 RID: 82
	internal class SpellCard_SSSEx_06B : BaseSpellCard
	{
		// Token: 0x0600029C RID: 668 RVA: 0x00011494 File Offset: 0x0000F694
		public SpellCard_SSSEx_06B(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "花符「云上的花田」";
			base.BaseScore = 40000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRika_ct1");
			new BackgroundBossEx(StageData);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00011530 File Offset: 0x0000F730
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[11];
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\6符1.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
