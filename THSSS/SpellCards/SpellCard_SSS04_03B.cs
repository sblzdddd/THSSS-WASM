using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS04_03B : BaseSpellCard
	{
		public SpellCard_SSS04_03B(StageDataPackage StageData)
			: base(StageData)
		{
			if (Difficulty < DifficultLevel.Hard)
			{
				SC_Name = "流光「幻影魔炮」";
			}
			else
			{
				SC_Name = "流光「幻影魔炮」";
			}
			BaseScore = 25000000L;
			Boss.DestPoint = new Point(BoundRect.Width / 2, 128);
			Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss04(StageData);
			new BulletTitle(StageData, SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceKage_ct");
		}

		public override void Shoot()
		{
			Boss.Armon = Boss.ArmonArray[4];
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
					text = ".\\CS\\St04\\关底Boss\\3符E1.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St04\\关底Boss\\3符N1.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St04\\关底Boss\\3符H1.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St04\\关底Boss\\3符L1.mbg";
					break;
				default:
					text = ".\\CS\\St04\\关底Boss\\3符U1.mbg";
					break;
				}
				CS_Data cs_Data = StageData.LoadCS(text);
				new CSEmitterController(StageData, cs_Data);
			}
			if ((Time - 160) % 363 == 0)
			{
				Boss.OnAction = 250;
			}
		}
	}
}
