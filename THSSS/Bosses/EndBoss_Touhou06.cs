using System.Drawing;

namespace Shooting
{
	internal class EndBoss_Touhou06 : EndBoss_Touhou
	{
		public EndBoss_Touhou06(StageDataPackage StageData, PointF OriginalPosition, Color Color1, Color Color2)
			: base(StageData, OriginalPosition, Color1, Color2)
		{
			StageData.SlowMode = 300;
			if (Boss is Boss_Tensei02)
			{
				if (Boss.HealthPoint <= 0f)
				{
					LifeTime = 95;
				}
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time == 98)
			{
				Boss.GiveItems();
				var baseParticle3D = new BaseParticle3D(StageData, "夜空（亮）", new PointF((float)(BoundRect.Width / 2), (float)(BoundRect.Height / 2)), 0f, 1.5707963267948966);
				baseParticle3D.Depth = -10f;
				baseParticle3D.TransparentVelocity = 10f;
				baseParticle3D.TransparentValueF = 0f;
				Background3D.BackgroundParticleList.Add(baseParticle3D);
			}
			if (Time == LifeTime)
			{
				if (Boss is Boss_Tensei02 && Boss.HealthPoint <= 0f)
				{
					Boss.HealthPoint = (float)Boss.MaxHP;
					Boss.Life = 1;
					Boss.Time = 1;
					Boss.Region = 20;
					Boss.LifeTime = 9999999;
					StageData.SlowMode = 0;
				}
				else
				{
					var endStage = new EndStage(StageData, "All", false, 100);
					endStage.LifeTime = 210;
					var num = MyPlane.Life * 20000000 + MyPlane.Spell * 5000000 + MyPlane.GreenPoint * 1000;
					new ClearBonus(StageData, 6000000 * ((int)Difficulty + 1) + num);
				}
			}
		}
	}
}
