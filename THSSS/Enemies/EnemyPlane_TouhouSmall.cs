using System;
using System.Drawing;

namespace Shooting
{
	internal class EnemyPlane_TouhouSmall : BaseEnemyPlane_Touhou
	{
		protected bool BackFire { get; set; }

		protected Color BackFireColor { get; set; }

		public EnemyPlane_TouhouSmall(StageDataPackage StageData, string EnemyName, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: base(StageData, EnemyName, OriginalPosition, Velocity, Direction, ColorType)
		{
			base.HealthPoint = 10f;
			base.ItemCount = 1;
		}

		public EnemyPlane_TouhouSmall(StageDataPackage StageData, string EnemyName, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, EnemyName, OriginalPosition, Velocity, Direction)
		{
			base.HealthPoint = 20f;
			base.ItemCount = 1;
		}

		public override void Show()
		{
			if (this.BackFire)
			{
				TextureObject textureObject = base.TextureObjectDictionary["GhostFire_" + base.Time % 32 / 4];
				base.SpriteMain.Draw2D(textureObject, base.ScaleWidth, base.ScaleLength, 0f, base.Position, Color.FromArgb(192, this.BackFireColor), base.Mirrored);
			}
			base.Show();
		}

		public void SetBackFire()
		{
			this.BackFire = true;
			switch (base.ColorType)
			{
			case 0:
				this.BackFireColor = Color.Red;
				break;
			case 1:
				this.BackFireColor = Color.Blue;
				break;
			case 2:
				this.BackFireColor = Color.Green;
				break;
			case 3:
				this.BackFireColor = Color.Yellow;
				break;
			default:
				this.BackFireColor = Color.White;
				break;
			}
		}
	}
}
