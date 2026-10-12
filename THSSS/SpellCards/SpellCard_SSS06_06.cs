using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS06_06 : BaseSpellCard
	{
		public SpellCard_SSS06_06(StageDataPackage StageData)
			: base(StageData)
		{
			if (base.Difficulty < DifficultLevel.Hard)
			{
				base.SC_Name = "「流星神话」";
			}
			else
			{
				base.SC_Name = "「流星神话」";
			}
			base.BaseScore = 35000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss06(StageData);
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic3(StageData, "FaceTensei_ct3");
		}

		public override void Shoot()
		{
			if (base.SpellList.Count > 0 || base.MyPlane.Time < base.MyPlane.UnmatchedTime - 30)
			{
				base.Boss.Armon = 1f;
			}
			else
			{
				base.Boss.Armon = base.Boss.ArmonArray[9];
			}
			if (base.Time > 100)
			{
				base.Boss.MoveUpDown();
			}
			if (base.Time == 150)
			{
				string text;
				switch (base.Difficulty)
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
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
			if ((base.Boss.HealthPoint < (float)(base.Boss.MaxHP * 3) / 4f || base.Time > 1800) && this.flag == 1)
			{
				this.flag++;
				this.ClearEmitter();
				string text;
				switch (base.Difficulty)
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
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
			if ((base.Boss.HealthPoint < (float)(base.Boss.MaxHP * 2) / 4f || base.Time > 3600) && this.flag == 2)
			{
				this.flag++;
				this.ClearEmitter();
				string text;
				switch (base.Difficulty)
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
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
			if ((base.Boss.HealthPoint < (float)base.Boss.MaxHP / 4f || base.Time > 5400) && this.flag == 3)
			{
				this.flag++;
				this.ClearEmitter();
				string text;
				switch (base.Difficulty)
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
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}

		private void ClearEmitter()
		{
			for (int i = base.EnemyPlaneList.Count - 1; i >= 0; i--)
			{
				if (!(base.EnemyPlaneList[i] is BaseSpellCard))
				{
					base.EnemyPlaneList[i].GiveEndEffect();
					base.EnemyPlaneList.RemoveAt(i);
				}
			}
			foreach (BaseBullet_Touhou baseBullet_Touhou in base.BulletList)
			{
				baseBullet_Touhou.GiveEndEffect();
			}
			base.BulletList.Clear();
		}

		private int flag = 1;
	}
}
