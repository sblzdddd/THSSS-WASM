using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000192 RID: 402
	internal class ParticleBossFire2 : BaseObject
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x0005BDA8 File Offset: 0x00059FA8
		// (set) Token: 0x0600099E RID: 2462 RVA: 0x0005BDBF File Offset: 0x00059FBF
		public double AngularVelocity { get; set; }

		// Token: 0x0600099F RID: 2463 RVA: 0x0005BDC8 File Offset: 0x00059FC8
		public ParticleBossFire2(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int LifeTime)
		{
			base.StageData = StageData;
			base.Velocity = Velocity;
			base.Direction = Direction;
			base.OriginalPosition = OriginalPosition;
			base.LifeTime = LifeTime;
			base.Scale = 0.01f;
			this.AngularVelocity = (double)base.Ran.Next(-2, 2) * 3.141592653589793 / 180.0;
			if (textureName != null)
			{
				base.TxtureObject = base.TextureObjectDictionary[textureName];
			}
			base.Active = true;
			base.Background.BackgroundList.Add(this);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0005BE78 File Offset: 0x0005A078
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time > 0)
			{
				base.TransparentValueF -= (float)(255 / base.LifeTime);
			}
			if (base.Boss != null)
			{
				base.OriginalPosition = base.Boss.OriginalPosition;
			}
			base.Direction += this.AngularVelocity;
			if (base.Time > 0)
			{
				base.Scale += 0.025f;
				base.TransparentValueF -= (float)(base.MaxTransparent / base.LifeTime);
			}
			if (this.OutBoundary())
			{
				base.Background.BackgroundList.Remove(this);
			}
		}
	}
}
