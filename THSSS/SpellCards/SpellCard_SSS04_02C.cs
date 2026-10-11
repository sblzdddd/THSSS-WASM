using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000FE RID: 254
	internal class SpellCard_SSS04_02C : BaseSpellCard
	{
		// Token: 0x0600070F RID: 1807 RVA: 0x0003F62C File Offset: 0x0003D82C
		public SpellCard_SSS04_02C(StageDataPackage StageData)
			: base(StageData)
		{
			if (base.Difficulty < DifficultLevel.Hard)
			{
				base.SC_Name = "镜符「瑠璃色银河裂隙」";
			}
			else
			{
				base.SC_Name = "镜符「瑠璃色银河裂隙」";
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

		// Token: 0x06000710 RID: 1808 RVA: 0x0003F6F8 File Offset: 0x0003D8F8
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[3];
			if (base.Time > 100)
			{
				base.Boss.RandomMove(205, 20, 2f, new Rectangle(base.BoundRect.Width / 2 - 45, 100, 90, 40));
			}
			if (base.Time == 150)
			{
				base.Boss.OnAction = 10000;
				string text;
				switch (base.Difficulty)
				{
				case DifficultLevel.Easy:
					text = ".\\CS\\St04\\关底Boss\\2符E2.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St04\\关底Boss\\2符N2.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St04\\关底Boss\\2符H2.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St04\\关底Boss\\2符L2.mbg";
					break;
				default:
					text = ".\\CS\\St04\\关底Boss\\2符U2.mbg";
					break;
				}
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
