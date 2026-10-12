using System.Drawing;

namespace Shooting
{
	public class SubPlane_Marisa : BaseSubPlane
	{
		public SubPlane_Marisa(StageDataPackage StageData, PointF Position)
			: base(StageData, "MarisaSubPlane", Position)
		{
		}

		public override void Shoot()
		{
			MarisaLaser marisaLaser = new MarisaLaser(base.StageData, "MarisaSubBullet00", base.OriginalPosition, -96f, base.ShootDirection)
			{
				Angle = 1.5707963705062866,
				Active = true
			};
			marisaLaser.SetBinding(this);
		}

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
