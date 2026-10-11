using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000096 RID: 150
	internal class SpellCard_SSSEx_06A : BaseSpellCard
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x000238D0 File Offset: 0x00021AD0
		public SpellCard_SSSEx_06A(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "天岚「逆卷神风」";
			base.BaseScore = 40000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 192);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRika_ct0");
			new BackgroundBossEx(StageData);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00023970 File Offset: 0x00021B70
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[11];
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\6符0.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
