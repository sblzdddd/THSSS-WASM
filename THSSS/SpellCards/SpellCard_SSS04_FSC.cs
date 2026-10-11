using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000057 RID: 87
	internal class SpellCard_SSS04_FSC : BaseFSC
	{
		// Token: 0x060002AB RID: 683 RVA: 0x00012364 File Offset: 0x00010564
		public SpellCard_SSS04_FSC(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "境界「三原色」";
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss04(StageData);
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceKage_ct");
		}

		// Token: 0x060002AC RID: 684 RVA: 0x000123F4 File Offset: 0x000105F4
		public override void Shoot()
		{
			if (base.Time > 100)
			{
				base.Boss.MoveUpDown();
				base.Boss.Armon = base.Boss.ArmonArray[7];
			}
			if (base.Time == 100)
			{
				string text;
				switch (base.Difficulty)
				{
				case DifficultLevel.Hard:
					text = ".\\CS\\St04\\关底Boss\\FSC符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St04\\关底Boss\\FSC符L.mbg";
					break;
				default:
					text = ".\\CS\\St04\\关底Boss\\FSC符L.mbg";
					break;
				}
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
