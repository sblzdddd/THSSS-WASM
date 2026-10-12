using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS05_02 : BaseSpellCard
	{
		public SpellCard_SSS05_02(StageDataPackage StageData)
			: base(StageData)
		{
			if (Difficulty < DifficultLevel.Hard)
			{
				SC_Name = "机关「火焰发射装置」";
			}
			else
			{
				SC_Name = "焰机「星夜的火焰舞曲」";
			}
			BaseScore = 30000000L;
			Boss.DestPoint = new Point(BoundRect.Width / 2, 120);
			Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss05(StageData);
			new BulletTitle(StageData, SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRakukun_ct");
		}

		public override void Shoot()
		{
			Boss.Armon = Boss.ArmonArray[3];
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
					text = ".\\CS\\St05\\关底Boss\\2符E.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St05\\关底Boss\\2符N.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St05\\关底Boss\\2符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St05\\关底Boss\\2符L.mbg";
					break;
				default:
					text = ".\\CS\\St05\\关底Boss\\2符L.mbg";
					break;
				}
				var cs_Data = StageData.LoadCS(text);
				new CSEmitterController(StageData, cs_Data);
			}
		}
	}
}
