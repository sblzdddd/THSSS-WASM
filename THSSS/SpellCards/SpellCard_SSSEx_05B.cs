using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200002D RID: 45
	internal class SpellCard_SSSEx_05B : BaseSpellCard
	{
		// Token: 0x060001EE RID: 494 RVA: 0x0000B734 File Offset: 0x00009934
		public SpellCard_SSSEx_05B(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "花咲「百花缭乱」";
			base.BaseScore = 40000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRika_ct1");
			new BackgroundBossEx(StageData);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000B7D0 File Offset: 0x000099D0
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[9];
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\5符1.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
