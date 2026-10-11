using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200002C RID: 44
	internal class SpellCard_SSSEx_06C : BaseSpellCard
	{
		// Token: 0x060001EC RID: 492 RVA: 0x0000B638 File Offset: 0x00009838
		public SpellCard_SSSEx_06C(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "雪幕「冰雪流凌」";
			base.BaseScore = 40000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 128);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRika_ct2");
			new BackgroundBossEx(StageData);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000B6D8 File Offset: 0x000098D8
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[11];
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\6符2.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
