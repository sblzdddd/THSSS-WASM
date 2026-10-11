using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001C9 RID: 457
	internal class EnemyPlane_TouhouYYY : BaseEnemyPlane_Touhou
	{
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0006624C File Offset: 0x0006444C
		// (set) Token: 0x06000A85 RID: 2693 RVA: 0x00066263 File Offset: 0x00064463
		public bool BackFire { get; set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0006626C File Offset: 0x0006446C
		// (set) Token: 0x06000A87 RID: 2695 RVA: 0x00066283 File Offset: 0x00064483
		public Color BackFireColor { get; set; }

		// Token: 0x06000A88 RID: 2696 RVA: 0x0006628C File Offset: 0x0006448C
		public EnemyPlane_TouhouYYY(StageDataPackage StageData, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: base(StageData, null, OriginalPosition, Velocity, Direction, ColorType)
		{
			base.HealthPoint = 30f;
			base.Region = 8;
			base.TxtureObject = base.TextureObjectDictionary["EnemyYYY" + ((int)(ColorType + 4)).ToString() + "_1"];
			base.AngularVelocityDegree = 5f;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x000662F7 File Offset: 0x000644F7
		public override void Shoot()
		{
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x000662FA File Offset: 0x000644FA
		public override void TextureCtrl()
		{
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x000662FD File Offset: 0x000644FD
		public override void GiveEndEffect()
		{
			base.GiveEndEffect();
			base.EffectList.Remove(this.circle0);
			base.EffectList.Remove(this.circle1);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0006632C File Offset: 0x0006452C
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == 1)
			{
				this.circle0 = new BaseEffect(base.StageData, "EnemyYYY" + ((int)(base.ColorType + 4)).ToString() + "_0", base.Position, 0f, 0.0)
				{
					Active = false,
					AngularVelocityDegree = -5f,
					AngleDegree = 90.0
				};
				this.circle1 = new BaseEffect(base.StageData, "EnemyYYY" + ((int)(base.ColorType + 4)).ToString() + "_1", base.Position, 0f, 0.0)
				{
					Scale = 1.5f,
					AngularVelocityDegree = -10f
				};
				this.circle0.SetBinding(this);
				this.circle1.SetBinding(this);
			}
			if (this.OutBoundary())
			{
				base.EffectList.Remove(this.circle0);
				base.EffectList.Remove(this.circle1);
			}
		}

		// Token: 0x040003E4 RID: 996
		private BaseEffect circle0;

		// Token: 0x040003E5 RID: 997
		private BaseEffect circle1;
	}
}
