using System.Drawing;

namespace Shooting
{
	internal class CG : BaseStoryItem
	{
		public CG(StageDataPackage StageData, string textureName)
			: base(StageData)
		{
			OriginalPosition = new PointF((float)(BoundRect.Width / 2), (float)(BoundRect.Height / 2));
			Velocity = 0f;
			Direction = 0.0;
			Active = false;
			TransparentValueF = 0f;
			TransparentVelocity = 20f;
			Angle = (double)(-(double)((float)(Direction - 1.5707963267948966)));
		}
	}
}
