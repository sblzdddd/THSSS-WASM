using System.Drawing;

namespace Shooting
{
	internal class BulletYukaLaser : BulletLaser
	{
		public BulletYukaLaser(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			base.OriginalPosition = OriginalPosition;
			base.ScaleWidth = 0.05f;
			base.Active = true;
			base.LifeTime = 500;
			base.ColorValue = Color.FromArgb(255, 120, 80);
			base.UnRemoveable = true;
			base.Angle = 0.0;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			base.Region = (int)((float)base.TxtureObject.Width / 2f * 0.7f);
			if (base.ScaleLength < base.DesScaleLength)
			{
				base.ScaleLength += 0.05f;
			}
			else if (base.ScaleWidth < base.DesScaleWidth)
			{
				base.ScaleWidth += 0.05f;
			}
			if (base.Time < base.LifeTime - 50)
			{
				base.TransparentValueF = (float)(150 + base.Ran.Next(40));
			}
			else
			{
				base.TransparentValueF = (float)((base.LifeTime - base.Time) * 2 - 20 + base.Ran.Next(40));
			}
		}

		public override void HitCheckAll()
		{
			if (base.MyPlane.HitEnabled && this.HitCheck(base.MyPlane) && base.Time < base.LifeTime - 50)
			{
				base.MyPlane.PreMiss();
			}
		}
	}
}
