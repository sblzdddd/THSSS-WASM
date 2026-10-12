using System;
using System.Drawing;
using SlimDX;

namespace Shooting
{
	public class BackgroundWing : BaseParticle3D
	{
		public BackgroundWing(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int LifeTime)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			StageData.Particle3D.ParticleList.Add(this);
			this.LifeTime = LifeTime;
			if (Boss != null)
			{
				SetBinding(Boss);
			}
			Scale = 0.1f;
			MaxScale = 0.6f;
			ScaleVelocity = 0.02f;
			RotatingAxis = new Vector3(0f, 1f, 0f);
			lastlife = Boss.Life;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			Angle3D = -(float)(0.17453292519943295 * (1.0 - Math.Sin((double)((float)Time / 30f))));
			if (Boss != null)
			{
				if (Boss.Time > Boss.UnmatchedTime && Time > 50 && Boss.HitEnabled)
				{
					for (int i = MyBulletList.Count - 1; i >= 0; i--)
					{
						if (MyBulletList[i].OriginalPosition.Y < OriginalPosition.Y)
						{
							if (MyBulletList[i].OriginalPosition.X > OriginalPosition.X - 100f && MyBulletList[i].OriginalPosition.X < OriginalPosition.X + 100f)
							{
								if (Boss.Time > Boss.UnmatchedTime)
								{
									Boss.HealthPoint -= (float)MyBulletList[i].Damage * (1f - Boss.Armon) / 3f;
								}
								MyBulletList[i].GiveEndEffect();
								MyBulletList.RemoveAt(i);
								MyPlane.Score += 100L;
							}
						}
					}
				}
				if (Boss.Life < lastlife)
				{
					LifeTime = Time + 10;
					TransparentVelocity = -25f;
					lastlife = Boss.Life;
				}
			}
			if (Time == LifeTime)
			{
				BulletPicEmitter.ShootItem(StageData, "WingItem.png", OriginalPosition, 10f);
			}
		}

		private int lastlife;
	}
}
