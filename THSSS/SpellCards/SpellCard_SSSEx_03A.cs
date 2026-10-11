using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000034 RID: 52
	internal class SpellCard_SSSEx_03A : BaseSpellCard
	{
		// Token: 0x06000226 RID: 550 RVA: 0x0000D790 File Offset: 0x0000B990
		public SpellCard_SSSEx_03A(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "风切「狂风交错」";
			base.BaseScore = 40000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 160);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRika_ct0");
			new BackgroundBossEx(StageData);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000D830 File Offset: 0x0000BA30
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[5];
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\3符0.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
