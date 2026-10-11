using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001F3 RID: 499
	internal class SpellCard_SSS06_04 : BaseSpellCard
	{
		// Token: 0x06000B54 RID: 2900 RVA: 0x0006FDB0 File Offset: 0x0006DFB0
		public SpellCard_SSS06_04(StageDataPackage StageData)
			: base(StageData)
		{
			if (base.Difficulty < DifficultLevel.Hard)
			{
				base.SC_Name = "星图「宇宙棋盘」";
			}
			else
			{
				base.SC_Name = "星图「宇宙棋盘」";
			}
			base.BaseScore = 35000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss06(StageData);
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic3(StageData, "FaceTensei_ct2");
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0006FE70 File Offset: 0x0006E070
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[7];
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
					text = ".\\CS\\St06\\关底Boss\\4符E.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St06\\关底Boss\\4符N.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St06\\关底Boss\\4符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St06\\关底Boss\\4符L.mbg";
					break;
				default:
					text = ".\\CS\\St06\\关底Boss\\4符L.mbg";
					break;
				}
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
