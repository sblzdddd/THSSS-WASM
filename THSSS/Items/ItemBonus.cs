using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200004C RID: 76
	public class ItemBonus : BaseEffect
	{
		// Token: 0x0600027F RID: 639 RVA: 0x000105FC File Offset: 0x0000E7FC
		public ItemBonus(StageDataPackage StageData)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2));
			base.LifeTime = 72;
			base.TransparentValueF = 0f;
			base.Accelerate = 0.1f;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0001067D File Offset: 0x0000E87D
		public override void Ctrl()
		{
			base.Ctrl();
			base.Direction = base.GetDirection(base.MyPlane);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0001069C File Offset: 0x0000E89C
		public override void Shoot()
		{
			int num = 160 - base.Time;
			int num2 = 10;
			for (int i = 0; i < num2; i++)
			{
				PointF pointF = new PointF(base.OriginalPosition.X + (float)num * (float)Math.Sin(base.Angle) + (float)base.Ran.Next(-15, 15), base.OriginalPosition.Y + (float)num * (float)Math.Cos(base.Angle) + (float)base.Ran.Next(-15, 15));
				new StarItem_Touhou(base.StageData, pointF);
				pointF = new PointF(base.OriginalPosition.X - (float)num * (float)Math.Sin(base.Angle) + (float)base.Ran.Next(-15, 15), base.OriginalPosition.Y - (float)num * (float)Math.Cos(base.Angle) + (float)base.Ran.Next(-15, 15));
				new StarItem_Touhou(base.StageData, pointF);
			}
			base.Angle += 0.30000001192092896;
		}
	}
}
