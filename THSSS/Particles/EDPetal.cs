using System;
using System.Drawing;
using SlimDX;

namespace Shooting
{
	public class EDPetal : BaseParticle3D
	{
		public double DestAngleDegree { get; set; }

		public EDPetal(StageDataPackage StageData, string modelName, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, modelName, OriginalPosition, 0f, 1.5707963267948966)
		{
			base.OriginalPosition = OriginalPosition;
			base.LifeTime = 300;
			base.TransparentValueF = 0f;
			base.TransparentVelocity = 10f;
			base.Angle3D = 1.5707964f;
			base.Depth = 1f;
			base.Background3D.BackgroundParticleList.Add(this);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Angle3D > 0f)
			{
				base.Angle3D -= 0.034906585f;
			}
			if (this.DestAngleDegree - base.AngleDegree > 1.0)
			{
				base.AngleDegree += (this.DestAngleDegree - base.AngleDegree) / 8.0;
			}
			else
			{
				base.AngleDegree = this.DestAngleDegree;
			}
			base.RotatingAxis = new Vector3((float)Math.Cos(base.Angle), (float)Math.Sin(base.Angle), 0f);
			if (base.Time == base.LifeTime - 22)
			{
				base.TransparentVelocity = -12f;
			}
		}
	}
}
