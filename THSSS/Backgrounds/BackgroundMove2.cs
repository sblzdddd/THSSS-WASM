using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001B8 RID: 440
	public class BackgroundMove2 : BaseObject_CS
	{
		// Token: 0x06000A4E RID: 2638 RVA: 0x00062DBC File Offset: 0x00060FBC
		public BackgroundMove2(StageDataPackage StageData, string textureName)
			: base(StageData, textureName, new PointF(0f, 0f), 0f, 0.0)
		{
			base.Background.BackgroundList.Add(this);
			base.OriginalPosition = new PointF((float)(base.BoundRect.Height / 2), (float)(base.BoundRect.Height / 2));
			this.Length = base.BoundRect.Height * 2;
			base.Velocity = 2f;
			base.DirectionDegree = 225.0;
			base.OutsideRegion = 10000;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00062E70 File Offset: 0x00061070
		public override void Ctrl()
		{
			base.Scale = (float)this.Length / (float)base.TxtureObject.Height;
			this.Width = (float)base.TxtureObject.Width * base.Scale;
			base.AngleDegree = -base.DirectionDegree + 90.0;
			base.Ctrl();
			float num = base.OriginalPosition.X;
			float num2 = base.OriginalPosition.Y;
			if (base.OriginalPosition.X <= -this.Width / 2f)
			{
				num = base.OriginalPosition.X + this.Width;
			}
			else if (base.OriginalPosition.X >= this.Width / 2f)
			{
				num = base.OriginalPosition.X - this.Width;
			}
			if (base.OriginalPosition.Y <= (float)(-(float)this.Length / 2))
			{
				num2 = base.OriginalPosition.Y + (float)this.Length;
			}
			else if (base.OriginalPosition.Y >= (float)(this.Length / 2))
			{
				num2 = base.OriginalPosition.Y - (float)this.Length;
			}
			base.OriginalPosition = new PointF(num, num2);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00062FE0 File Offset: 0x000611E0
		public override void Show()
		{
			float num = base.OriginalPosition.X;
			float num2 = base.OriginalPosition.Y;
			base.Show();
			num += this.Width;
			base.OriginalPosition = new PointF(num, num2);
			base.Show();
			num2 += (float)this.Length;
			base.OriginalPosition = new PointF(num, num2);
			base.Show();
			num -= this.Width;
			base.OriginalPosition = new PointF(num, num2);
			base.Show();
			num2 -= (float)this.Length;
			base.OriginalPosition = new PointF(num, num2);
		}

		// Token: 0x040003D7 RID: 983
		public int Length;

		// Token: 0x040003D8 RID: 984
		private float Width;
	}
}
