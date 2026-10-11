using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200007A RID: 122
	internal class SpellCard_SSSEx_07B : BaseSpellCard
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		public SpellCard_SSSEx_07B(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "「雾隐流剑技 ～ 血染之樱」";
			base.BaseScore = 40000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 96);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRika_ct1");
			new BackgroundBossEx(StageData);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001C078 File Offset: 0x0001A278
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[13];
			if (base.Time > 100)
			{
				if ((base.Time - 30) % 780 == 50)
				{
					base.Boss.DestPoint = new PointF(base.MyPlane.OriginalPosition.X, base.Boss.OriginalPosition.Y);
					base.Boss.Velocity = 4f;
				}
			}
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\7符1.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
