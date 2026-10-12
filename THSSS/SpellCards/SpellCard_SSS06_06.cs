using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS06_06 : BaseSpellCard
	{
		public SpellCard_SSS06_06(StageDataPackage StageData)
			: base(StageData)
		{
			if (Difficulty < DifficultLevel.Hard)
			{
				SC_Name = "「流星神话」";
			}
			else
			{
				SC_Name = "「流星神话」";
			}
			BaseScore = 35000000L;
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
			if (SpellList.Count > 0 || MyPlane.Time < MyPlane.UnmatchedTime - 30)
			{
				Boss.Armon = 1f;
			}
			else
			{
				Boss.Armon = Boss.ArmonArray[9];
			}
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
					text = ".\\CS\\St06\\关底Boss\\6符P1E.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St06\\关底Boss\\6符P1N.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St06\\关底Boss\\6符P1H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St06\\关底Boss\\6符P1L.mbg";
					break;
				default:
					text = ".\\CS\\St06\\关底Boss\\6符P1L.mbg";
					break;
				}
				CS_Data cs_Data = StageData.LoadCS(text);
				new CSEmitterController(StageData, cs_Data);
			}
			if ((Boss.HealthPoint < (float)(Boss.MaxHP * 3) / 4f || Time > 1800) && flag == 1)
			{
				flag++;
				ClearEmitter();
				string text;
				switch (Difficulty)
				{
				case DifficultLevel.Easy:
					text = ".\\CS\\St06\\关底Boss\\6符P2E.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St06\\关底Boss\\6符P2N.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St06\\关底Boss\\6符P2H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St06\\关底Boss\\6符P2L.mbg";
					break;
				default:
					text = ".\\CS\\St06\\关底Boss\\6符P2L.mbg";
					break;
				}
				CS_Data cs_Data = StageData.LoadCS(text);
				new CSEmitterController(StageData, cs_Data);
			}
			if ((Boss.HealthPoint < (float)(Boss.MaxHP * 2) / 4f || Time > 3600) && flag == 2)
			{
				flag++;
				ClearEmitter();
				string text;
				switch (Difficulty)
				{
				case DifficultLevel.Easy:
					text = ".\\CS\\St06\\关底Boss\\6符P3E.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St06\\关底Boss\\6符P3N.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St06\\关底Boss\\6符P3H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St06\\关底Boss\\6符P3L.mbg";
					break;
				default:
					text = ".\\CS\\St06\\关底Boss\\6符P2L.mbg";
					break;
				}
				CS_Data cs_Data = StageData.LoadCS(text);
				new CSEmitterController(StageData, cs_Data);
			}
			if ((Boss.HealthPoint < (float)Boss.MaxHP / 4f || Time > 5400) && flag == 3)
			{
				flag++;
				ClearEmitter();
				string text;
				switch (Difficulty)
				{
				case DifficultLevel.Easy:
					text = ".\\CS\\St06\\关底Boss\\6符P4E.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St06\\关底Boss\\6符P4N.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St06\\关底Boss\\6符P4H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St06\\关底Boss\\6符P4L.mbg";
					break;
				default:
					text = ".\\CS\\St06\\关底Boss\\6符P4L.mbg";
					break;
				}
				CS_Data cs_Data = StageData.LoadCS(text);
				new CSEmitterController(StageData, cs_Data);
			}
		}

		private void ClearEmitter()
		{
			for (int i = EnemyPlaneList.Count - 1; i >= 0; i--)
			{
				if (!(EnemyPlaneList[i] is BaseSpellCard))
				{
					EnemyPlaneList[i].GiveEndEffect();
					EnemyPlaneList.RemoveAt(i);
				}
			}
			foreach (BaseBullet_Touhou baseBullet_Touhou in BulletList)
			{
				baseBullet_Touhou.GiveEndEffect();
			}
			BulletList.Clear();
		}

		private int flag = 1;
	}
}
