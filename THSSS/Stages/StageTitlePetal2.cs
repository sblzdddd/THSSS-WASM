using System;
using System.Drawing;
using SlimDX;

namespace Shooting
{
	public class StageTitlePetal2 : BaseParticle3D
	{
		public StageTitlePetal2(StageDataPackage StageData)
		{
			this.StageData = StageData;
			Background3D.BackgroundParticleList.Add(this);
			OriginalPosition = new PointF((float)(BoundRect.Width / 2 - 45), 40f);
			Direction = 1.5707963267948966;
			Velocity = 3f;
			LifeTime = 80;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			Shoot();
			float num = 5f;
			Vx = 10f * (float)Math.Sin((double)((float)Time / num));
			Depth += 10f * (float)Math.Cos((double)((float)Time / num));
		}

		public override void Shoot()
		{
			if (Time % 1 == 0)
			{
				for (int i = 0; i < 8; i++)
				{
					Particle3D4 particle3D = new Particle3D4(StageData, "Petal00", new PointF((float)StageData.Ran.Next((int)OriginalPosition.X - 60, (int)OriginalPosition.X - 35), (float)((int)OriginalPosition.Y)), (float)StageData.Ran.Next(10, 30) / 10f, 1.5707963267948966, 100 + Ran.Next(40));
					particle3D.Angle = (double)((float)StageData.Ran.Next(0, 360) * 3.1415927f / 180f);
					particle3D.AngularVelocity3D = (float)StageData.Ran.Next(10) / 100f;
					particle3D.Scale = (float)StageData.Ran.Next(5, 8) / 10f;
					particle3D.RotatingAxis = new Vector3((float)StageData.Ran.Next(-100, 100), (float)StageData.Ran.Next(-100, 100), (float)StageData.Ran.Next(-100, 100));
					particle3D.MaxTransparent = 255;
					particle3D.Depth = (float)Ran.Next((int)Depth - 50, (int)Depth - 50);
				}
			}
		}

		public override void Show()
		{
		}
	}
}
