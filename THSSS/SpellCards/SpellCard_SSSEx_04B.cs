using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000205 RID: 517
	internal class SpellCard_SSSEx_04B : BaseSpellCard
	{
		// Token: 0x06000B8F RID: 2959 RVA: 0x000727F8 File Offset: 0x000709F8
		public SpellCard_SSSEx_04B(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "荣符「生长与枯萎的循环」";
			base.BaseScore = 40000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 160);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRika_ct1");
			new BackgroundBossEx(StageData);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x00072898 File Offset: 0x00070A98
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[7];
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\4符1.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
