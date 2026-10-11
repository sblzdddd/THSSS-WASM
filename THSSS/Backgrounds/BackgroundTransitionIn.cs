using System;
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
			if (base.Time > this.Delay)
			{
				base.TransparentValueF -= (float)(255 / (base.LifeTime - this.Delay) + 1);
			}
		}
	}
}
