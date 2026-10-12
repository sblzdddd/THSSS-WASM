using System.Drawing;

namespace Shooting
{
	public class TextTwinkle : BaseEffect
	{
		public int Circle { get; set; }

		public Color TwinkleColor { get; set; }

		public Color OriginalColor { get; set; }

		public TextTwinkle(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData, textureName, Position, Velocity, Direction)
		{
			base.Active = false;
			base.LifeTime = 150;
			this.Circle = 30;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time <= 1)
			{
				this.OriginalColor = base.ColorValue;
			}
			else if (base.Time < base.LifeTime * 3 / 5)
			{
				if (base.Time % this.Circle == 0)
				{
					base.ColorValue = this.TwinkleColor;
				}
				else if (base.Time % this.Circle == this.Circle / 2)
				{
					base.ColorValue = this.OriginalColor;
				}
			}
			else if (base.LifeTime * 4 / 5 < base.Time && base.Time < base.LifeTime)
			{
				base.TransparentValueF -= (float)(base.MaxTransparent * 5 / base.LifeTime);
			}
		}
	}
}
