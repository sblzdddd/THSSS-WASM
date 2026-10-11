using System;
using System.Drawing;

namespace Shooting
{
	internal class BackgroundTransitionOut : BaseObject
	{
		public BackgroundTransitionOut(StageDataPackage StageData, int LifeTime)
			: base(StageData, "bullet21_7", new PointF(0f, 0f), 0f, 0.0)
		{
			base.Background.BackgroundList.Add(this);
			base.Position = new PointF((float)(StageData.BoundRect.X + StageData.BoundRect.Width / 2), (float)(StageData.BoundRect.Y + StageData.BoundRect.Height / 2));
			base.Scale = 30f;
			base.TransparentValueF = 0f;
			base.TransparentVelocity = (float)(base.MaxTransparent / LifeTime);
			base.LifeTime = LifeTime + 1000;
			base.ColorValue = Color.Black;
		}
	}
}
