using System;
using System.Drawing;

namespace Shooting
{
	public class Bullet_Touhou_01_02 : Bullet_Touhou_01
	{
		public string SubBulletName { get; set; }

		public Bullet_Touhou_01_02(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction, ColorType)
		{
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == base.LifeTime)
			{
				new BaseBullet_Touhou(base.StageData, this.SubBulletName, base.OriginalPosition, base.Velocity / 2f, base.Direction, base.ColorType);
				this.GiveEndEffect();
			}
		}
	}
}
