using System.Drawing;

namespace Shooting
{
	internal class BackgroundParticleEmitter : Background3DObject
	{
		public BackgroundParticleEmitter(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, OriginalPosition, true)
		{
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time % 30 == 0)
			{
				PointF pointF = new PointF(OriginalPosition.X + (float)Ran.Next(-20, 20), OriginalPosition.Y + (float)Ran.Next(-20, 20));
				Background3DObject background3DObject = new Background3DObject(StageData, "591-1", OriginalPosition, false);
				background3DObject.Scale = (float)Ran.Next(40, 60) / 100f;
				background3DObject.ScaleVelocity = -0.004f;
				background3DObject.Velocity = (float)Ran.Next(-10, 10) / 20f;
				background3DObject.DirectionDegree = (double)Ran.Next(360);
				background3DObject.VelocityZ = (float)Ran.Next(6, 15) / 20f;
				background3DObject.LifeTime = 200;
				background3DObject.Active = true;
				background3DObject.Angle3DX = -1.5707964f + StageData.Background3D.Envi.CameraAngle;
				background3DObject.TransparentValueF = 0f;
				background3DObject.TransparentVelocity = 10f;
			}
		}

		public override void Show()
		{
		}
	}
}
