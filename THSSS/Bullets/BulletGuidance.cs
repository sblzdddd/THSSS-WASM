using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200003B RID: 59
	public class BulletGuidance : BaseObject
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000DB44 File Offset: 0x0000BD44
		// (set) Token: 0x06000233 RID: 563 RVA: 0x0000DB5B File Offset: 0x0000BD5B
		public BaseObject Target { get; set; }

		// Token: 0x06000234 RID: 564 RVA: 0x0000DB64 File Offset: 0x0000BD64
		public BulletGuidance(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData, textureName, Position, Velocity, Direction)
		{
			base.Damage = 3;
			base.Region = 15;
			base.LifeTime = 100;
			base.MyBulletList.Add(this);
			if (base.Boss != null)
			{
				if (base.Boss.OriginalPosition.X > 0f && base.Boss.OriginalPosition.X < (float)base.BoundRect.Width && base.Boss.OriginalPosition.Y > 0f && base.Boss.OriginalPosition.Y < (float)base.BoundRect.Height)
				{
					if (base.Boss.HitEnabled)
					{
						this.Target = base.Boss;
					}
				}
			}
			base.GhostingCount = 5;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000DCCC File Offset: 0x0000BECC
		public override void Ctrl()
		{
			base.Ctrl();
			if (this.Target == null && !this.targetFlag)
			{
				double dist = 300.0;
				base.EnemyPlaneList.ForEach(delegate(BaseEnemyPlane x)
				{
					if (x.HitEnabled)
					{
						double distance = this.GetDistance(x);
						if (distance < dist)
						{
							this.Target = x;
							dist = distance;
						}
					}
				});
			}
			else
			{
				this.targetFlag = true;
			}
			if (this.Target != null)
			{
				if (this.Target.HealthPoint > 0f)
				{
					double num = base.GetDirection(this.Target);
					double num2 = base.Direction;
					if (num > 3.141592653589793)
					{
						num -= 6.283185307179586;
					}
					if (num2 > 3.141592653589793)
					{
						num2 -= 6.283185307179586;
					}
					double num3 = num - num2;
					if (num3 > 3.141592653589793)
					{
						num3 -= 6.283185307179586;
					}
					if (Math.Abs(num3) > 0.019999999552965164 && base.GetDistance(this.Target) > 50.0)
					{
						base.Direction += num3 / 5.0;
					}
					else
					{
						base.Direction += num3;
					}
				}
				else
				{
					this.Target = null;
				}
			}
			if (this.OutBoundary())
			{
				base.MyBulletList.Remove(this);
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000DE7C File Offset: 0x0000C07C
		public override bool OutBoundary()
		{
			bool flag;
			if (base.LifeTime != 0 && base.Time > base.LifeTime)
			{
				flag = true;
			}
			else if (base.Time < 10)
			{
				flag = false;
			}
			else
			{
				int num = 2;
				flag = (float)(-(float)num) >= base.OriginalPosition.X || base.OriginalPosition.X >= (float)(base.BoundRect.Width + num) || (float)(-(float)num) >= base.OriginalPosition.Y || base.OriginalPosition.Y >= (float)(base.BoundRect.Height + num);
			}
			return flag;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000DF48 File Offset: 0x0000C148
		public override void GiveEndEffect()
		{
			ParticleBiger particleBiger = new ParticleBiger(base.StageData, null, base.Position, 2f, base.Direction);
			particleBiger.Scale = 1.2f;
			particleBiger.LifeTime = 15;
			particleBiger.TxtureObject = base.TxtureObject;
			particleBiger.Angle = base.Angle;
		}

		// Token: 0x040000AE RID: 174
		private bool targetFlag = false;
	}
}
