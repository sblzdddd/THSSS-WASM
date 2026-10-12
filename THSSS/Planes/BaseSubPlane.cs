using System.Drawing;

namespace Shooting
{
	public class BaseSubPlane : BaseObject
	{
		public double ShootDirection { get; set; }

		public BaseSubPlane(StageDataPackage StageData, string textureName, PointF Position)
			: base(StageData, textureName, Position, 0f, 1.5707963267948966)
		{
			base.TransparentValueF = 0f;
			base.TransparentVelocity = 20f;
			base.AngleWithDirection = false;
			base.AngleDegree = 90.0;
		}

		public virtual void Refresh()
		{
			base.Time = 0;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			PointF destPoint = base.DestPoint;
			bool flag = 1 == 0;
			this.MoveToPoint(base.DestPoint);
		}

		public void MoveToPoint(PointF DP)
		{
			float num = (float)base.GetDistance(DP);
			if (num > 2f)
			{
				base.Velocity = num / 2.5f;
			}
			else if (num > 0f)
			{
				base.Velocity = 0.5f;
			}
			else
			{
				base.Velocity = 0f;
			}
			base.Direction = base.GetDirection(DP);
		}

		public override void Shoot()
		{
			BulletGuidance bulletGuidance = new BulletGuidance(base.StageData, "子机子弹", base.Position, 8f, -1.5707963267948966);
			bulletGuidance.TransparentValueF = 150f;
			bulletGuidance.Angle = 3.1415927410125732;
		}

		public override void Show()
		{
			if (base.Enabled)
			{
				base.Show();
			}
		}
	}
}
