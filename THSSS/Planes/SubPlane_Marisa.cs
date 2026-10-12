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
			MarisaLaser marisaLaser = new MarisaLaser(StageData, "MarisaSubBullet00", OriginalPosition, -96f, ShootDirection)
			{
				Angle = 1.5707963705062866,
				Active = true
			};
			marisaLaser.SetBinding(this);
		}

		public override void Show()
		{
			if (Enabled)
			{
				if (TxtureObject != null)
				{
					double num = (double)(-(double)Time * 5 % 360) * 3.141592653589793 / 180.0;
					if (MyPlane.Power >= 400)
					{
						SpriteMain.Draw2D(TxtureObject, 1.3f, 1.3f, -(float)num, Position, Color.FromArgb(150, ColorValue), Mirrored);
					}
					SpriteMain.Draw2D(TxtureObject, ScaleWidth, ScaleLength, (float)num, Position, Color.FromArgb(TransparentValue, ColorValue), Mirrored);
				}
			}
		}
	}
}
