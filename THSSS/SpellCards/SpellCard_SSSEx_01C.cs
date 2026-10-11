using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000196 RID: 406
	internal class SpellCard_SSSEx_01C : BaseSpellCard
	{
		// Token: 0x060009A7 RID: 2471 RVA: 0x0005C1F8 File Offset: 0x0005A3F8
		public SpellCard_SSSEx_01C(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "冬临「初雪之寒径」";
			base.BaseScore = 40000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 224);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRika_ct2");
			new BackgroundBossEx(StageData);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0005C298 File Offset: 0x0005A498
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[1];
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\1符2.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
