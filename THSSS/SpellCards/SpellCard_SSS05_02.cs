using System;
using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS05_02 : BaseSpellCard
	{
		public SpellCard_SSS05_02(StageDataPackage StageData)
			: base(StageData)
		{
			if (base.Difficulty < DifficultLevel.Hard)
			{
				base.SC_Name = "机关「火焰发射装置」";
			}
			else
			{
				base.SC_Name = "焰机「星夜的火焰舞曲」";
			}
			base.BaseScore = 30000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss05(StageData);
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRakukun_ct");
		}

		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[3];
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
					text = ".\\CS\\St05\\关底Boss\\2符E.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St05\\关底Boss\\2符N.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St05\\关底Boss\\2符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St05\\关底Boss\\2符L.mbg";
					break;
				default:
					text = ".\\CS\\St05\\关底Boss\\2符L.mbg";
					break;
				}
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
