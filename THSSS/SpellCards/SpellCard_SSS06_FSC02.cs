using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS06_FSC02 : BaseFSC
	{
		public SpellCard_SSS06_FSC02(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "第二个愿望「无穷之力」";
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
			if (base.Time > 100)
			{
				base.Boss.MoveUpDown();
				base.Boss.Armon = base.Boss.ArmonArray[11];
			}
			if (base.Time == 100)
			{
				string text;
				switch (base.Difficulty)
				{
				case DifficultLevel.Hard:
					text = ".\\CS\\St06\\关底Boss\\FSC2H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St06\\关底Boss\\FSC2L.mbg";
					break;
				default:
					text = ".\\CS\\St06\\关底Boss\\FSC2L.mbg";
					break;
				}
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
