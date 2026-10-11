using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000D1 RID: 209
	internal class PlaneSpark : BaseEffect
	{
		// Token: 0x060005C7 RID: 1479 RVA: 0x00033334 File Offset: 0x00031534
		public PlaneSpark(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			base.OriginalPosition = OriginalPosition;
			base.ScaleWidth = 0.05f;
			base.Active = true;
			base.LifeTime = 300;
			base.ColorValue = Color.FromArgb(85, 140, 255);
			base.UnRemoveable = true;
			base.Angle = 0.0;
			base.ScaleVL = 0.1f;
			base.ScaleVW = 0.1f;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000333C4 File Offset: 0x000315C4
		public override void Ctrl()
		{
			base.Ctrl();
			base.Region = (int)((float)base.TxtureObject.Width / 2f * 0.7f);
			if (base.Time < base.LifeTime - 50)
			{
				base.TransparentValueF = (float)(150 + base.Ran.Next(40));
			}
			else
			{
				base.TransparentValueF = (float)((base.LifeTime - base.Time) * 2 - 20 + base.Ran.Next(40));
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0003345C File Offset: 0x0003165C
		public override void Show()
		{
			if (base.TxtureObject != null)
			{
				SizeF sizeF = new SizeF((float)base.TxtureObject.Width * base.ScaleWidth, (float)base.TxtureObject.Height * base.ScaleLength);
				base.SpriteMain.Draw2D(base.TxtureObject.TXTure, base.TxtureObject.PosRect, sizeF, new PointF((float)(base.TxtureObject.Width / 2), 0f), (float)(base.Direction - 1.5707963267948966 + base.Angle), base.Position, Color.FromArgb(base.TransparentValue, base.ColorValue));
			}
		}
	}
}
