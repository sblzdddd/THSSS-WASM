using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200020A RID: 522
	internal class BackgroundParticleEmitter : Background3DObject
	{
		// Token: 0x06000B9B RID: 2971 RVA: 0x0007352A File Offset: 0x0007172A
		public BackgroundParticleEmitter(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, OriginalPosition, true)
		{
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0007353C File Offset: 0x0007173C
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time % 30 == 0)
			{
				PointF pointF = new PointF(base.OriginalPosition.X + (float)base.Ran.Next(-20, 20), base.OriginalPosition.Y + (float)base.Ran.Next(-20, 20));
				Background3DObject background3DObject = new Background3DObject(base.StageData, "591-1", base.OriginalPosition, false);
				background3DObject.Scale = (float)base.Ran.Next(40, 60) / 100f;
				background3DObject.ScaleVelocity = -0.004f;
				background3DObject.Velocity = (float)base.Ran.Next(-10, 10) / 20f;
				background3DObject.DirectionDegree = (double)base.Ran.Next(360);
				background3DObject.VelocityZ = (float)base.Ran.Next(6, 15) / 20f;
				background3DObject.LifeTime = 200;
				background3DObject.Active = true;
				background3DObject.Angle3DX = -1.5707964f + base.StageData.Background3D.Envi.CameraAngle;
				background3DObject.TransparentValueF = 0f;
				background3DObject.TransparentVelocity = 10f;
			}
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00073691 File Offset: 0x00071891
		public override void Show()
		{
		}
	}
}
