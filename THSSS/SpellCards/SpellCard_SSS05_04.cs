using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000DA RID: 218
	internal class SpellCard_SSS05_04 : BaseSpellCard
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x00035654 File Offset: 0x00033854
		public SpellCard_SSS05_04(StageDataPackage StageData)
			: base(StageData)
		{
			if (base.Difficulty < DifficultLevel.Hard)
			{
				base.SC_Name = "焰符「原始的烈焰」";
			}
			else
			{
				base.SC_Name = "焰符「八百里的烈焰」";
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

		// Token: 0x06000607 RID: 1543 RVA: 0x00035714 File Offset: 0x00033914
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[6];
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
					text = ".\\CS\\St05\\关底Boss\\4符E.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St05\\关底Boss\\4符N.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St05\\关底Boss\\4符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St05\\关底Boss\\4符L.mbg";
					break;
				default:
					text = ".\\CS\\St05\\关底Boss\\4符L.mbg";
					break;
				}
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
