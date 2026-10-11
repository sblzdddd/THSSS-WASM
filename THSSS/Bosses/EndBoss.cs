using System;
using System.Drawing;

namespace Shooting
{
	internal class EndBoss : BaseEffect
	{
		public EndBoss(StageDataPackage StageData)
			: base(StageData)
		{
			base.LifeTime = 200;
		}

		public override void Move()
		{
			if (base.Boss != null)
			{
				base.Position = base.Boss.Position;
			}
		}

		public override void Shoot()
		{
			if (base.Time == 1)
			{
				base.StageData.SoundPlay("se_enep01.wav");
				for (int i = 0; i < 16; i++)
				{
					ParticleSmaller particleSmaller = new ParticleSmaller(base.StageData, "光点", base.Position, 2f + (float)base.Ran.Next(30) / 10f, (double)((float)base.Ran.Next(360) / 180f) * 3.141592653589793);
					particleSmaller.Scale = (float)base.Ran.Next(10, 20) / 10f;
					particleSmaller.ColorValue = Color.FromArgb(255, base.Ran.Next(20, 240), base.Ran.Next(200, 255));
				}
				base.StageData.VibrateStart(50);
			}
			else if (base.Time == 100)
			{
				base.StageData.SoundPlay("se_enep01.wav");
				for (int i = 0; i < 60; i++)
				{
					ParticleSmaller particleSmaller2 = new ParticleSmaller(base.StageData, "光点", base.Position, 1f + (float)base.Ran.Next(50) / 10f, (double)((float)base.Ran.Next(360) / 180f) * 3.141592653589793);
					particleSmaller2.Scale = (float)base.Ran.Next(10, 20) / 10f;
					particleSmaller2.ColorValue = Color.FromArgb(255, base.Ran.Next(20, 240), base.Ran.Next(220, 255));
				}
				base.StageData.VibrateStart(50);
				base.MyBulletList.Clear();
				base.Boss = null;
			}
			else if (base.Time == 250)
			{
				new EndStage(base.StageData, "在世界的某个角落", true);
				new TransitionOut(base.StageData);
				try
				{
					INI_RW ini_RW = new INI_RW(".\\Setting.INI");
					if (ini_RW.ExistINIFile())
					{
						ini_RW.IniWriteValue("Mode", "Clear", "yes");
					}
				}
				catch
				{
				}
			}
		}

		public override void Show()
		{
		}
	}
}
