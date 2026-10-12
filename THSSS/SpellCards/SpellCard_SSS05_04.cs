using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS05_04 : BaseSpellCard
	{
		public SpellCard_SSS05_04(StageDataPackage StageData)
			: base(StageData)
		{
			if (Difficulty < DifficultLevel.Hard)
			{
				SC_Name = "焰符「原始的烈焰」";
			}
			else
			{
				SC_Name = "焰符「八百里的烈焰」";
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
			Boss.Armon = Boss.ArmonArray[6];
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
					text = ".\\CS\\St05\\关底Boss\\4符E.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St05\\关底Boss\\4符N.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St05\\关底Boss\\4符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St05\\关底Boss\\4符L.mbg";
					break;
				default:
					text = ".\\CS\\St05\\关底Boss\\4符L.mbg";
					break;
				}
				CS_Data cs_Data = StageData.LoadCS(text);
				new CSEmitterController(StageData, cs_Data);
			}
		}
	}
}
