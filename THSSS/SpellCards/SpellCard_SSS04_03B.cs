using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS04_03B : BaseSpellCard
	{
		public SpellCard_SSS04_03B(StageDataPackage StageData)
			: base(StageData)
		{
			if (base.Difficulty < DifficultLevel.Hard)
			{
				base.SC_Name = "流光「幻影魔炮」";
			}
			else
			{
				base.SC_Name = "流光「幻影魔炮」";
			}
			base.BaseScore = 25000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 128);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss04(StageData);
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceKage_ct");
		}

		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[4];
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
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
			if ((base.Time - 160) % 363 == 0)
			{
				base.Boss.OnAction = 250;
			}
		}
	}
}
