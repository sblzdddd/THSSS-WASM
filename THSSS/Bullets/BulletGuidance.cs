using System;
using System.Drawing;

namespace Shooting
{
	public class BulletGuidance : BaseObject
	{
		public BaseObject Target { get; set; }

		public BulletGuidance(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData, textureName, Position, Velocity, Direction)
		{
			Damage = 3;
			Region = 15;
			LifeTime = 100;
			MyBulletList.Add(this);
			if (Boss != null)
			{
				if (Boss.OriginalPosition.X > 0f && Boss.OriginalPosition.X < (float)BoundRect.Width && Boss.OriginalPosition.Y > 0f && Boss.OriginalPosition.Y < (float)BoundRect.Height)
				{
					if (Boss.HitEnabled)
					{
						Target = Boss;
					}
				}
			}
			GhostingCount = 5;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Target == null && !targetFlag)
			{
				var dist = 300.0;
				EnemyPlaneList.ForEach(delegate(BaseEnemyPlane x)
				{
					if (x.HitEnabled)
					{
						var distance = GetDistance(x);
						if (distance < dist)
						{
							Target = x;
							dist = distance;
						}
					}
				});
			}
			else
			{
				targetFlag = true;
			}
			if (Target != null)
			{
				if (Target.HealthPoint > 0f)
				{
					var num = GetDirection(Target);
					var num2 = Direction;
					if (num > 3.141592653589793)
					{
						num -= 6.283185307179586;
					}
					if (num2 > 3.141592653589793)
					{
						num2 -= 6.283185307179586;
					}
					var num3 = num - num2;
					if (num3 > 3.141592653589793)
					{
						num3 -= 6.283185307179586;
					}
					if (Math.Abs(num3) > 0.019999999552965164 && GetDistance(Target) > 50.0)
					{
						Direction += num3 / 5.0;
					}
					else
					{
						Direction += num3;
					}
				}
				else
				{
					Target = null;
				}
			}
			if (OutBoundary())
			{
				MyBulletList.Remove(this);
			}
		}

		public override bool OutBoundary()
		{
			bool flag;
			if (LifeTime != 0 && Time > LifeTime)
			{
				flag = true;
			}
			else if (Time < 10)
			{
				flag = false;
			}
			else
			{
				var num = 2;
				flag = (float)(-(float)num) >= OriginalPosition.X || OriginalPosition.X >= (float)(BoundRect.Width + num) || (float)(-(float)num) >= OriginalPosition.Y || OriginalPosition.Y >= (float)(BoundRect.Height + num);
			}
			return flag;
		}

		public override void GiveEndEffect()
		{
			var particleBiger = new ParticleBiger(StageData, null, Position, 2f, Direction);
			particleBiger.Scale = 1.2f;
			particleBiger.LifeTime = 15;
			particleBiger.TxtureObject = TxtureObject;
			particleBiger.Angle = Angle;
		}

		private bool targetFlag = false;
	}
}
