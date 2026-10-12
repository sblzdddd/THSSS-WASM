using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS04_04A : BaseTimeSC
	{
		public SpellCard_SSS04_04A(StageDataPackage StageData)
			: base(StageData)
		{
			if (Difficulty < DifficultLevel.Hard)
			{
				SC_Name = "辉针「闪耀万针剑」";
			}
			else
			{
				SC_Name = "辉针「闪耀万针剑」";
			}
			BaseScore = 25000000L;
			Boss.DestPoint = new Point(BoundRect.Width / 2, 100);
			Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss04(StageData);
			new BulletTitle(StageData, SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceKage_ct");
		}

		public override void Shoot()
		{
			Boss.Armon = Boss.ArmonArray[5];
			Boss.HitEnabled = false;
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
					text = ".\\CS\\St04\\关底Boss\\4符E0.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St04\\关底Boss\\4符N0.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St04\\关底Boss\\4符H0.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St04\\关底Boss\\4符L0.mbg";
					break;
				default:
					text = ".\\CS\\St04\\关底Boss\\4符U0.mbg";
					break;
				}
				CS_Data cs_Data = StageData.LoadCS(text);
				new CSEmitterController(StageData, cs_Data);
			}
			if ((Time - 100) % 200 == 0)
			{
				Boss.OnAction = 150;
			}
		}
	}
}
