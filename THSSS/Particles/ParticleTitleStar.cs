using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000BD RID: 189
	internal class ParticleTitleStar : BaseBackground
	{
		// Token: 0x06000555 RID: 1365 RVA: 0x0002E3EA File Offset: 0x0002C5EA
		public ParticleTitleStar(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData, textureName, Position, Velocity, Direction)
		{
			base.TransparentValueF = 180f;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0002E408 File Offset: 0x0002C608
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == 10)
			{
				base.TransparentVelocity -= 5f;
				base.Time += base.Ran.Next(20);
			}
			double num = 80.0 * Math.Sin((double)((float)base.Time * 20f / 180f) * 3.141592653589793);
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

		// Token: 0x06000557 RID: 1367 RVA: 0x0002E4D8 File Offset: 0x0002C6D8
		public override void Show()
		{
			SizeF sizeF = new SizeF((float)base.TxtureObject.Width * base.Scale, (float)base.TxtureObject.Height * base.Scale);
			base.SpriteMain.Draw2D(base.TxtureObject.TXTure, base.TxtureObject.PosRect, sizeF, base.TxtureObject.RotatingCenter, 0f, base.Position, Color.FromArgb(this.Tran, Color.White));
		}

		// Token: 0x040001CA RID: 458
		private int Tran;
	}
}
