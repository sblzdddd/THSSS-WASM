using System;
using System.Drawing;

namespace Shooting
{
	internal class BackgroundQB : BaseObject
	{
		public BackgroundQB(StageDataPackage StageData, string textureName)
			: base(StageData, textureName, new PointF(0f, 0f), 0f, 1.5707963267948966)
		{
			base.Background.BackgroundList.Add(this);
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), 200f);
			base.TransparentValueF = 0f;
			base.Scale = 0.9f;
		}

		public override void Ctrl()
		{
			base.Time++;
			if (base.TransparentValueF < 200f)
			{
				base.TransparentValueF += 5f;
			}
		}
	}
}
