using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200017B RID: 379
	internal class SpellCard_SSS04_04A : BaseTimeSC
	{
		// Token: 0x0600092A RID: 2346 RVA: 0x00058210 File Offset: 0x00056410
		public SpellCard_SSS04_04A(StageDataPackage StageData)
			: base(StageData)
		{
			if (base.Difficulty < DifficultLevel.Hard)
			{
				base.SC_Name = "辉针「闪耀万针剑」";
			}
			else
			{
				base.SC_Name = "辉针「闪耀万针剑」";
			}
			base.BaseScore = 25000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 100);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss04(StageData);
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceKage_ct");
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x000582D0 File Offset: 0x000564D0
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[5];
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
					text = ".\\CS\\St04\\关底Boss\\4符E0.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St04\\关底Boss\\4符N0.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St04\\关底Boss\\4符H0.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St04\\关底Boss\\4符L0.mbg";
					break;
				default:
					text = ".\\CS\\St04\\关底Boss\\4符U0.mbg";
					break;
				}
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
			if ((base.Time - 100) % 200 == 0)
			{
				base.Boss.OnAction = 150;
			}
		}
	}
}
