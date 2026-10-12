using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS04_04D : BaseTimeSC
	{
		public SpellCard_SSS04_04D(StageDataPackage StageData)
			: base(StageData)
		{
			if (base.Difficulty < DifficultLevel.Hard)
			{
				base.SC_Name = "「古明地恋的消失」";
			}
			else
			{
				base.SC_Name = "「古明地恋的消失」";
			}
			base.BaseScore = 25000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 100);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss04(StageData);
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceKage_ct");
		}

		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[5];
			base.Boss.HitEnabled = false;
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
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
			if ((base.Time - 100) % 200 == 0)
			{
				base.Boss.OnAction = 150;
			}
		}
	}
}
