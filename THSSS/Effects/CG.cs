using System;
using System.Drawing;

namespace Shooting
{
	internal class CG : BaseStoryItem
	{
		public CG(StageDataPackage StageData, string textureName)
			: base(StageData)
		{
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2));
			base.Velocity = 0f;
			base.Direction = 0.0;
			base.Active = false;
			base.TransparentValueF = 0f;
			base.TransparentVelocity = 20f;
			base.Angle = (double)(-(double)((float)(base.Direction - 1.5707963267948966)));
		}
	}
}
