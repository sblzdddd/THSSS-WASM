using System;
using System.Drawing;

namespace Shooting
{
	internal class EndBoss_Touhou06 : EndBoss_Touhou
	{
		public EndBoss_Touhou06(StageDataPackage StageData, PointF OriginalPosition, Color Color1, Color Color2)
			: base(StageData, OriginalPosition, Color1, Color2)
		{
			StageData.SlowMode = 300;
			if (base.Boss is Boss_Tensei02)
			{
				if (base.Boss.HealthPoint <= 0f)
				{
					base.LifeTime = 95;
				}
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == 98)
			{
				base.Boss.GiveItems();
				BaseParticle3D baseParticle3D = new BaseParticle3D(base.StageData, "夜空（亮）", new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2)), 0f, 1.5707963267948966);
				baseParticle3D.Depth = -10f;
				baseParticle3D.TransparentVelocity = 10f;
				baseParticle3D.TransparentValueF = 0f;
				base.Background3D.BackgroundParticleList.Add(baseParticle3D);
			}
			if (base.Time == base.LifeTime)
			{
				if (base.Boss is Boss_Tensei02 && base.Boss.HealthPoint <= 0f)
				{
					base.Boss.HealthPoint = (float)base.Boss.MaxHP;
					base.Boss.Life = 1;
					base.Boss.Time = 1;
					base.Boss.Region = 20;
					base.Boss.LifeTime = 9999999;
					base.StageData.SlowMode = 0;
				}
				else
				{
					EndStage endStage = new EndStage(base.StageData, "All", false, 100);
					endStage.LifeTime = 210;
					int num = base.MyPlane.Life * 20000000 + base.MyPlane.Spell * 5000000 + base.MyPlane.GreenPoint * 1000;
					new ClearBonus(base.StageData, 6000000 * ((int)base.Difficulty + 1) + num);
				}
			}
		}
	}
}
