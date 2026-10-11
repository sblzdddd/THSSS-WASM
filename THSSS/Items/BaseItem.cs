using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000016 RID: 22
	public class BaseItem : BaseObject
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00007D10 File Offset: 0x00005F10
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00007D27 File Offset: 0x00005F27
		public float AngularVelocity { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00007D30 File Offset: 0x00005F30
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00007D47 File Offset: 0x00005F47
		public bool Obtain { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00007D50 File Offset: 0x00005F50
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00007D67 File Offset: 0x00005F67
		public bool Doubled { get; set; }

		// Token: 0x06000138 RID: 312 RVA: 0x00007D70 File Offset: 0x00005F70
		public BaseItem(StageDataPackage StageData, string textureName, PointF Position)
			: base(StageData, textureName, Position, 0f, 0.0)
		{
			base.ItemList.Add(this);
			base.Velocity = (float)base.Ran.Next(1, 6);
			base.Direction = (double)((float)base.Ran.Next(180, 360) / 360f) * 3.141592653589793 * 2.0;
			base.Angle = 0.0;
			if ((float)base.Direction - 1.5707964f > 0f)
			{
				this.AngularVelocity = -0.2f;
			}
			else
			{
				this.AngularVelocity = 0.2f;
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007E3C File Offset: 0x0000603C
		public override void HitCheckAll()
		{
			if (this.HitCheck(base.MyPlane, 200f))
			{
				this.Obtain = true;
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007E6C File Offset: 0x0000606C
		public override void Ctrl()
		{
			base.Ctrl();
			base.Angle += (double)this.AngularVelocity;
			if (base.Time == 12)
			{
				base.Direction = 1.5707963267948966;
				this.AngularVelocity = 0f;
				base.Angle = 0.0;
			}
			else if (base.Time > 12)
			{
				if (this.Obtain)
				{
					base.Direction = 3.141592653589793 + Math.Atan2((double)(base.Position.Y - base.MyPlane.Position.Y), (double)(base.Position.X - base.MyPlane.Position.X));
					base.Velocity = 8f;
				}
				else
				{
					base.Velocity = 4f;
				}
			}
			if (this.OutBoundary())
			{
				base.ItemList.Remove(this);
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007F94 File Offset: 0x00006194
		public override bool OutBoundary()
		{
			if (base.OriginalPosition.X < 0f)
			{
				base.OriginalPosition = new PointF(0f, base.OriginalPosition.Y);
			}
			else if (base.OriginalPosition.X > (float)base.BoundRect.Width)
			{
				base.OriginalPosition = new PointF((float)base.BoundRect.Width, base.OriginalPosition.Y);
			}
			return base.Time >= 30 && base.Position.Y > (float)(base.BoundRect.Bottom + 30);
		}
	}
}
