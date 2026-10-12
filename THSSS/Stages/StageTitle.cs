using System.Drawing;

namespace Shooting
{
	public class StageTitle : BaseEffect
	{
		public StageTitle(StageDataPackage StageData, string TextureName)
			: base(StageData, TextureName, new Point(0, 0), 0f, 1.5707963267948966)
		{
			OriginalPosition = new PointF((float)(BoundRect.Width / 2), (float)(BoundRect.Height / 2 - 80));
			LifeTime = 200;
			TransparentValueF = 0f;
			Active = false;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time < LifeTime / 3)
			{
				TransparentValueF += (float)(MaxTransparent * 3 / LifeTime);
			}
			else if (Time > LifeTime * 2 / 3)
			{
				TransparentValueF -= (float)(MaxTransparent * 3 / LifeTime);
			}
		}
	}
}
