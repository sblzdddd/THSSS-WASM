using System;
using System.Drawing;

namespace Shooting
{
	internal class BackgroundBlack : BaseObject
	{
		public BackgroundBlack(StageDataPackage StageData, PointF Position)
			: base(StageData, "MyBullet0008", new PointF(0f, 0f), 0f, 0.0)
		{
			base.Background.BackgroundList.Add(this);
			base.Position = Position;
			base.Scale = 1f;
			base.LifeTime = 350;
			base.TransparentValueF = 0f;
			base.MaxTransparent = 180;
			base.ColorValue = Color.Black;
		}

		public override void Ctrl()
		{
			base.Time++;
			base.Scale += 1f;
			if (base.Time < 40)
			{
				base.TransparentValueF += 6f;
			}
			if (base.Time > base.LifeTime - 40)
			{
				base.TransparentValueF -= 6f;
			}
		}
	}
}
