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
			for (int i = this.lastAngle.Length - 1; i > 0; i--)
			{
				this.lastAngle[i] = this.lastAngle[i - 1];
			}
			this.lastAngle[0] = base.Angle;
			if (base.StageData.SlowMode > 0)
			{
				base.Active = true;
				base.ColorValue = Color.Pink;
				base.AngularVelocityDegree = 1f;
			}
			else
			{
				base.Active = false;
				base.ColorValue = Color.White;
				this.sc = (float)Math.Sin((double)((float)base.Time / 103.5f) * 3.141592653589793 * 2.0) / 16f;
				base.Scale = 2.5f + this.sc;
				base.AngularVelocityDegree = -0.5f;
			}
		}

		public override void Show()
		{
			if (base.StageData.SlowMode > 0)
			{
				double angle = base.Angle;
				int transparentValue = base.TransparentValue;
				for (int i = this.lastAngle.Length - 1; i >= 0; i -= 2)
				{
					base.Angle = this.lastAngle[i];
					base.TransparentValueF = (float)(transparentValue * (this.lastAngle.Length - i + 15) / (this.lastAngle.Length + 1 + 15));
					base.Show();
				}
				base.Angle = angle;
				base.TransparentValueF = (float)transparentValue;
			}
			base.Show();
		}

		private double[] lastAngle = new double[10];

		private float sc;
	}
}
