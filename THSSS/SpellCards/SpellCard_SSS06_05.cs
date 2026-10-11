using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001D2 RID: 466
	internal class SpellCard_SSS06_05 : BaseTimeSC
	{
		// Token: 0x06000AA7 RID: 2727 RVA: 0x000678CC File Offset: 0x00065ACC
		public SpellCard_SSS06_05(StageDataPackage StageData)
			: base(StageData)
		{
			if (base.Difficulty < DifficultLevel.Hard)
			{
				base.SC_Name = "「弹幕之海」";
			}
			else
			{
				base.SC_Name = "「弹幕之海」";
			}
			base.BaseScore = 35000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			base.Boss.SpellTime = base.Boss.Time + 3000 - 30;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss06(StageData);
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic3(StageData, "FaceTensei_ct3");
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x000679AC File Offset: 0x00065BAC
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[8];
			base.Boss.HitEnabled = false;
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
					text = ".\\CS\\St06\\关底Boss\\5符E.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St06\\关底Boss\\5符N.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St06\\关底Boss\\5符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St06\\关底Boss\\5符L.mbg";
					break;
				default:
					text = ".\\CS\\St06\\关底Boss\\5符L.mbg";
					break;
				}
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00067A76 File Offset: 0x00065C76
		public override void GiveEndEffect()
		{
			base.GiveEndEffect();
			base.Boss.HitEnabled = true;
		}
	}
}
