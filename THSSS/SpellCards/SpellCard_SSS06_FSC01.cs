using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200017A RID: 378
	internal class SpellCard_SSS06_FSC01 : BaseFSC
	{
		// Token: 0x06000928 RID: 2344 RVA: 0x000580D8 File Offset: 0x000562D8
		public SpellCard_SSS06_FSC01(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "第一个愿望「无尽之财」";
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss06(StageData);
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic3(StageData, "FaceTensei_ct3");
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00058168 File Offset: 0x00056368
		public override void Shoot()
		{
			if (base.Time > 100)
			{
				base.Boss.MoveUpDown();
				base.Boss.Armon = base.Boss.ArmonArray[10];
			}
			if (base.Time == 100)
			{
				string text;
				switch (base.Difficulty)
				{
				case DifficultLevel.Hard:
					text = ".\\CS\\St06\\关底Boss\\FSC1H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St06\\关底Boss\\FSC1L.mbg";
					break;
				default:
					text = ".\\CS\\St06\\关底Boss\\FSC1L.mbg";
					break;
				}
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
