using System.Drawing;
using SlimDX;

namespace Shooting
{
	internal class BackgroundPetal3D3 : BaseParticle3D
	{
		public BackgroundPetal3D3(StageDataPackage StageData)
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
			if (Time % 4 == 0)
			{
				for (var i = 0; i < 3; i++)
				{
					new BackgroundParticle3D2(StageData, "Petal0" + Ran.Next(4).ToString(), new PointF((float)StageData.Ran.Next(0, BoundRect.Width), (float)StageData.Ran.Next(-150, (Time > 600) ? 100 : 300)), (float)StageData.Ran.Next(3, 6), 1.5707963267948966 + (double)((float)StageData.Ran.Next(-4, 3) / 10f), 250)
					{
						Angle = (double)((float)StageData.Ran.Next(0, 360) * 3.1415927f / 180f),
						AngularVelocity3D = (float)StageData.Ran.Next(10) / 100f,
						Scale = (float)StageData.Ran.Next(4, 6) / 10f,
						RotatingAxis = new Vector3((float)StageData.Ran.Next(-100, 100), (float)StageData.Ran.Next(-100, 100), (float)StageData.Ran.Next(-100, 100)),
						MaxTransparent = 160,
						Depth = (float)Ran.Next(-300, 0),
						MaxDepth = 1000f
					}.Direction3D = ((Time > 600) ? 0.5235987755982988 : 0.7853981633974483);
				}
			}
		}

		public override void Show()
		{
		}
	}
}
