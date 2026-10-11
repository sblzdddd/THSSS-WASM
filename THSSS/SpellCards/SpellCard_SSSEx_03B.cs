using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001F1 RID: 497
	internal class SpellCard_SSSEx_03B : BaseSpellCard
	{
		// Token: 0x06000B50 RID: 2896 RVA: 0x0006FBC0 File Offset: 0x0006DDC0
		public SpellCard_SSSEx_03B(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "萱草「忘却之爱」";
			base.BaseScore = 40000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRika_ct1");
			new BackgroundBossEx(StageData);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0006FC5C File Offset: 0x0006DE5C
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[5];
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\3符1.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
