using System.Drawing;
using SlimDX;

namespace Shooting
{
	internal class BackgroundPetal3D : BaseParticle3D
	{
		public BackgroundPetal3D(StageDataPackage StageData)
		{
			this.StageData = StageData;
			Background3D.BackgroundParticleList.Add(this);
		}

		public override void Ctrl()
		{
			Time++;
			Shoot();
		}

		public override void Shoot()
		{
			if (Time % 5 == 0)
			{
				for (var i = 0; i < 3; i++)
				{
					var backgroundParticle3D = new BackgroundParticle3D1(StageData, "Petal3" + Ran.Next(4).ToString(), new PointF((float)StageData.Ran.Next(280, BoundRect.Width + 160), (float)StageData.Ran.Next(-20, 0)), (float)StageData.Ran.Next(15, 25) / 10f, 1.5707963267948966 + (double)((float)StageData.Ran.Next(1, 8) / 10f), 200);
					backgroundParticle3D.Angle = (double)((float)StageData.Ran.Next(0, 360) * 3.1415927f / 180f);
					backgroundParticle3D.AngularVelocity3D = (float)StageData.Ran.Next(10) / 100f;
					backgroundParticle3D.Scale = (float)StageData.Ran.Next(35, 50) / 100f;
					backgroundParticle3D.RotatingAxis = new Vector3((float)StageData.Ran.Next(-100, 100), (float)StageData.Ran.Next(-100, 100), (float)StageData.Ran.Next(-100, 100));
					backgroundParticle3D.MaxTransparent = 200;
					backgroundParticle3D.Depth = (float)Ran.Next(0, 30);
				}
			}
		}

		public override void Show()
		{
		}
	}
}
