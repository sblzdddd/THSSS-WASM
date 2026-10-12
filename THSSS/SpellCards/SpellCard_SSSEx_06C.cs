using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSSEx_06C : BaseSpellCard
	{
		public SpellCard_SSSEx_06C(StageDataPackage StageData)
			: base(StageData)
		{
			SC_Name = "雪幕「冰雪流凌」";
			BaseScore = 40000000L;
			Boss.DestPoint = new Point(BoundRect.Width / 2, 128);
			Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRika_ct2");
			new BackgroundBossEx(StageData);
		}

		public override void Shoot()
		{
			Boss.Armon = Boss.ArmonArray[11];
			if (Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\6符2.mbg";
				CS_Data cs_Data = StageData.LoadCS(text);
				new CSEmitterController(StageData, cs_Data);
			}
		}
	}
}
