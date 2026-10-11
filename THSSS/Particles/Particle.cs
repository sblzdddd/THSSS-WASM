using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200018A RID: 394
	internal class Particle : BaseEffect
	{
		// Token: 0x06000956 RID: 2390 RVA: 0x0005A793 File Offset: 0x00058993
		public Particle(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData, textureName, Position, Velocity, Direction)
		{
			base.TransparentValueF = 180f;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0005A7B4 File Offset: 0x000589B4
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time > 10)
			{
				base.TransparentValueF -= 5f;
			}
			double num = 80.0 * Math.Sin((double)((float)base.Time * 33f / 180f) * 3.141592653589793);
			this.Tran = base.TransparentValue + (int)num;
			if (this.Tran < 0)
			{
				this.Tran = 0;
			}
			else if (this.Tran > 255)
			{
				this.Tran = 255;
			}
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0005A868 File Offset: 0x00058A68
		public override void Show()
		{
			SizeF sizeF = new SizeF((float)base.TxtureObject.Width * base.Scale, (float)base.TxtureObject.Height * base.Scale);
			base.SpriteMain.Draw2D(base.TxtureObject.TXTure, base.TxtureObject.PosRect, sizeF, base.TxtureObject.RotatingCenter, 0f, base.Position, Color.FromArgb(this.Tran, Color.White));
		}

		// Token: 0x0400037C RID: 892
		private int Tran;
	}
}
