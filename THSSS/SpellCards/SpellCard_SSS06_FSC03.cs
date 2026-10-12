using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS06_FSC03 : BaseTimeSC
	{
		public SpellCard_SSS06_FSC03(StageDataPackage StageData)
			: base(StageData)
		{
			SC_Name = "第三个愿望「永生不灭」";
			Boss.DestPoint = new Point(BoundRect.Width / 2, 120);
			Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss06(StageData);
			new BulletTitle(StageData, SC_Name);
			new SpellCardAttack(StageData);
			new FullPic3(StageData, "FaceTensei_ct3");
		}

		public override void Shoot()
		{
			if (Time > 100)
			{
				Boss.MoveUpDown();
				Boss.Armon = Boss.ArmonArray[12];
			}
			if (Time == 100)
			{
				string text;
				switch (Difficulty)
				{
				case DifficultLevel.Hard:
					text = ".\\CS\\St06\\关底Boss\\FSC3H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St06\\关底Boss\\FSC3L.mbg";
					break;
				default:
					text = ".\\CS\\St06\\关底Boss\\FSC3L.mbg";
					break;
				}
				CS_Data cs_Data = StageData.LoadCS(text);
				new CSEmitterController(StageData, cs_Data);
			}
		}

		public override void GiveEndEffect()
		{
			if (!Bombed && !Missed)
			{
				if (Time >= SpellTime)
				{
					StageData.SoundPlay("se_cardget.wav");
					new SpellCardBonus(StageData, GetSpellCardBonus());
					if (!StageData.OnReplay)
					{
						SC_Data.Recorded++;
					}
				}
			}
			Background3D.Enabled3D = true;
			StageData.SCBstring = null;
		}

		public override long GetSpellCardBonus()
		{
			long num;
			if (Bombed || Missed)
			{
				num = 0L;
			}
			else
			{
				long num2 = 99999990L;
				num = num2;
			}
			return num;
		}
	}
}
