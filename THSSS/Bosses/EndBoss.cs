using System.Drawing;

namespace Shooting
{
	internal class EndBoss : BaseEffect
	{
		public EndBoss(StageDataPackage StageData)
			: base(StageData)
		{
			LifeTime = 200;
		}

		public override void Move()
		{
			if (Boss != null)
			{
				Position = Boss.Position;
			}
		}

		public override void Shoot()
		{
			if (Time == 1)
			{
				StageData.SoundPlay("se_enep01.wav");
				for (int i = 0; i < 16; i++)
				{
					ParticleSmaller particleSmaller = new ParticleSmaller(StageData, "光点", Position, 2f + (float)Ran.Next(30) / 10f, (double)((float)Ran.Next(360) / 180f) * 3.141592653589793);
					particleSmaller.Scale = (float)Ran.Next(10, 20) / 10f;
					particleSmaller.ColorValue = Color.FromArgb(255, Ran.Next(20, 240), Ran.Next(200, 255));
				}
				StageData.VibrateStart(50);
			}
			else if (Time == 100)
			{
				StageData.SoundPlay("se_enep01.wav");
				for (int i = 0; i < 60; i++)
				{
					ParticleSmaller particleSmaller2 = new ParticleSmaller(StageData, "光点", Position, 1f + (float)Ran.Next(50) / 10f, (double)((float)Ran.Next(360) / 180f) * 3.141592653589793);
					particleSmaller2.Scale = (float)Ran.Next(10, 20) / 10f;
					particleSmaller2.ColorValue = Color.FromArgb(255, Ran.Next(20, 240), Ran.Next(220, 255));
				}
				StageData.VibrateStart(50);
				MyBulletList.Clear();
				Boss = null;
			}
			else if (Time == 250)
			{
				new EndStage(StageData, "在世界的某个角落", true);
				new TransitionOut(StageData);
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
