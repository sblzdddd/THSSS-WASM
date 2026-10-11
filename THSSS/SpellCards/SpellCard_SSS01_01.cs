using System;
using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS01_01 : BaseSpellCard
	{
		public SpellCard_SSS01_01(StageDataPackage StageData)
			: base(StageData)
		{
			if (base.Difficulty < DifficultLevel.Hard)
			{
				base.SC_Name = "";
			}
			else
			{
				base.SC_Name = "微风「摇曳的三叶草」";
			}
			base.BaseScore = 10000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			BackgroundMove2 backgroundMove = new BackgroundMove2(StageData, "Card01b");
			backgroundMove.DirectionDegree = 270.0;
			BackgroundMove2 backgroundMove2 = new BackgroundMove2(StageData, "Card01a");
			backgroundMove2.DirectionDegree = -80.0;
			backgroundMove2.Length = 1024;
			backgroundMove2.TransparentValueF = 80f;
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceAmi_ct");
		}

		public override void Shoot()
		{
			base.Boss.Armon = 0.7f;
			if (base.Time > 100)
			{
				base.Boss.MoveUpDown();
			}
			if (base.Time == 150)
			{
				base.Boss.OnAction = 10000;
				string text;
				switch (base.Difficulty)
				{
				case DifficultLevel.Easy:
					text = "";
					break;
				case DifficultLevel.Normal:
					text = "";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St01\\道中Boss\\1符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St01\\道中Boss\\1符L.mbg";
					break;
				default:
					text = ".\\CS\\St01\\道中Boss\\1符L.mbg";
					break;
				}
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
