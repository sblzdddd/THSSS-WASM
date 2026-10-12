using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSSEx_FSC : BaseSpellCard
	{
		public SpellCard_SSSEx_FSC(StageDataPackage StageData)
			: base(StageData)
		{
			SC_Name = "「雾隐之蝶」";
			BaseScore = 40000000L;
			Boss.DestPoint = new Point(BoundRect.Width / 2, 120);
			Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, SC_Name);
			new SpellCardAttack(StageData);
			new BackgroundBossEx(StageData);
			var name = MyPlane.Name;
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

		public override void Shoot()
		{
			Boss.Armon = Boss.ArmonArray[18];
			if (Time == 100)
			{
				var text = ".\\CS\\StEx\\关底Boss\\FSC符.mbg";
				var cs_Data = StageData.LoadCS(text);
				new CSEmitterController(StageData, cs_Data);
			}
		}
	}
}
