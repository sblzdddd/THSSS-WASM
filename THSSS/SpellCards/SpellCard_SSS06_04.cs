using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS06_04 : BaseSpellCard
	{
		public SpellCard_SSS06_04(StageDataPackage StageData)
			: base(StageData)
		{
			if (Difficulty < DifficultLevel.Hard)
			{
				SC_Name = "星图「宇宙棋盘」";
			}
			else
			{
				SC_Name = "星图「宇宙棋盘」";
			}
			BaseScore = 35000000L;
			Boss.DestPoint = new Point(BoundRect.Width / 2, 120);
			Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss06(StageData);
			new BulletTitle(StageData, SC_Name);
			new SpellCardAttack(StageData);
			new FullPic3(StageData, "FaceTensei_ct2");
		}

		public override void Shoot()
		{
			Boss.Armon = Boss.ArmonArray[7];
			if (Time > 100)
			{
				Boss.MoveUpDown();
			}
			if (Time == 150)
			{
				string text;
				switch (Difficulty)
				{
				case DifficultLevel.Easy:
					text = ".\\CS\\St06\\关底Boss\\4符E.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St06\\关底Boss\\4符N.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St06\\关底Boss\\4符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St06\\关底Boss\\4符L.mbg";
					break;
				default:
					text = ".\\CS\\St06\\关底Boss\\4符L.mbg";
					break;
				}
				var cs_Data = StageData.LoadCS(text);
				new CSEmitterController(StageData, cs_Data);
			}
		}
	}
}
