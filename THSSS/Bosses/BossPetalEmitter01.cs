using System;
using System.Drawing;
using SlimDX;

namespace Shooting
{
	public class BossPetalEmitter01 : BaseEffect
	{
		public BossPetalEmitter01(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			base.OriginalPosition = OriginalPosition;
			base.LifeTime = 130;
		}

		public override void Shoot()
		{
			if (base.Time % 1 == 0)
			{
				for (int i = 0; i < 2; i++)
				{
					Particle3D3 particle3D = new Particle3D3(base.StageData, "Petal30", new PointF((float)base.StageData.Ran.Next((int)base.OriginalPosition.X - 20, (int)base.OriginalPosition.X + 20), (float)base.StageData.Ran.Next((int)base.OriginalPosition.Y - 20, (int)base.OriginalPosition.Y + 20)), (float)base.StageData.Ran.Next(10, 30) / 10f, base.Direction, 100 + base.Ran.Next(60));
					particle3D.Angle = (double)((float)base.StageData.Ran.Next(0, 360) * 3.1415927f / 180f);
					particle3D.AngularVelocity3D = (float)base.StageData.Ran.Next(10) / 100f;
					particle3D.Scale = (float)base.StageData.Ran.Next(5, 8) / 10f;
					particle3D.RotatingAxis = new Vector3((float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100), (float)base.StageData.Ran.Next(-100, 100));
					particle3D.MaxTransparent = 255;
				}
			}
		}
	}
}
