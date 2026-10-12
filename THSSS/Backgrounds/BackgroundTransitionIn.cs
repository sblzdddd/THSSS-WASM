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
			base.Background.BackgroundList.Add(this);
			base.Position = new PointF((float)(StageData.BoundRect.X + StageData.BoundRect.Width / 2), (float)(StageData.BoundRect.Y + StageData.BoundRect.Height / 2));
			base.Scale = 30f;
			base.LifeTime = 480;
			this.Delay = 400;
			base.TransparentValueF = 255f;
			base.ColorValue = Color.Black;
		}

		public override void Ctrl()
		{
			base.Time++;
			if (base.Time <= this.Delay)
			{
				if (base.Time < 50)
				{
					this.loadingLight += 5;
				}
				else if (base.Time > 60)
				{
					if (base.Time % 60 >= 30)
					{
						this.loadingLight += 4;
					}
					else
					{
						this.loadingLight -= 4;
					}
				}
				if (this.loadingLight < 0)
				{
					this.loadingLight = 0;
				}
				else if (this.loadingLight > 255)
				{
					this.loadingLight = 255;
				}
				Particle particle = new Particle(base.StageData, "Star", new PointF((float)base.StageData.Ran.Next(400, 640), (float)base.StageData.Ran.Next(440, 580)), (float)(base.StageData.Ran.Next(10, 20) / 10), -1.5707963267948966 - (double)((float)base.StageData.Ran.Next(1, 5) / 10f));
				particle.Active = true;
				particle.Scale = (float)base.StageData.Ran.Next(25, 70) / 100f;
				particle.LifeTime = 70;
				base.EffectList.Remove(particle);
				this.stars.Add(particle);
				for (int i = this.stars.Count - 1; i >= 0; i--)
				{
					this.stars[i].Ctrl();
					if (this.stars[i].Time > this.stars[i].LifeTime)
					{
						this.stars.RemoveAt(i);
					}
				}
			}
			else if (this.stars.Count > 0)
			{
				this.stars.Clear();
			}
			if (base.Time > this.Delay)
			{
				base.TransparentValueF -= (float)(255 / (base.LifeTime - this.Delay) + 1);
			}
		}

		public void ShowLoading()
		{
			if (base.Time > this.Delay)
			{
				return;
			}
			for (int i = 0; i < this.stars.Count; i++)
			{
				this.stars[i].Show();
			}
			base.SpriteMain.Draw2D(base.TextureObjectDictionary["NowLoading-0"], 1f, 0f, new Point(520, 440), this.loadingLight);
		}

		private readonly List<Particle> stars = new List<Particle>();

		private int loadingLight;
	}
}
