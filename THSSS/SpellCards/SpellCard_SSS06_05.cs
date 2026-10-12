using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS06_05 : BaseTimeSC
	{
		public SpellCard_SSS06_05(StageDataPackage StageData)
			: base(StageData)
		{
			if (Difficulty < DifficultLevel.Hard)
			{
				SC_Name = "「弹幕之海」";
			}
			else
			{
				SC_Name = "「弹幕之海」";
			}
			BaseScore = 35000000L;
			Boss.DestPoint = new Point(BoundRect.Width / 2, 120);
			Boss.Velocity = 4f;
			Boss.SpellTime = Boss.Time + 3000 - 30;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss06(StageData);
			new BulletTitle(StageData, SC_Name);
			new SpellCardAttack(StageData);
			new FullPic3(StageData, "FaceTensei_ct3");
		}

		public override void Shoot()
		{
			Boss.Armon = Boss.ArmonArray[8];
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
					text = ".\\CS\\St06\\关底Boss\\5符E.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St06\\关底Boss\\5符N.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St06\\关底Boss\\5符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St06\\关底Boss\\5符L.mbg";
					break;
				default:
					text = ".\\CS\\St06\\关底Boss\\5符L.mbg";
					break;
				}
				var cs_Data = StageData.LoadCS(text);
				new CSEmitterController(StageData, cs_Data);
			}
		}

		public override void GiveEndEffect()
		{
			base.GiveEndEffect();
			Boss.HitEnabled = true;
		}
	}
}
