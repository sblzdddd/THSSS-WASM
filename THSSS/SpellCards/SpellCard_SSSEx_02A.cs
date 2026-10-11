using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000209 RID: 521
	internal class SpellCard_SSSEx_02A : BaseSpellCard
	{
		// Token: 0x06000B99 RID: 2969 RVA: 0x00073434 File Offset: 0x00071634
		public SpellCard_SSSEx_02A(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "速风「双龙卷」";
			base.BaseScore = 40000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRika_ct0");
			new BackgroundBossEx(StageData);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x000734D0 File Offset: 0x000716D0
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[3];
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\2符0.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
