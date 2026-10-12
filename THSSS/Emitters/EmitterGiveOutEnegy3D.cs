using System.Drawing;
using SlimDX;

namespace Shooting
{
	internal class EmitterGiveOutEnegy3D : BaseEffect
	{
		public EmitterGiveOutEnegy3D(StageDataPackage StageData, PointF OriginalPosition, Color color)
			: base(StageData)
		{
			this.OriginalPosition = OriginalPosition;
			Velocity = 0f;
			Direction = 0.0;
			LifeTime = 25;
			ColorValue = color;
			BaseEffect baseEffect = new BaseEffect(StageData, "Effect-2", Position, 0f, 0.0);
			baseEffect.LifeTime = 100;
			baseEffect.Scale = 1f;
			baseEffect.ScaleVelocity = 0.8f;
			baseEffect.ColorValue = color;
			baseEffect.TransparentValueF = 180f;
			baseEffect.TransparentVelocity = -2f;
		}

		public override void Shoot()
		{
			if (Time == 1)
			{
				for (int i = 0; i < 50; i++)
				{
					Particle3D1 particle3D = new Particle3D1(StageData, "Effect-1", OriginalPosition, (float)Ran.Next(50, 130) / 10f, (double)((float)Ran.Next(360) / 180f * 3.1415927f), 30 + StageData.Ran.Next(50));
					particle3D.Direction3D = (double)((float)StageData.Ran.Next(0, 60) * 3.1415927f / 180f);
					particle3D.Angle = (double)((float)StageData.Ran.Next(0, 360) * 3.1415927f / 180f);
					particle3D.AngularVelocity3D = (float)StageData.Ran.Next(10, 18) / 100f;
					particle3D.Scale = (float)StageData.Ran.Next(20, 30) / 10f;
					particle3D.RotatingAxis = new Vector3((float)StageData.Ran.Next(-100, 100), (float)StageData.Ran.Next(-100, 100), (float)StageData.Ran.Next(-100, 100));
					particle3D.ColorValue = ColorValue;
				}
			}
		}

		public override void Show()
		{
		}
	}
}
