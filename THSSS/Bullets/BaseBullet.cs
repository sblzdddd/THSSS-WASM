using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000D7 RID: 215
	public class BaseBullet : BaseBullet_Touhou
	{
		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00034EF8 File Offset: 0x000330F8
		// (set) Token: 0x060005F3 RID: 1523 RVA: 0x00034F10 File Offset: 0x00033110
		public int HighLightValue
		{
			get
			{
				return this.highLightValue;
			}
			set
			{
				if (value < 0)
				{
					this.highLightValue = 0;
				}
				else if (value > 255)
				{
					this.highLightValue = 255;
				}
				else
				{
					this.highLightValue = value;
				}
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00034F5B File Offset: 0x0003315B
		public BaseBullet(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			this.HighLightValue = 200;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00034F80 File Offset: 0x00033180
		public override void Ctrl()
		{
			base.Ctrl();
			if (this.HighLightValue > 0)
			{
				this.HighLightValue -= 10;
			}
			else
			{
				this.HighLightValue = 0;
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00034FC4 File Offset: 0x000331C4
		public override void HitCheckAll()
		{
			if (base.MyPlane.HitEnabled)
			{
				if (this.HitCheck(base.MyPlane))
				{
					base.MyPlane.PreMiss();
				}
			}
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00035008 File Offset: 0x00033208
		public override void GiveEndEffect()
		{
			ParticleSmaller particleSmaller = new ParticleSmaller(base.StageData, "光点", base.Position, base.Velocity, base.Direction);
			particleSmaller.LifeTime = 30;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00035044 File Offset: 0x00033244
		public override void Show()
		{
			base.Show();
			if (this.HighLightValue > 0)
			{
				int width = base.TextureObjectDictionary["光点"].Width;
				int height = base.TextureObjectDictionary["光点"].Height;
				SizeF sizeF = new SizeF((float)width * base.Scale, (float)height * base.Scale);
				base.SpriteMain.Draw2D(base.TextureObjectDictionary["光点"].TXTure, base.TextureObjectDictionary["光点"].PosRect, sizeF, base.TextureObjectDictionary["光点"].RotatingCenter, (float)(base.Direction - 1.5707963267948966), base.Position, Color.FromArgb(this.HighLightValue, Color.White));
			}
		}

		// Token: 0x0400020F RID: 527
		private int highLightValue = 0;
	}
}
