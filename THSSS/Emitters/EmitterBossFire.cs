using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000152 RID: 338
	internal class EmitterBossFire : BaseEffect
	{
		// Token: 0x060008AE RID: 2222 RVA: 0x00050B71 File Offset: 0x0004ED71
		public EmitterBossFire(StageDataPackage StageData, PointF OriginalPosition, Color ColorValue)
			: base(StageData, null, OriginalPosition, 0f, 0.0)
		{
			base.OriginalPosition = OriginalPosition;
			base.ColorValue = ColorValue;
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00050BA0 File Offset: 0x0004EDA0
		public override void Move()
		{
			if (base.Boss != null)
			{
				base.OriginalPosition = base.Boss.OriginalPosition;
			}
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00050BD0 File Offset: 0x0004EDD0
		public override void Shoot()
		{
			if (base.Time % 10 == 0)
			{
				ParticleBossFire particleBossFire = new ParticleBossFire(base.StageData, "Effect01_1", base.OriginalPosition, 0f, 0.0, 60);
				particleBossFire.ColorValue = base.ColorValue;
				ParticleBossFire2 particleBossFire2 = new ParticleBossFire2(base.StageData, "Effect01_2", base.OriginalPosition, 0f, (double)base.Ran.Next(360) * 3.141592653589793 / 180.0, 80);
				particleBossFire2.ColorValue = base.ColorValue;
				ParticleBossFire2 particleBossFire3 = new ParticleBossFire2(base.StageData, "Effect01_2", base.OriginalPosition, 0f, (double)base.Ran.Next(360) * 3.141592653589793 / 180.0, 100);
				particleBossFire3.ColorValue = base.ColorValue;
			}
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00050CD0 File Offset: 0x0004EED0
		public override bool OutBoundary()
		{
			return base.Boss == null;
		}
	}
}
