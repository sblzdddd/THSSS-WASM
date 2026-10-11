using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000A5 RID: 165
	internal class SpellCard_SSSEx_10 : BaseSpellCard
	{
		// Token: 0x060004E6 RID: 1254 RVA: 0x00026D40 File Offset: 0x00024F40
		public SpellCard_SSSEx_10(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "「未定」";
			base.BaseScore = 40000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 128);
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

		// Token: 0x060004E7 RID: 1255 RVA: 0x00026E60 File Offset: 0x00025060
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[17];
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\10符.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
