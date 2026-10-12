using System.Drawing;
using SlimDX;

namespace Shooting
{
	internal class BackgroundLeaf3D : BaseParticle3D
	{
		public BackgroundLeaf3D(StageDataPackage StageData)
		{
			base.StageData = StageData;
			StageData.Particle3D.ParticleList.Add(this);
		}

		public override void Ctrl()
		{
			base.Time++;
			this.Shoot();
		}

		public override void Shoot()
		{
			if (base.Time % 20 == 0)
			{
				for (int i = 0; i < 2; i++)
				{
					BackgroundParticle3D1 backgroundParticle3D = new BackgroundParticle3D1(base.StageData, "Leaf", new PointF((float)base.StageData.Ran.Next(130, 700), (float)base.StageData.Ran.Next(-50, -20)), (float)base.StageData.Ran.Next(2, 4), 1.5707963267948966 + (double)((float)base.StageData.Ran.Next(1, 8) / 10f), 200);
					backgroundParticle3D.Angle = (double)((float)base.StageData.Ran.Next(0, 360) * 3.1415927f / 180f);
					backgroundParticle3D.AngularVelocity3D = (float)base.StageData.Ran.Next(5) / 100f;
					backgroundParticle3D.Scale = (float)base.StageData.Ran.Next(5, 20) / 10f;
					backgroundParticle3D.RotatingAxis = new Vector3((float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100));
					backgroundParticle3D.ColorValue = Color.FromArgb(base.StageData.Ran.Next(150, 250), 255, base.StageData.Ran.Next(150, 255));
				}
			}
		}

		public override void Show()
		{
		}
	}
}
