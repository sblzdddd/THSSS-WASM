using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000FD RID: 253
	internal class SpellCard_SSS04_03D : BaseSpellCard
	{
		// Token: 0x0600070D RID: 1805 RVA: 0x0003F47C File Offset: 0x0003D67C
		public SpellCard_SSS04_03D(StageDataPackage StageData)
			: base(StageData)
		{
			if (base.Difficulty < DifficultLevel.Hard)
			{
				base.SC_Name = "空想「恋之宣告」";
			}
			else
			{
				base.SC_Name = "空想「恋之宣告」";
			}
			base.BaseScore = 25000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 142);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss04(StageData);
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceKage_ct");
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0003F540 File Offset: 0x0003D740
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
					text = ".\\CS\\St04\\关底Boss\\3符E3.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St04\\关底Boss\\3符N3.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St04\\关底Boss\\3符H3.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St04\\关底Boss\\3符L3.mbg";
					break;
				default:
					text = ".\\CS\\St04\\关底Boss\\3符U3.mbg";
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
