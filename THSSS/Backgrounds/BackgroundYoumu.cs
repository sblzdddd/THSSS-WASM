using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200017F RID: 383
	public class BackgroundYoumu : BackgroundRotate
	{
		// Token: 0x06000932 RID: 2354 RVA: 0x00058761 File Offset: 0x00056961
		public BackgroundYoumu(StageDataPackage StageData, string textureName)
			: base(StageData, textureName)
		{
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0005877C File Offset: 0x0005697C
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

		// Token: 0x06000934 RID: 2356 RVA: 0x00058870 File Offset: 0x00056A70
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

		// Token: 0x04000361 RID: 865
		private double[] lastAngle = new double[10];

		// Token: 0x04000362 RID: 866
		private float sc;
	}
}
