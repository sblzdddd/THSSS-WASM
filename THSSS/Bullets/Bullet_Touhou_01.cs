using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000027 RID: 39
	public class Bullet_Touhou_01 : BaseBullet_Touhou
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000ABA0 File Offset: 0x00008DA0
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x0000ABB7 File Offset: 0x00008DB7
		public float Velocity0 { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000ABC0 File Offset: 0x00008DC0
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x0000ABD7 File Offset: 0x00008DD7
		public float Velocity1 { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000ABE0 File Offset: 0x00008DE0
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x0000ABF7 File Offset: 0x00008DF7
		public int Time1 { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000AC00 File Offset: 0x00008E00
		// (set) Token: 0x060001CB RID: 459 RVA: 0x0000AC17 File Offset: 0x00008E17
		public float Velocity2 { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000AC20 File Offset: 0x00008E20
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0000AC37 File Offset: 0x00008E37
		public int Time2 { get; set; }

		// Token: 0x060001CE RID: 462 RVA: 0x0000AC40 File Offset: 0x00008E40
		public Bullet_Touhou_01(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction, ColorType)
		{
			this.Velocity0 = Velocity;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000AC60 File Offset: 0x00008E60
		public override void Ctrl()
		{
			if (base.Time < this.Time1)
			{
				base.Accelerate = (this.Velocity1 - this.Velocity0) / (float)this.Time1;
			}
			else if (this.Time1 == base.Time)
			{
				base.Accelerate = 0f;
			}
			else if (this.Time1 < base.Time && base.Time < this.Time2)
			{
				base.Accelerate = (this.Velocity2 - this.Velocity1) / (float)(this.Time2 - this.Time1);
			}
			else if (this.Time2 == base.Time)
			{
				base.Accelerate = 0f;
			}
			base.Ctrl();
		}
	}
}
