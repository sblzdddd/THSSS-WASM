using System;
using System.Drawing;

namespace Shooting
{
	internal class BackgroundTouhouWater : BackgroundTouhouStage01_3D
	{
		public float V1 { get; set; }

		public BackgroundTouhouWater(StageDataPackage StageData, string textureName)
			: base(StageData, textureName)
		{
		}

		public override void Ctrl()
		{
			base.Ctrl();
			float v = this.V1;
			float num = 0.2f * (float)Math.Sin((double)(base.Time + 30) * 3.141592653589793 * 2.0 / 200.0);
			float num2 = base.OriginalPosition.X + v;
			float num3 = base.OriginalPosition.Y + num;
			if (num2 < -500f)
			{
				num2 += 1000f;
			}
			else if (num2 > 500f)
			{
				num2 -= 1000f;
			}
			base.OriginalPosition = new PointF(num2, num3);
		}
	}
}
