using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000204 RID: 516
	internal class SpellCard_SSSEx_05C : BaseSpellCard
	{
		// Token: 0x06000B8D RID: 2957 RVA: 0x00072700 File Offset: 0x00070900
		public SpellCard_SSSEx_05C(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "六花「冰结界的雪花阵」";
			base.BaseScore = 40000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRika_ct2");
			new BackgroundBossEx(StageData);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0007279C File Offset: 0x0007099C
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[9];
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\5符2.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
