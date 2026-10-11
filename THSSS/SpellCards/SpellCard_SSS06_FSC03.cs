using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000128 RID: 296
	internal class SpellCard_SSS06_FSC03 : BaseTimeSC
	{
		// Token: 0x060007FD RID: 2045 RVA: 0x00048A90 File Offset: 0x00046C90
		public SpellCard_SSS06_FSC03(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "第三个愿望「永生不灭」";
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BackgroundBoss06(StageData);
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic3(StageData, "FaceTensei_ct3");
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00048B20 File Offset: 0x00046D20
		public override void Shoot()
		{
			if (base.Time > 100)
			{
				base.Boss.MoveUpDown();
				base.Boss.Armon = base.Boss.ArmonArray[12];
			}
			if (base.Time == 100)
			{
				string text;
				switch (base.Difficulty)
				{
				case DifficultLevel.Hard:
					text = ".\\CS\\St06\\关底Boss\\FSC3H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St06\\关底Boss\\FSC3L.mbg";
					break;
				default:
					text = ".\\CS\\St06\\关底Boss\\FSC3L.mbg";
					break;
				}
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00048BC8 File Offset: 0x00046DC8
		public override void GiveEndEffect()
		{
			if (!base.Bombed && !base.Missed)
			{
				if (base.Time >= base.SpellTime)
				{
					base.StageData.SoundPlay("se_cardget.wav");
					new SpellCardBonus(base.StageData, this.GetSpellCardBonus());
					if (!base.StageData.OnReplay)
					{
						this.SC_Data.Recorded++;
					}
				}
			}
			base.Background3D.Enabled3D = true;
			base.StageData.SCBstring = null;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00048C64 File Offset: 0x00046E64
		public override long GetSpellCardBonus()
		{
			long num;
			if (base.Bombed || base.Missed)
			{
				num = 0L;
			}
			else
			{
				long num2 = 99999990L;
				num = num2;
			}
			return num;
		}
	}
}
