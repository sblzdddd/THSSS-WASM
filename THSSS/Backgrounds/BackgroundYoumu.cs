using System;
using System.Drawing;

namespace Shooting
{
	public class BackgroundYoumu : BackgroundRotate
	{
		public BackgroundYoumu(StageDataPackage StageData, string textureName)
			: base(StageData, textureName)
		{
		}

		public override void Ctrl()
		{
			base.Ctrl();
			for (int i = lastAngle.Length - 1; i > 0; i--)
			{
				lastAngle[i] = lastAngle[i - 1];
			}
			lastAngle[0] = Angle;
			if (StageData.SlowMode > 0)
			{
				Active = true;
				ColorValue = Color.Pink;
				AngularVelocityDegree = 1f;
			}
			else
			{
				Active = false;
				ColorValue = Color.White;
				sc = (float)Math.Sin((double)((float)Time / 103.5f) * 3.141592653589793 * 2.0) / 16f;
				Scale = 2.5f + sc;
				AngularVelocityDegree = -0.5f;
			}
		}

		public override void Show()
		{
			if (StageData.SlowMode > 0)
			{
				double angle = Angle;
				int transparentValue = TransparentValue;
				for (int i = lastAngle.Length - 1; i >= 0; i -= 2)
				{
					Angle = lastAngle[i];
					TransparentValueF = (float)(transparentValue * (lastAngle.Length - i + 15) / (lastAngle.Length + 1 + 15));
					base.Show();
				}
				Angle = angle;
				TransparentValueF = (float)transparentValue;
			}
			base.Show();
		}

		private double[] lastAngle = new double[10];

		private float sc;
	}
}
