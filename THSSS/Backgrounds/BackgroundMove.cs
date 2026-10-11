using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200015F RID: 351
	public class BackgroundMove : BaseObject_CS
	{
		// Token: 0x060008DD RID: 2269 RVA: 0x0005493C File Offset: 0x00052B3C
		public BackgroundMove(StageDataPackage StageData, string textureName)
			: base(StageData, textureName, new PointF(0f, 0f), 0f, 0.0)
		{
			base.Background.BackgroundList.Add(this);
			base.OriginalPosition = new PointF((float)(base.BoundRect.Height / 2), (float)(base.BoundRect.Height / 2));
			base.Scale = (float)base.BoundRect.Height / (float)base.TxtureObject.Height;
			base.Velocity = 2f;
			base.DirectionDegree = 225.0;
			base.OutsideRegion = 1000;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x000549FC File Offset: 0x00052BFC
		public override void Ctrl()
		{
			base.AngleDegree = -base.DirectionDegree + 90.0;
			base.Ctrl();
			float num = base.OriginalPosition.X;
			float num2 = base.OriginalPosition.Y;
			if (base.OriginalPosition.X <= (float)(-(float)base.BoundRect.Height / 2))
			{
				num = base.OriginalPosition.X + (float)base.BoundRect.Height;
			}
			else if (base.OriginalPosition.X >= (float)(base.BoundRect.Height / 2))
			{
				num = base.OriginalPosition.X - (float)base.BoundRect.Height;
			}
			if (base.OriginalPosition.Y <= (float)(-(float)base.BoundRect.Height / 2))
			{
				num2 = base.OriginalPosition.Y + (float)base.BoundRect.Height;
			}
			else if (base.OriginalPosition.Y >= (float)(base.BoundRect.Height / 2))
			{
				num2 = base.OriginalPosition.Y - (float)base.BoundRect.Height;
			}
			base.OriginalPosition = new PointF(num, num2);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00054B7C File Offset: 0x00052D7C
		public override void Show()
		{
			float num = base.OriginalPosition.X;
			float num2 = base.OriginalPosition.Y;
			base.Show();
			num += (float)base.BoundRect.Height;
			base.OriginalPosition = new PointF(num, num2);
			base.Show();
			num2 += (float)base.BoundRect.Height;
			base.OriginalPosition = new PointF(num, num2);
			base.Show();
			num -= (float)base.BoundRect.Height;
			base.OriginalPosition = new PointF(num, num2);
			base.Show();
			num2 -= (float)base.BoundRect.Height;
			base.OriginalPosition = new PointF(num, num2);
		}
	}
}
