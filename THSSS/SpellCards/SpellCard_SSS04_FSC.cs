using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS04_FSC : BaseFSC
	{
		public SpellCard_SSS04_FSC(StageDataPackage StageData)
			: base(StageData)
		{
			SC_Name = "境界「三原色」";
			Boss.DestPoint = new Point(BoundRect.Width / 2, 120);
			Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss04(StageData);
			new BulletTitle(StageData, SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceKage_ct");
		}

		public override void Shoot()
		{
			if (Time > 100)
			{
				Boss.MoveUpDown();
				Boss.Armon = Boss.ArmonArray[7];
			}
			if (Time == 100)
			{
				string text;
				switch (Difficulty)
				{
				case DifficultLevel.Hard:
					text = ".\\CS\\St04\\关底Boss\\FSC符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St04\\关底Boss\\FSC符L.mbg";
					break;
				default:
					text = ".\\CS\\St04\\关底Boss\\FSC符L.mbg";
					break;
				}
				var cs_Data = StageData.LoadCS(text);
				new CSEmitterController(StageData, cs_Data);
			}
		}
	}
}
