using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001F2 RID: 498
	internal class SpellCard_SSSEx_01A : BaseSpellCard
	{
		// Token: 0x06000B52 RID: 2898 RVA: 0x0006FCB8 File Offset: 0x0006DEB8
		public SpellCard_SSSEx_01A(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "风符「环形气流」";
			base.BaseScore = 40000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRika_ct0");
			new BackgroundBossEx(StageData);
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0006FD54 File Offset: 0x0006DF54
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[1];
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\1符0.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
