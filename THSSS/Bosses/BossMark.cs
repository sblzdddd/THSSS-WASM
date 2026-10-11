using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200006E RID: 110
	public class BossMark : BaseObject
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x0001A884 File Offset: 0x00018A84
		public BossMark(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.TxtureObject = base.TextureObjectDictionary["BossMark"];
			base.Position = new PointF((float)(base.BoundRect.Left + base.BoundRect.Width / 2), 472f);
			base.AngleDegree = 90.0;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001A8FC File Offset: 0x00018AFC
		public override void Ctrl()
		{
			base.Time++;
			if (base.Boss != null)
			{
				base.OriginalPosition = new PointF(base.Boss.OriginalPosition.X + (float)base.BoundRect.X, 472f);
				base.TransparentValueF = (float)(150 + (int)(100.0 * Math.Sin((double)base.Time * 3.141592653589793 * 2.0 / 60.0)));
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001A9A4 File Offset: 0x00018BA4
		public override void Show()
		{
			if (base.Boss != null)
			{
				if (base.TxtureObject != null)
				{
					base.SpriteMain.Draw2D(base.TxtureObject, base.ScaleWidth, base.ScaleLength, (float)(base.Direction - 1.5707963267948966 + base.Angle), base.OriginalPosition, Color.FromArgb(base.TransparentValue, base.ColorValue), base.Mirrored);
				}
			}
		}
	}
}
