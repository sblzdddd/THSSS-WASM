using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200007F RID: 127
	internal class SpellCard_SSSEx_05A : BaseSpellCard
	{
		// Token: 0x06000437 RID: 1079 RVA: 0x0001EC2C File Offset: 0x0001CE2C
		public SpellCard_SSSEx_05A(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "飓风「台风眼」";
			base.BaseScore = 40000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRika_ct0");
			new BackgroundBossEx(StageData);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0001ECC8 File Offset: 0x0001CEC8
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[9];
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\5符0.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
