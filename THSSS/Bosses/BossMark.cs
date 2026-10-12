using System;
using System.Drawing;

namespace Shooting
{
	public class BossMark : BaseObject
	{
		public BossMark(StageDataPackage StageData)
		{
			this.StageData = StageData;
			TxtureObject = TextureObjectDictionary["BossMark"];
			Position = new PointF((float)(BoundRect.Left + BoundRect.Width / 2), 472f);
			AngleDegree = 90.0;
		}

		public override void Ctrl()
		{
			Time++;
			if (Boss != null)
			{
				OriginalPosition = new PointF(Boss.OriginalPosition.X + (float)BoundRect.X, 472f);
				TransparentValueF = (float)(150 + (int)(100.0 * Math.Sin((double)Time * 3.141592653589793 * 2.0 / 60.0)));
			}
		}

		public override void Show()
		{
			if (Boss != null)
			{
				if (TxtureObject != null)
				{
					SpriteMain.Draw2D(TxtureObject, ScaleWidth, ScaleLength, (float)(Direction - 1.5707963267948966 + Angle), OriginalPosition, Color.FromArgb(TransparentValue, ColorValue), Mirrored);
				}
			}
		}
	}
}
