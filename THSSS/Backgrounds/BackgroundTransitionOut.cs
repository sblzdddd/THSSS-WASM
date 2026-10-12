using System.Drawing;

namespace Shooting
{
	internal class BackgroundTransitionOut : BaseObject
	{
		public BackgroundTransitionOut(StageDataPackage StageData, int LifeTime)
			: base(StageData, "bullet21_7", new PointF(0f, 0f), 0f, 0.0)
		{
			Background.BackgroundList.Add(this);
			Position = new PointF((float)(StageData.BoundRect.X + StageData.BoundRect.Width / 2), (float)(StageData.BoundRect.Y + StageData.BoundRect.Height / 2));
			Scale = 30f;
			TransparentValueF = 0f;
			TransparentVelocity = (float)(MaxTransparent / LifeTime);
			this.LifeTime = LifeTime + 1000;
			ColorValue = Color.Black;
		}
	}
}
