using System.Drawing;
using SlimDX;

namespace Shooting
{
	public class BossPetalEmitter01 : BaseEffect
	{
		public BossPetalEmitter01(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			this.OriginalPosition = OriginalPosition;
			LifeTime = 130;
		}

		public override void Shoot()
		{
			if (Time % 1 == 0)
			{
				for (var i = 0; i < 2; i++)
				{
					var particle3D = new Particle3D3(StageData, "Petal30", new PointF((float)StageData.Ran.Next((int)OriginalPosition.X - 20, (int)OriginalPosition.X + 20), (float)StageData.Ran.Next((int)OriginalPosition.Y - 20, (int)OriginalPosition.Y + 20)), (float)StageData.Ran.Next(10, 30) / 10f, Direction, 100 + Ran.Next(60));
					particle3D.Angle = (double)((float)StageData.Ran.Next(0, 360) * 3.1415927f / 180f);
					particle3D.AngularVelocity3D = (float)StageData.Ran.Next(10) / 100f;
					particle3D.Scale = (float)StageData.Ran.Next(5, 8) / 10f;
					particle3D.RotatingAxis = new Vector3((float)StageData.Ran.Next(-100, 100), (float)StageData.Ran.Next(-100, 100), (float)StageData.Ran.Next(-100, 100));
					particle3D.MaxTransparent = 255;
				}
			}
		}
	}
}
