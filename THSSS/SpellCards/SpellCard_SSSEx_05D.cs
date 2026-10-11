using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001CF RID: 463
	internal class SpellCard_SSSEx_05D : BaseSpellCard
	{
		// Token: 0x06000AA1 RID: 2721 RVA: 0x000675E4 File Offset: 0x000657E4
		public SpellCard_SSSEx_05D(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "净符「月光世界」";
			base.BaseScore = 40000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRika_ct3");
			new BackgroundBossEx(StageData);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00067680 File Offset: 0x00065880
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[9];
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\5符3.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
