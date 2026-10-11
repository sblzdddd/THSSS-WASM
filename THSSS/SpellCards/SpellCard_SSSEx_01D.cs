using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000157 RID: 343
	internal class SpellCard_SSSEx_01D : BaseSpellCard
	{
		// Token: 0x060008C2 RID: 2242 RVA: 0x00052BEC File Offset: 0x00050DEC
		public SpellCard_SSSEx_01D(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "迴梦「月色琉璃光」";
			base.BaseScore = 40000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRika_ct3");
			new BackgroundBossEx(StageData);
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x00052C88 File Offset: 0x00050E88
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[1];
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\1符3.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
