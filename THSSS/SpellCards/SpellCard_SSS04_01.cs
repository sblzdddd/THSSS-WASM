using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS04_01 : BaseSpellCard
	{
		public SpellCard_SSS04_01(StageDataPackage StageData)
			: base(StageData)
		{
			if (Difficulty < DifficultLevel.Hard)
			{
				SC_Name = "魔镜「幻影之境」";
			}
			else
			{
				SC_Name = "魔镜「虚无幽幻之境」";
			}
			BaseScore = 25000000L;
			Boss.DestPoint = new Point(BoundRect.Width / 2, 120);
			Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss04(StageData);
			new BulletTitle(StageData, SC_Name);
			new SpellCardAttack(StageData);
			FullPic2 fullPic = new FullPic2(StageData, "FaceKage_ct");
			fullPic.Scale = 0.8f;
		}

		public override void Shoot()
		{
			Boss.Armon = Boss.ArmonArray[1];
			if (Time > 50)
			{
				Boss.MoveUpDown();
			}
			if (Time == 100)
			{
				string text;
				switch (Difficulty)
				{
				case DifficultLevel.Easy:
					text = ".\\CS\\St04\\关底Boss\\1符E.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St04\\关底Boss\\1符N.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St04\\关底Boss\\1符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St04\\关底Boss\\1符L.mbg";
					break;
				default:
					text = ".\\CS\\St04\\关底Boss\\1符U.mbg";
					break;
				}
				CS_Data cs_Data = StageData.LoadCS(text);
				CSEmitterController csemitterController = new CSEmitterController(StageData, cs_Data);
				csemitterController.BossBinding = true;
			}
		}
	}
}
