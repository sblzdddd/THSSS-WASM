using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200012E RID: 302
	internal class MusicTitle : BaseEffect
	{
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x0004952C File Offset: 0x0004772C
		public new PointF Position
		{
			get
			{
				return new PointF(base.OriginalPosition.X + 36f, base.OriginalPosition.Y + 16f);
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0004956C File Offset: 0x0004776C
		public MusicTitle(StageDataPackage StageData, string textureName)
			: this(StageData, textureName, new Point(0, 0))
		{
			base.DestPoint = new Point(base.BoundRect.Width, base.BoundRect.Height - 16);
			base.Scale = 0.5f;
			base.OriginalPosition = new PointF((float)base.BoundRect.Width, (float)(base.BoundRect.Height + 100));
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x000495F4 File Offset: 0x000477F4
		public MusicTitle(StageDataPackage StageData, string textureName, Point DestPoint)
			: base(StageData)
		{
			base.TxtureObject = base.TextureObjectDictionary[textureName];
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width - 10), 350f);
			base.Velocity = 4f;
			base.Direction = 1.5707963267948966;
			base.Scale = 2f;
			base.DestPoint = DestPoint;
			base.LifeTime = 240;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00049684 File Offset: 0x00047884
		public override void Move()
		{
			float num = (float)base.GetDistance(base.DestPoint);
			if (num > 50f)
			{
				base.Velocity = 7f;
			}
			else if (num > 35f)
			{
				base.Velocity = 5f;
			}
			else if (num > 15f)
			{
				base.Velocity = 3f;
			}
			else if (num > 1f)
			{
				base.Velocity = 1f;
			}
			else
			{
				base.Velocity = 0f;
			}
			base.Direction = base.GetDirection(base.DestPoint);
			base.Angle = (double)(-(double)((float)(base.Direction - 1.5707963267948966)));
			base.Move();
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00049760 File Offset: 0x00047960
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Scale > 1f)
			{
				base.Scale -= 0.03f;
			}
			if (base.Time >= base.LifeTime - 35)
			{
				base.MaxTransparent -= 8;
			}
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x000497C4 File Offset: 0x000479C4
		public override bool OutBoundary()
		{
			return base.Time > base.LifeTime;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x000497F4 File Offset: 0x000479F4
		public override void Show()
		{
			SizeF sizeF = new SizeF((float)base.TxtureObject.Width * base.Scale, (float)base.TxtureObject.Height * base.Scale);
			base.SpriteMain.Draw2D(base.TxtureObject.TXTure, base.TxtureObject.PosRect, sizeF, base.TxtureObject.RightTop, 0f, this.Position, Color.FromArgb(base.TransparentValue, Color.White));
		}
	}
}
