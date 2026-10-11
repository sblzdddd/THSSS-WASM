using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001B1 RID: 433
	internal class SpellCard_SSS04_05 : BaseTimeSC
	{
		// Token: 0x06000A37 RID: 2615 RVA: 0x00061284 File Offset: 0x0005F484
		public SpellCard_SSS04_05(StageDataPackage StageData)
			: base(StageData)
		{
			if (base.Difficulty < DifficultLevel.Hard)
			{
				base.SC_Name = "虹奥义「光幕结界」";
			}
			else
			{
				base.SC_Name = "虹奥义「光幕结界」";
			}
			base.BaseScore = 25000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss04(StageData);
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceKage_ct");
			base.Boss.TransparentValueF = 0f;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00061354 File Offset: 0x0005F554
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[6];
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
					text = ".\\CS\\St04\\关底Boss\\5符E.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St04\\关底Boss\\5符N.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St04\\关底Boss\\5符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St04\\关底Boss\\5符L.mbg";
					break;
				default:
					text = ".\\CS\\St04\\关底Boss\\5符U.mbg";
					break;
				}
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
