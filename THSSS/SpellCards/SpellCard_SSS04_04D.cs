using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS04_04D : BaseTimeSC
	{
		public SpellCard_SSS04_04D(StageDataPackage StageData)
			: base(StageData)
		{
			if (Difficulty < DifficultLevel.Hard)
			{
				SC_Name = "「古明地恋的消失」";
			}
			else
			{
				SC_Name = "「古明地恋的消失」";
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
					text = ".\\CS\\St04\\关底Boss\\4符E3.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St04\\关底Boss\\4符N3.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St04\\关底Boss\\4符H3.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St04\\关底Boss\\4符L3.mbg";
					break;
				default:
					text = ".\\CS\\St04\\关底Boss\\4符U3.mbg";
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
