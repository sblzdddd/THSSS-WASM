using System.Drawing;

namespace Shooting
{
	public class BaseSubPlane : BaseObject
	{
		public double ShootDirection { get; set; }

		public BaseSubPlane(StageDataPackage StageData, string textureName, PointF Position)
			: base(StageData, textureName, Position, 0f, 1.5707963267948966)
		{
			TransparentValueF = 0f;
			TransparentVelocity = 20f;
			AngleWithDirection = false;
			AngleDegree = 90.0;
		}

		public virtual void Refresh()
		{
			Time = 0;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			var destPoint = DestPoint;
			var flag = 1 == 0;
			MoveToPoint(DestPoint);
		}

		public void MoveToPoint(PointF DP)
		{
			var num = (float)GetDistance(DP);
			if (num > 2f)
			{
				Velocity = num / 2.5f;
			}
			else if (num > 0f)
			{
				Velocity = 0.5f;
			}
			else
			{
				Velocity = 0f;
			}
			Direction = GetDirection(DP);
		}

		public override void Shoot()
		{
			var bulletGuidance = new BulletGuidance(StageData, "子机子弹", Position, 8f, -1.5707963267948966);
			bulletGuidance.TransparentValueF = 150f;
			bulletGuidance.Angle = 3.1415927410125732;
		}

		public override void Show()
		{
			if (Enabled)
			{
				base.Show();
			}
		}
	}
}
