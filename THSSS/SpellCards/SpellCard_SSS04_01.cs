using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200002F RID: 47
	internal class SpellCard_SSS04_01 : BaseSpellCard
	{
		// Token: 0x060001F2 RID: 498 RVA: 0x0000B9AC File Offset: 0x00009BAC
		public SpellCard_SSS04_01(StageDataPackage StageData)
			: base(StageData)
		{
			if (base.Difficulty < DifficultLevel.Hard)
			{
				base.SC_Name = "魔镜「幻影之境」";
			}
			else
			{
				base.SC_Name = "魔镜「虚无幽幻之境」";
			}
			base.BaseScore = 25000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss04(StageData);
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			FullPic2 fullPic = new FullPic2(StageData, "FaceKage_ct");
			fullPic.Scale = 0.8f;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000BA78 File Offset: 0x00009C78
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[1];
			if (base.Time > 50)
			{
				base.Boss.MoveUpDown();
			}
			if (base.Time == 100)
			{
				string text;
				switch (base.Difficulty)
				{
				case DifficultLevel.Easy:
					text = ".\\CS\\St04\\关底Boss\\1符E.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St04\\关底Boss\\1符N.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St04\\关底Boss\\1符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St04\\关底Boss\\1符L.mbg";
					break;
				default:
					text = ".\\CS\\St04\\关底Boss\\1符U.mbg";
					break;
				}
				CS_Data cs_Data = base.StageData.LoadCS(text);
				CSEmitterController csemitterController = new CSEmitterController(base.StageData, cs_Data);
				csemitterController.BossBinding = true;
			}
		}
	}
}
