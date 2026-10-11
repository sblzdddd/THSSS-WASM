using System;
using System.Drawing;
using SlimDX;

namespace Shooting
{
	// Token: 0x02000092 RID: 146
	public class EDPetal : BaseParticle3D
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x000233B4 File Offset: 0x000215B4
		// (set) Token: 0x0600049E RID: 1182 RVA: 0x000233CB File Offset: 0x000215CB
		public double DestAngleDegree { get; set; }

		// Token: 0x0600049F RID: 1183 RVA: 0x000233D4 File Offset: 0x000215D4
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

		// Token: 0x060004A0 RID: 1184 RVA: 0x00023454 File Offset: 0x00021654
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
