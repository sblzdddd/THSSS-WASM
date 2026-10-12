using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSSEx_08 : BaseSpellCard
	{
		public SpellCard_SSSEx_08(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "仙境「风花雪月」";
			base.BaseScore = 40000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new BackgroundBossEx(StageData);
			string name = base.MyPlane.Name;
			if (name != null)
			{
				if (name == "Reimu")
				{
					new FullPic2(StageData, "FaceRika_ct0");
					return;
				}
				if (name == "Marisa")
				{
					new FullPic2(StageData, "FaceRika_ct1");
					return;
				}
				if (name == "Sanae")
				{
					new FullPic2(StageData, "FaceRika_ct2");
					return;
				}
				if (name == "Koishi")
				{
					new FullPic2(StageData, "FaceRika_ct3");
					return;
				}
			}
			new FullPic2(StageData, "FaceRika_ct0");
		}

		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[15];
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\关底Boss\\8符.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
