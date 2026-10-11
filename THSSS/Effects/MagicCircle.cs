using System;
using System.Drawing;

namespace Shooting
{
	internal class MagicCircle : BaseEffect
	{
		public MagicCircle(StageDataPackage StageData, string textureName)
			: base(StageData, textureName, new PointF(0f, 0f), 0f, 0.0)
		{
		}

		public override void Move()
		{
			if (base.Boss != null)
			{
				base.OriginalPosition = base.Boss.OriginalPosition;
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			base.Angle += 0.019999999552965164;
			base.Scale += (float)Math.Sin((double)((float)base.Time * 3f / 180f) * 3.141592653589793) / 80f;
			base.TransparentValueF = (float)(100 + (int)(Math.Sin((double)((float)base.Time * 2f / 180f) * 3.141592653589793) * 75.0));
		}

		public override bool OutBoundary()
		{
			return base.Boss == null;
		}
	}
}
