using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001AE RID: 430
	internal class SpellCard_SSSEx_02C : BaseSpellCard
	{
		// Token: 0x06000A30 RID: 2608 RVA: 0x00060BD0 File Offset: 0x0005EDD0
		public SpellCard_SSSEx_02C(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "仙器「太乙冰凌」";
			base.BaseScore = 40000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRika_ct2");
			new BackgroundBossEx(StageData);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00060C6C File Offset: 0x0005EE6C
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[3];
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\2符2.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
