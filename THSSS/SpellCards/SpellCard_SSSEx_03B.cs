using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSSEx_03B : BaseSpellCard
	{
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
