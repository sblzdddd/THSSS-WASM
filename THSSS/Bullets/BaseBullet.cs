using System;
using System.Drawing;

namespace Shooting
{
	public class BaseBullet : BaseBullet_Touhou
	{
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

		public BaseBullet(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			this.HighLightValue = 200;
		}

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

		public override void GiveEndEffect()
		{
			ParticleSmaller particleSmaller = new ParticleSmaller(base.StageData, "光点", base.Position, base.Velocity, base.Direction);
			particleSmaller.LifeTime = 30;
		}

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

		private int highLightValue = 0;
	}
}
