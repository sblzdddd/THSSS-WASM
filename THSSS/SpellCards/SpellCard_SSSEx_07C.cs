using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000051 RID: 81
	internal class SpellCard_SSSEx_07C : BaseSpellCard
	{
		// Token: 0x0600029A RID: 666 RVA: 0x0001139C File Offset: 0x0000F59C
		public SpellCard_SSSEx_07C(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "「雾隐流剑技 ～ 落雪凝霜」";
			base.BaseScore = 40000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRika_ct2");
			new BackgroundBossEx(StageData);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00011438 File Offset: 0x0000F638
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[13];
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\7符2.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
