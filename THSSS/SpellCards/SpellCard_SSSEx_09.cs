using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSSEx_09 : BaseTimeSC
	{
		public SpellCard_SSSEx_09(StageDataPackage StageData)
			: base(StageData)
		{
			SC_Name = "仙蝶「无限的彩灵」";
			BaseScore = 40000000L;
			Boss.DestPoint = new Point(BoundRect.Width / 2, -150);
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
			Boss.Armon = Boss.ArmonArray[16];
			if (Time == 100)
			{
				var text = ".\\CS\\StEx\\关底Boss\\9符.mbg";
				var cs_Data = StageData.LoadCS(text);
				new CSEmitterController(StageData, cs_Data);
			}
		}
	}
}
