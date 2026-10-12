using System.Drawing;

namespace Shooting
{
	public class Bullet_Touhou_01 : BaseBullet_Touhou
	{
		public float Velocity0 { get; set; }

		public float Velocity1 { get; set; }

		public int Time1 { get; set; }

		public float Velocity2 { get; set; }

		public int Time2 { get; set; }

		public Bullet_Touhou_01(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction, ColorType)
		{
			this.Velocity0 = Velocity;
		}

		public override void Ctrl()
		{
			if (base.Time < this.Time1)
			{
				base.Accelerate = (this.Velocity1 - this.Velocity0) / (float)this.Time1;
			}
			else if (this.Time1 == base.Time)
			{
				base.Accelerate = 0f;
			}
			else if (this.Time1 < base.Time && base.Time < this.Time2)
			{
				base.Accelerate = (this.Velocity2 - this.Velocity1) / (float)(this.Time2 - this.Time1);
			}
			else if (this.Time2 == base.Time)
			{
				base.Accelerate = 0f;
			}
			base.Ctrl();
		}
	}
}
