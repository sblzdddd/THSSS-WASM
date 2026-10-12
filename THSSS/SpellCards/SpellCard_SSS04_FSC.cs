using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS04_FSC : BaseFSC
	{
		public SpellCard_SSS04_FSC(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "境界「三原色」";
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss04(StageData);
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceKage_ct");
		}

		public override void Shoot()
		{
			if (base.Time > 100)
			{
				base.Boss.MoveUpDown();
				base.Boss.Armon = base.Boss.ArmonArray[7];
			}
			if (base.Time == 100)
			{
				string text;
				switch (base.Difficulty)
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
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
