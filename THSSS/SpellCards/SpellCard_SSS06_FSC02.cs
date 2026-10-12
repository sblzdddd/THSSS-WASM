using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS06_FSC02 : BaseFSC
	{
		public SpellCard_SSS06_FSC02(StageDataPackage StageData)
			: base(StageData)
		{
			SC_Name = "第二个愿望「无穷之力」";
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
				Boss.Armon = Boss.ArmonArray[11];
			}
			if (Time == 100)
			{
				string text;
				switch (Difficulty)
				{
				case DifficultLevel.Hard:
					text = ".\\CS\\St06\\关底Boss\\FSC2H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St06\\关底Boss\\FSC2L.mbg";
					break;
				default:
					text = ".\\CS\\St06\\关底Boss\\FSC2L.mbg";
					break;
				}
				CS_Data cs_Data = StageData.LoadCS(text);
				new CSEmitterController(StageData, cs_Data);
			}
		}
	}
}
