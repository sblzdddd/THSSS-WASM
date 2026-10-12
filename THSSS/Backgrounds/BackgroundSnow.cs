using System.Drawing;

namespace Shooting
{
	internal class BackgroundSnow : BaseObject
	{
		public BackgroundSnow(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.Background.BackgroundList.Add(this);
		}

		public override void Ctrl()
		{
			base.Time++;
			this.Shoot();
		}

		public override void Shoot()
		{
			if (base.Time % 20 == 0)
			{
				for (int i = 0; i < 3; i++)
				{
					ParticleBack particleBack = new ParticleBack(base.StageData, "snow", new PointF((float)base.Ran.Next(base.BoundRect.Left, base.BoundRect.Right), (float)base.Ran.Next(base.BoundRect.Top, base.BoundRect.Bottom / 3)), (float)base.Ran.Next(10, 20) / 10f, 1.5707963267948966 + 0.5 * base.Ran.NextDouble());
					particleBack.LifeTime = 200;
					particleBack.AngularVelocity = (float)base.Ran.Next(20) / 1000f;
					particleBack.Scale = (float)base.Ran.Next(2, 10) / 10f;
					particleBack.Active = true;
				}
			}
		}

		public override void Show()
		{
		}
	}
}
