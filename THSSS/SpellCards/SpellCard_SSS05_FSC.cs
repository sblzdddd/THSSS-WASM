using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000DB RID: 219
	internal class SpellCard_SSS05_FSC : BaseFSC
	{
		// Token: 0x06000608 RID: 1544 RVA: 0x000357D4 File Offset: 0x000339D4
		public SpellCard_SSS05_FSC(StageDataPackage StageData)
			: base(StageData)
		{
			if (base.Difficulty < DifficultLevel.Hard)
			{
				base.SC_Name = "烈焰「焚秽净世」";
			}
			else
			{
				base.SC_Name = "烈焰「焚秽净世」";
			}
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss05(StageData);
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRakukun_ct");
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00035888 File Offset: 0x00033A88
		public override void Shoot()
		{
			if (base.Time > 100)
			{
				base.Boss.MoveUpDown();
				base.Boss.Armon = base.Boss.ArmonArray[8];
			}
			if (base.Time == 100)
			{
				string text;
				switch (base.Difficulty)
				{
				case DifficultLevel.Hard:
					text = ".\\CS\\St05\\关底Boss\\FSC符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St05\\关底Boss\\FSC符L.mbg";
					break;
				default:
					text = ".\\CS\\St05\\关底Boss\\FSC符L.mbg";
					break;
				}
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
