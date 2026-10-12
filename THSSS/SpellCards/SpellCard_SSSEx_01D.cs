using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSSEx_01D : BaseSpellCard
	{
		public SpellCard_SSSEx_01D(StageDataPackage StageData)
			: base(StageData)
		{
			SC_Name = "迴梦「月色琉璃光」";
			BaseScore = 40000000L;
			Boss.DestPoint = new Point(BoundRect.Width / 2, 120);
			Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			new BulletTitle(StageData, SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRika_ct3");
			new BackgroundBossEx(StageData);
		}

		public override void Shoot()
		{
			Boss.Armon = Boss.ArmonArray[1];
			if (Time == 100)
			{
				var text = ".\\CS\\StEx\\关底Boss\\1符3.mbg";
				var cs_Data = StageData.LoadCS(text);
				new CSEmitterController(StageData, cs_Data);
			}
		}
	}
}
