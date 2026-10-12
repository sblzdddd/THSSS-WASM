using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSSEx_04C : BaseSpellCard
	{
		public SpellCard_SSSEx_04C(StageDataPackage StageData)
			: base(StageData)
		{
			SC_Name = "米雪「冷云雪晶」";
			BaseScore = 40000000L;
			Boss.DestPoint = new Point(BoundRect.Width / 2, 160);
			Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRika_ct2");
			new BackgroundBossEx(StageData);
		}

		public override void Shoot()
		{
			Boss.Armon = Boss.ArmonArray[7];
			if (Time == 100)
			{
				var text = ".\\CS\\StEx\\关底Boss\\4符2.mbg";
				var cs_Data = StageData.LoadCS(text);
				new CSEmitterController(StageData, cs_Data);
			}
		}
	}
}
