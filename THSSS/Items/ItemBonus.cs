using System;
using System.Drawing;

namespace Shooting
{
	public class ItemBonus : BaseEffect
	{
		public ItemBonus(StageDataPackage StageData)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2));
			base.LifeTime = 72;
			base.TransparentValueF = 0f;
			base.Accelerate = 0.1f;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			base.Direction = base.GetDirection(base.MyPlane);
		}

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
