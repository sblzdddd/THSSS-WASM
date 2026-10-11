using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000DE RID: 222
	internal class SpellCard_SSS01_02 : BaseSpellCard
	{
		// Token: 0x0600060E RID: 1550 RVA: 0x00035E64 File Offset: 0x00034064
		public SpellCard_SSS01_02(StageDataPackage StageData)
			: base(StageData)
		{
			if (base.Difficulty < DifficultLevel.Hard)
			{
				base.SC_Name = "星符「夏夜的晚星」";
			}
			else
			{
				base.SC_Name = "星符「夏夜的晚星」";
			}
			base.BaseScore = 10000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			BackgroundMove2 backgroundMove = new BackgroundMove2(StageData, "Card01b");
			backgroundMove.DirectionDegree = 270.0;
			BackgroundMove2 backgroundMove2 = new BackgroundMove2(StageData, "Card01a");
			backgroundMove2.DirectionDegree = -80.0;
			backgroundMove2.Length = 1024;
			backgroundMove2.TransparentValueF = 80f;
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceAmi_ct");
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00035F70 File Offset: 0x00034170
		public override void Shoot()
		{
			base.Boss.Armon = 0.905f;
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
					text = ".\\CS\\St01\\关底Boss\\1符E.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St01\\关底Boss\\1符N.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St01\\关底Boss\\1符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St01\\关底Boss\\1符L.mbg";
					break;
				default:
					text = ".\\CS\\St01\\关底Boss\\1符L.mbg";
					break;
				}
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
			if ((base.Time - 150) % 430 == 0)
			{
				base.Boss.OnAction = 150;
			}
		}
	}
}
