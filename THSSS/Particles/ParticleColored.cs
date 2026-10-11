using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000040 RID: 64
	internal class ParticleColored : BaseEffect
	{
		// Token: 0x06000245 RID: 581 RVA: 0x0000E69C File Offset: 0x0000C89C
		public ParticleColored(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData, textureName, Position, Velocity, Direction)
		{
			switch (base.Ran.Next(7))
			{
			case 0:
				this.color = Color.BlueViolet;
				break;
			case 1:
				this.color = Color.CadetBlue;
				break;
			case 2:
				this.color = Color.DarkBlue;
				break;
			case 3:
				this.color = Color.Green;
				break;
			case 4:
				this.color = Color.SkyBlue;
				break;
			case 5:
				this.color = Color.Blue;
				break;
			case 6:
				this.color = Color.CornflowerBlue;
				break;
			default:
				this.color = Color.White;
				break;
			}
			base.TransparentValueF = 150f;
			base.Scale = 2f;
			base.LifeTime = 50;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000E774 File Offset: 0x0000C974
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Direction > 3.141592653589793)
			{
				double num = 1.5707963267948966 + base.Direction;
				if (num > 3.141592653589793)
				{
					num -= 6.283185307179586;
				}
				base.Direction -= num / 5.0;
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000E7F0 File Offset: 0x0000C9F0
		public override void Show()
		{
			SizeF sizeF = new SizeF((float)base.TxtureObject.Width * base.Scale, (float)base.TxtureObject.Height * base.Scale);
			base.SpriteMain.Draw2D(base.TxtureObject.TXTure, base.TxtureObject.PosRect, sizeF, base.TxtureObject.RotatingCenter, 0f, base.Position, Color.FromArgb(base.TransparentValue, this.color));
		}

		// Token: 0x040000B1 RID: 177
		private Color color;
	}
}
