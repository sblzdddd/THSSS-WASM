using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000D8 RID: 216
	internal class BulletLaser : BaseBullet
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00035128 File Offset: 0x00033328
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x0003513F File Offset: 0x0003333F
		public float DesScaleWidth { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00035148 File Offset: 0x00033348
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x0003515F File Offset: 0x0003335F
		public float DesScaleLength { get; set; }

		// Token: 0x060005FD RID: 1533 RVA: 0x00035168 File Offset: 0x00033368
		public BulletLaser(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			base.ScaleLength = 0.1f;
			this.DesScaleLength = 3f;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00035194 File Offset: 0x00033394
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.ScaleLength <= this.DesScaleLength)
			{
				base.ScaleLength += 0.02f;
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000351D0 File Offset: 0x000333D0
		public override bool HitCheck(BaseObject MyPlane)
		{
			double distance = base.GetDistance(MyPlane);
			double direction = base.GetDirection(MyPlane);
			double num = distance * Math.Cos((double)((float)(base.Direction - direction)));
			double num2 = distance * Math.Sin((double)((float)(base.Direction - direction)));
			double num3 = (double)((float)base.Region * base.ScaleWidth);
			double num4 = (double)((float)base.TxtureObject.Height * base.ScaleLength * 0.45f);
			return num2 * num2 / num3 / num3 + (num - (double)((float)base.TxtureObject.Height * base.ScaleLength / 2f)) * (num - (double)((float)base.TxtureObject.Height * base.ScaleLength / 2f)) / num4 / num4 < 1.0;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000352AA File Offset: 0x000334AA
		public override void GiveItems()
		{
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x000352B0 File Offset: 0x000334B0
		public override void Show()
		{
			if (base.TxtureObject != null)
			{
				SizeF sizeF = new SizeF((float)base.TxtureObject.Width * base.ScaleWidth, (float)base.TxtureObject.Height * base.ScaleLength);
				base.SpriteMain.Draw2D(base.TxtureObject.TXTure, base.TxtureObject.PosRect, sizeF, new PointF((float)(base.TxtureObject.Width / 2), 0f), (float)(base.Direction - 1.5707963267948966 + base.Angle), base.Position, Color.FromArgb(base.TransparentValue, base.ColorValue));
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0003536C File Offset: 0x0003356C
		public override void ShowRegion()
		{
			PointF originalPosition = base.OriginalPosition;
			base.OriginalPosition = new PointF(base.OriginalPosition.X + (float)base.TxtureObject.Height * base.ScaleLength * 0.5f * (float)Math.Cos(base.Direction), base.OriginalPosition.Y + (float)base.TxtureObject.Height * base.ScaleLength * 0.5f * (float)Math.Sin(base.Direction));
			base.SpriteMain.Draw2D(base.TextureObjectDictionary["Region"], 2f * (float)base.Region / (float)base.TextureObjectDictionary["Region"].Width * base.ScaleWidth, (float)base.TxtureObject.Height * 0.9f / (float)base.TextureObjectDictionary["Region"].Height * base.ScaleLength, (float)(base.Direction - 1.5707963267948966 + base.Angle), base.Position, Color.White, base.Mirrored);
			base.OriginalPosition = originalPosition;
		}
	}
}
