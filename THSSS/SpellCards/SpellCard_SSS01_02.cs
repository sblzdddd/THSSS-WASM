using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS01_02 : BaseSpellCard
	{
		public SpellCard_SSS01_02(StageDataPackage StageData)
			: base(StageData)
		{
			if (Difficulty < DifficultLevel.Hard)
			{
				SC_Name = "星符「夏夜的晚星」";
			}
			else
			{
				SC_Name = "星符「夏夜的晚星」";
			}
			BaseScore = 10000000L;
			Boss.DestPoint = new Point(BoundRect.Width / 2, 120);
			Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			BackgroundMove2 backgroundMove = new BackgroundMove2(StageData, "Card01b");
			backgroundMove.DirectionDegree = 270.0;
			BackgroundMove2 backgroundMove2 = new BackgroundMove2(StageData, "Card01a");
			backgroundMove2.DirectionDegree = -80.0;
			backgroundMove2.Length = 1024;
			backgroundMove2.TransparentValueF = 80f;
			new BulletTitle(StageData, SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceAmi_ct");
		}

		public override void Shoot()
		{
			Boss.Armon = 0.905f;
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
					text = ".\\CS\\St01\\关底Boss\\1符E.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St01\\关底Boss\\1符N.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St01\\关底Boss\\1符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St01\\关底Boss\\1符L.mbg";
					break;
				default:
					text = ".\\CS\\St01\\关底Boss\\1符L.mbg";
					break;
				}
				CS_Data cs_Data = StageData.LoadCS(text);
				new CSEmitterController(StageData, cs_Data);
			}
			if ((Time - 150) % 430 == 0)
			{
				Boss.OnAction = 150;
			}
		}
	}
}
