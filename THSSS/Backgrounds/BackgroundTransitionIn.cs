using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	internal class BackgroundTransitionIn : BaseObject
	{
		public int Delay { get; set; }

		public BackgroundTransitionIn(StageDataPackage StageData)
			: base(StageData, "bullet21_7", new PointF(0f, 0f), 0f, 0.0)
		{
			Background.BackgroundList.Add(this);
			Position = new PointF((float)(StageData.BoundRect.X + StageData.BoundRect.Width / 2), (float)(StageData.BoundRect.Y + StageData.BoundRect.Height / 2));
			Scale = 30f;
			LifeTime = 480;
			Delay = 400;
			TransparentValueF = 255f;
			ColorValue = Color.Black;
		}

		public override void Ctrl()
		{
			Time++;
			if (Time <= Delay)
			{
				if (Time < 50)
				{
					loadingLight += 5;
				}
				else if (Time > 60)
				{
					if (Time % 60 >= 30)
					{
						loadingLight += 4;
					}
					else
					{
						loadingLight -= 4;
					}
				}
				if (loadingLight < 0)
				{
					loadingLight = 0;
				}
				else if (loadingLight > 255)
				{
					loadingLight = 255;
				}
				var particle = new Particle(StageData, "Star", new PointF((float)StageData.Ran.Next(400, 640), (float)StageData.Ran.Next(440, 580)), (float)(StageData.Ran.Next(10, 20) / 10), -1.5707963267948966 - (double)((float)StageData.Ran.Next(1, 5) / 10f));
				particle.Active = true;
				particle.Scale = (float)StageData.Ran.Next(25, 70) / 100f;
				particle.LifeTime = 70;
				EffectList.Remove(particle);
				stars.Add(particle);
				for (var i = stars.Count - 1; i >= 0; i--)
				{
					stars[i].Ctrl();
					if (stars[i].Time > stars[i].LifeTime)
					{
						stars.RemoveAt(i);
					}
				}
			}
			else if (stars.Count > 0)
			{
				stars.Clear();
			}
			if (Time > Delay)
			{
				TransparentValueF -= (float)(255 / (LifeTime - Delay) + 1);
			}
		}

		public void ShowLoading()
		{
			if (Time > Delay)
			{
				return;
			}
			for (var i = 0; i < stars.Count; i++)
			{
				stars[i].Show();
			}
			SpriteMain.Draw2D(TextureObjectDictionary["NowLoading-0"], 1f, 0f, new Point(520, 440), loadingLight);
		}

		private readonly List<Particle> stars = new List<Particle>();

		private int loadingLight;
	}
}
