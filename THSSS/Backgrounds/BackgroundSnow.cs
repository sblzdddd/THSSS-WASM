using System.Drawing;

namespace Shooting
{
	internal class BackgroundSnow : BaseObject
	{
		public BackgroundSnow(StageDataPackage StageData)
		{
			this.StageData = StageData;
			Background.BackgroundList.Add(this);
		}

		public override void Ctrl()
		{
			Time++;
			Shoot();
		}

		public override void Shoot()
		{
			if (Time % 20 == 0)
			{
				for (int i = 0; i < 3; i++)
				{
					ParticleBack particleBack = new ParticleBack(StageData, "snow", new PointF((float)Ran.Next(BoundRect.Left, BoundRect.Right), (float)Ran.Next(BoundRect.Top, BoundRect.Bottom / 3)), (float)Ran.Next(10, 20) / 10f, 1.5707963267948966 + 0.5 * Ran.NextDouble());
					particleBack.LifeTime = 200;
					particleBack.AngularVelocity = (float)Ran.Next(20) / 1000f;
					particleBack.Scale = (float)Ran.Next(2, 10) / 10f;
					particleBack.Active = true;
				}
			}
		}

		public override void Show()
		{
		}
	}
}
