using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000022 RID: 34
	internal class ParticleBossFire : BaseObject
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x00009DB4 File Offset: 0x00007FB4
		public ParticleBossFire(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int LifeTime)
		{
			base.StageData = StageData;
			base.Velocity = Velocity;
			base.Direction = Direction;
			base.OriginalPosition = OriginalPosition;
			base.LifeTime = LifeTime;
			if (textureName != null)
			{
				base.TxtureObject = base.TextureObjectDictionary[textureName];
			}
			base.Active = true;
			base.Background.BackgroundList.Add(this);
			base.ScaleLength = 0.5f;
			base.ScaleWidth = 0.3f;
			base.DirectionDegree = (double)(90 + base.Ran.Next(-4, 5));
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00009E60 File Offset: 0x00008060
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
			if (base.Time > 0)
			{
				base.ScaleLength += 0.07f;
				base.ScaleWidth += 0.03f;
				base.TransparentValueF -= (float)(base.MaxTransparent / base.LifeTime);
			}
			if (this.OutBoundary())
			{
				base.Background.BackgroundList.Remove(this);
			}
		}
	}
}
