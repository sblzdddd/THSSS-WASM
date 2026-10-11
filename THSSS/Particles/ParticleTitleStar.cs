using System;
using System.Drawing;

namespace Shooting
{
	internal class ParticleTitleStar : BaseBackground
	{
		public ParticleTitleStar(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData, textureName, Position, Velocity, Direction)
		{
			base.TransparentValueF = 180f;
		}

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

		public override void Show()
		{
			SizeF sizeF = new SizeF((float)base.TxtureObject.Width * base.Scale, (float)base.TxtureObject.Height * base.Scale);
			base.SpriteMain.Draw2D(base.TxtureObject.TXTure, base.TxtureObject.PosRect, sizeF, base.TxtureObject.RotatingCenter, 0f, base.Position, Color.FromArgb(this.Tran, Color.White));
		}

		private int Tran;
	}
}
