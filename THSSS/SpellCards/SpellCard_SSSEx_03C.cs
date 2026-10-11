using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001D0 RID: 464
	internal class SpellCard_SSSEx_03C : BaseSpellCard
	{
		// Token: 0x06000AA3 RID: 2723 RVA: 0x000676DC File Offset: 0x000658DC
		public SpellCard_SSSEx_03C(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "霜冻「寒霜封印」";
			base.BaseScore = 40000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRika_ct2");
			new BackgroundBossEx(StageData);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00067778 File Offset: 0x00065978
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[5];
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\3符2.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
