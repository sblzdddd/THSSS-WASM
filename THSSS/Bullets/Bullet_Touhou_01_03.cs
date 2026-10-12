using System.Drawing;

namespace Shooting
{
	public class Bullet_Touhou_01_03 : Bullet_Touhou_01
	{
		public string SubBulletName { get; set; }

		public Bullet_Touhou_01_03(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction, ColorType)
		{
			base.LifeTime = 50;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == base.LifeTime)
			{
				byte colorType = base.ColorType;
				BaseObject baseObject = new BaseObject(base.StageData, this.SubBulletName + colorType.ToString(), new PointF(0f, 0f), 2.5f, 0.0);
				new BulletGroupCircle(base.StageData, baseObject, base.OriginalPosition, base.Direction, (int)base.Difficulty * (int)DifficultLevel.Hard + 3, 0, colorType);
				this.GiveEndEffect();
			}
		}
	}
}
