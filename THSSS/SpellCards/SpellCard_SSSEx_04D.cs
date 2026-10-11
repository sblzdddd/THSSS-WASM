using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001AD RID: 429
	internal class SpellCard_SSSEx_04D : BaseSpellCard
	{
		// Token: 0x06000A2E RID: 2606 RVA: 0x00060AD8 File Offset: 0x0005ECD8
		public SpellCard_SSSEx_04D(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "仙幻「月葬」";
			base.BaseScore = 40000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRika_ct3");
			new BackgroundBossEx(StageData);
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00060B74 File Offset: 0x0005ED74
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[7];
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\4符3.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
