using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200013B RID: 315
	internal class SpellCard_SSS04_02A : BaseSpellCard
	{
		// Token: 0x06000843 RID: 2115 RVA: 0x0004C948 File Offset: 0x0004AB48
		public SpellCard_SSS04_02A(StageDataPackage StageData)
			: base(StageData)
		{
			if (base.Difficulty < DifficultLevel.Hard)
			{
				base.SC_Name = "镜符「月季色红梦裂隙」";
			}
			else
			{
				base.SC_Name = "镜符「月季色红梦裂隙」";
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

		// Token: 0x06000844 RID: 2116 RVA: 0x0004CA14 File Offset: 0x0004AC14
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
					text = ".\\CS\\St04\\关底Boss\\2符E0.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St04\\关底Boss\\2符N0.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St04\\关底Boss\\2符H0.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St04\\关底Boss\\2符L0.mbg";
					break;
				default:
					text = ".\\CS\\St04\\关底Boss\\2符U0.mbg";
					break;
				}
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
