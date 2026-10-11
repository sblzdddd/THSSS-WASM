using System;
using System.Drawing;
using SlimDX;

namespace Shooting
{
	// Token: 0x020001F8 RID: 504
	public class BackgroundWing : BaseParticle3D
	{
		// Token: 0x06000B67 RID: 2919 RVA: 0x00071524 File Offset: 0x0006F724
		public BackgroundWing(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int LifeTime)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			StageData.Particle3D.ParticleList.Add(this);
			base.LifeTime = LifeTime;
			if (base.Boss != null)
			{
				base.SetBinding(base.Boss);
			}
			base.Scale = 0.1f;
			base.MaxScale = 0.6f;
			base.ScaleVelocity = 0.02f;
			base.RotatingAxis = new Vector3(0f, 1f, 0f);
			this.lastlife = base.Boss.Life;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x000715C8 File Offset: 0x0006F7C8
		public override void Ctrl()
		{
			base.Ctrl();
			base.Angle3D = -(float)(0.17453292519943295 * (1.0 - Math.Sin((double)((float)base.Time / 30f))));
			if (base.Boss != null)
			{
				if (base.Boss.Time > base.Boss.UnmatchedTime && base.Time > 50 && base.Boss.HitEnabled)
				{
					for (int i = base.MyBulletList.Count - 1; i >= 0; i--)
					{
						if (base.MyBulletList[i].OriginalPosition.Y < base.OriginalPosition.Y)
						{
							if (base.MyBulletList[i].OriginalPosition.X > base.OriginalPosition.X - 100f && base.MyBulletList[i].OriginalPosition.X < base.OriginalPosition.X + 100f)
							{
								if (base.Boss.Time > base.Boss.UnmatchedTime)
								{
									base.Boss.HealthPoint -= (float)base.MyBulletList[i].Damage * (1f - base.Boss.Armon) / 3f;
								}
								base.MyBulletList[i].GiveEndEffect();
								base.MyBulletList.RemoveAt(i);
								base.MyPlane.Score += 100L;
							}
						}
					}
				}
				if (base.Boss.Life < this.lastlife)
				{
					base.LifeTime = base.Time + 10;
					base.TransparentVelocity = -25f;
					this.lastlife = base.Boss.Life;
				}
			}
			if (base.Time == base.LifeTime)
			{
				BulletPicEmitter.ShootItem(base.StageData, "WingItem.png", base.OriginalPosition, 10f);
			}
		}

		// Token: 0x04000433 RID: 1075
		private int lastlife;
	}
}
