using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS04_02D : BaseSpellCard
	{
		public SpellCard_SSS04_02D(StageDataPackage StageData)
			: base(StageData)
		{
			if (base.Difficulty < DifficultLevel.Hard)
			{
				base.SC_Name = "镜符「若竹色翠石裂隙」";
			}
			else
			{
				base.SC_Name = "镜符「若竹色翠石裂隙」";
			}
			base.BaseScore = 25000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss04(StageData);
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			FullPic2 fullPic = new FullPic2(StageData, "FaceKage_ct");
			fullPic.Scale = 0.8f;
		}

		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[3];
			if (base.Time > 100)
			{
				base.Boss.RandomMove(205, 20, 2f, new Rectangle(base.BoundRect.Width / 2 - 45, 100, 90, 40));
			}
			if (base.Time == 150)
			{
				base.Boss.OnAction = 10000;
				string text;
				switch (base.Difficulty)
				{
				case DifficultLevel.Easy:
					text = ".\\CS\\St04\\关底Boss\\2符E3.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St04\\关底Boss\\2符N3.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St04\\关底Boss\\2符H3.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St04\\关底Boss\\2符L3.mbg";
					break;
				default:
					text = ".\\CS\\St04\\关底Boss\\2符U3.mbg";
					break;
				}
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
