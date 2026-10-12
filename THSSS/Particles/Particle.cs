using System;
using System.Drawing;

namespace Shooting
{
	internal class Particle : BaseEffect
	{
		public Particle(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData, textureName, Position, Velocity, Direction)
		{
			TransparentValueF = 180f;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time > 10)
			{
				TransparentValueF -= 5f;
			}
			double num = 80.0 * Math.Sin((double)((float)Time * 33f / 180f) * 3.141592653589793);
			Tran = TransparentValue + (int)num;
			if (Tran < 0)
			{
				Tran = 0;
			}
			else if (Tran > 255)
			{
				Tran = 255;
			}
		}

		public override void Show()
		{
			SizeF sizeF = new SizeF((float)TxtureObject.Width * Scale, (float)TxtureObject.Height * Scale);
			SpriteMain.Draw2D(TxtureObject.TXTure, TxtureObject.PosRect, sizeF, TxtureObject.RotatingCenter, 0f, Position, Color.FromArgb(Tran, Color.White));
		}

		private int Tran;
	}
}
