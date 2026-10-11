using System;
using System.Drawing;

namespace Shooting
{
	internal class EnemyPlane_TouhouMaoYu : BaseEnemyPlane_Touhou
	{
		public bool BackFire { get; set; }

		public Color BackFireColor { get; set; }

		public EnemyPlane_TouhouMaoYu(StageDataPackage StageData, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: base(StageData, null, OriginalPosition, Velocity, Direction, ColorType)
		{
			base.HealthPoint = 15f;
			base.Region = 6;
			base.TxtureObject = base.TextureObjectDictionary["EnemyMaoYu0" + ColorType];
			base.ItemCount = 1;
		}

		public override void Shoot()
		{
		}

		public override void TextureCtrl()
		{
			base.AngleDegree += 15.0;
		}
	}
}
