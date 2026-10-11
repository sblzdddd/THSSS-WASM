using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000179 RID: 377
	internal class SpellCard_SSSEx_09 : BaseTimeSC
	{
		// Token: 0x06000926 RID: 2342 RVA: 0x00057F5C File Offset: 0x0005615C
		public SpellCard_SSSEx_09(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "仙蝶「无限的彩灵」";
			base.BaseScore = 40000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, -150);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new BackgroundBossEx(StageData);
			string name = base.MyPlane.Name;
			if (name != null)
			{
				if (name == "Reimu")
				{
					new FullPic2(StageData, "FaceRika_ct0");
					return;
				}
				if (name == "Marisa")
				{
					new FullPic2(StageData, "FaceRika_ct1");
					return;
				}
				if (name == "Sanae")
				{
					new FullPic2(StageData, "FaceRika_ct2");
					return;
				}
				if (name == "Koishi")
				{
					new FullPic2(StageData, "FaceRika_ct3");
					return;
				}
			}
			new FullPic2(StageData, "FaceRika_ct0");
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0005807C File Offset: 0x0005627C
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[16];
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\9符.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
