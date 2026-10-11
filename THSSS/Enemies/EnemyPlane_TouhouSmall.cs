using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000070 RID: 112
	internal class EnemyPlane_TouhouSmall : BaseEnemyPlane_Touhou
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0001B504 File Offset: 0x00019704
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0001B51B File Offset: 0x0001971B
		protected bool BackFire { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0001B524 File Offset: 0x00019724
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x0001B53B File Offset: 0x0001973B
		protected Color BackFireColor { get; set; }

		// Token: 0x060003F8 RID: 1016 RVA: 0x0001B544 File Offset: 0x00019744
		public EnemyPlane_TouhouSmall(StageDataPackage StageData, string EnemyName, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: base(StageData, EnemyName, OriginalPosition, Velocity, Direction, ColorType)
		{
			base.HealthPoint = 10f;
			base.ItemCount = 1;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001B56C File Offset: 0x0001976C
		public EnemyPlane_TouhouSmall(StageDataPackage StageData, string EnemyName, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, EnemyName, OriginalPosition, Velocity, Direction)
		{
			base.HealthPoint = 20f;
			base.ItemCount = 1;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001B594 File Offset: 0x00019794
		public override void Show()
		{
			if (this.BackFire)
			{
				TextureObject textureObject = base.TextureObjectDictionary["GhostFire_" + base.Time % 32 / 4];
				base.SpriteMain.Draw2D(textureObject, base.ScaleWidth, base.ScaleLength, 0f, base.Position, Color.FromArgb(192, this.BackFireColor), base.Mirrored);
			}
			base.Show();
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001B618 File Offset: 0x00019818
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
