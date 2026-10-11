using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001A1 RID: 417
	public class SubPlane_Marisa : BaseSubPlane
	{
		// Token: 0x060009D1 RID: 2513 RVA: 0x0005DBBC File Offset: 0x0005BDBC
		public SubPlane_Marisa(StageDataPackage StageData, PointF Position)
			: base(StageData, "MarisaSubPlane", Position)
		{
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0005DBD0 File Offset: 0x0005BDD0
		public override void Shoot()
		{
			MarisaLaser marisaLaser = new MarisaLaser(base.StageData, "MarisaSubBullet00", base.OriginalPosition, -96f, base.ShootDirection)
			{
				Angle = 1.5707963705062866,
				Active = true
			};
			marisaLaser.SetBinding(this);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0005DC24 File Offset: 0x0005BE24
		public override void Show()
		{
			if (base.Enabled)
			{
				if (base.TxtureObject != null)
				{
					double num = (double)(-(double)base.Time * 5 % 360) * 3.141592653589793 / 180.0;
					if (base.MyPlane.Power >= 400)
					{
						base.SpriteMain.Draw2D(base.TxtureObject, 1.3f, 1.3f, -(float)num, base.Position, Color.FromArgb(150, base.ColorValue), base.Mirrored);
					}
					base.SpriteMain.Draw2D(base.TxtureObject, base.ScaleWidth, base.ScaleLength, (float)num, base.Position, Color.FromArgb(base.TransparentValue, base.ColorValue), base.Mirrored);
				}
			}
		}
	}
}
