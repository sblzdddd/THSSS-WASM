using System;
using System.Drawing;

namespace Shooting
{
	internal class CharacterL : CharacterR
	{
		public CharacterL(StageDataPackage StageData, string textureName)
			: base(StageData, textureName)
		{
			base.Position = new PointF((float)(base.BoundRect.Left + 100), (float)(base.BoundRect.Top + 200));
		}

		public override void Move()
		{
			if (base.Active)
			{
				if (base.OriginalPosition.X < 150f)
				{
					base.OriginalPosition = new PointF(base.OriginalPosition.X + 2f, base.OriginalPosition.Y);
				}
			}
			else if (base.OriginalPosition.X > 100f)
			{
				base.OriginalPosition = new PointF(base.OriginalPosition.X - 2f, base.OriginalPosition.Y);
			}
		}
	}
}
