using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001E5 RID: 485
	internal class EnemyPlane_TouhouMaoYu : BaseEnemyPlane_Touhou
	{
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0006E0F4 File Offset: 0x0006C2F4
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x0006E10B File Offset: 0x0006C30B
		public bool BackFire { get; set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x0006E114 File Offset: 0x0006C314
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x0006E12B File Offset: 0x0006C32B
		public Color BackFireColor { get; set; }

		// Token: 0x06000B21 RID: 2849 RVA: 0x0006E134 File Offset: 0x0006C334
		public EnemyPlane_TouhouMaoYu(StageDataPackage StageData, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: base(StageData, null, OriginalPosition, Velocity, Direction, ColorType)
		{
			base.HealthPoint = 15f;
			base.Region = 6;
			base.TxtureObject = base.TextureObjectDictionary["EnemyMaoYu0" + ColorType];
			base.ItemCount = 1;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0006E191 File Offset: 0x0006C391
		public override void Shoot()
		{
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0006E194 File Offset: 0x0006C394
		public override void TextureCtrl()
		{
			base.AngleDegree += 15.0;
		}
	}
}
