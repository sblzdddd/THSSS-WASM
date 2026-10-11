using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200000A RID: 10
	public class BaseSubPlane : BaseObject
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000058D8 File Offset: 0x00003AD8
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x000058EF File Offset: 0x00003AEF
		public double ShootDirection { get; set; }

		// Token: 0x060000F3 RID: 243 RVA: 0x000058F8 File Offset: 0x00003AF8
		public BaseSubPlane(StageDataPackage StageData, string textureName, PointF Position)
			: base(StageData, textureName, Position, 0f, 1.5707963267948966)
		{
			base.TransparentValueF = 0f;
			base.TransparentVelocity = 20f;
			base.AngleWithDirection = false;
			base.AngleDegree = 90.0;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000594F File Offset: 0x00003B4F
		public virtual void Refresh()
		{
			base.Time = 0;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000595C File Offset: 0x00003B5C
		public override void Ctrl()
		{
			base.Ctrl();
			PointF destPoint = base.DestPoint;
			bool flag = 1 == 0;
			this.MoveToPoint(base.DestPoint);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000598C File Offset: 0x00003B8C
		public void MoveToPoint(PointF DP)
		{
			float num = (float)base.GetDistance(DP);
			if (num > 2f)
			{
				base.Velocity = num / 2.5f;
			}
			else if (num > 0f)
			{
				base.Velocity = 0.5f;
			}
			else
			{
				base.Velocity = 0f;
			}
			base.Direction = base.GetDirection(DP);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005A00 File Offset: 0x00003C00
		public override void Shoot()
		{
			BulletGuidance bulletGuidance = new BulletGuidance(base.StageData, "子机子弹", base.Position, 8f, -1.5707963267948966);
			bulletGuidance.TransparentValueF = 150f;
			bulletGuidance.Angle = 3.1415927410125732;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005A54 File Offset: 0x00003C54
		public override void Show()
		{
			if (base.Enabled)
			{
				base.Show();
			}
		}
	}
}
