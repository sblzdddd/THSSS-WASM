using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS06_FSC01 : BaseFSC
	{
		public SpellCard_SSS06_FSC01(StageDataPackage StageData)
			: base(StageData)
		{
			SC_Name = "第一个愿望「无尽之财」";
			Boss.DestPoint = new Point(BoundRect.Width / 2, 120);
			Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss06(StageData);
			new BulletTitle(StageData, SC_Name);
			new SpellCardAttack(StageData);
			new FullPic3(StageData, "FaceTensei_ct3");
		}

		public override void Shoot()
		{
			if (Time > 100)
			{
				Boss.MoveUpDown();
				Boss.Armon = Boss.ArmonArray[10];
			}
			if (Time == 100)
			{
				string text;
				switch (Difficulty)
				{
				case DifficultLevel.Hard:
					text = ".\\CS\\St06\\关底Boss\\FSC1H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St06\\关底Boss\\FSC1L.mbg";
					break;
				default:
					text = ".\\CS\\St06\\关底Boss\\FSC1L.mbg";
					break;
				}
				CS_Data cs_Data = StageData.LoadCS(text);
				new CSEmitterController(StageData, cs_Data);
			}
		}
	}
}
