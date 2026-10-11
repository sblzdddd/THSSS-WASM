using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001E0 RID: 480
	public class MyPlane_AyaB : BaseMyPlane_Touhou
	{
		// Token: 0x06000B0C RID: 2828 RVA: 0x0006D1E0 File Offset: 0x0006B3E0
		public MyPlane_AyaB(StageDataPackage StageData, Point OriginalPosition)
			: base(StageData, OriginalPosition, "Aya")
		{
			base.WeaponType = "B";
			base.SubPlaneList = new List<BaseSubPlane>
			{
				new SubPlane_AyaB(StageData, base.Position),
				new SubPlane_AyaB(StageData, base.Position),
				new SubPlane_AyaB(StageData, base.Position),
				new SubPlane_AyaB(StageData, base.Position)
			};
			base.HighSpeed = 5f;
			base.LowSpeed = 2f;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0006D27C File Offset: 0x0006B47C
		public override void Shoot()
		{
			if (base.SpellList.Count <= 0)
			{
				if (base.Time % 3 == 0)
				{
					base.StageData.SoundPlay("se_plst00.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
					float x = base.OriginalPosition.X;
					float y = base.OriginalPosition.Y;
					new MyBullet_Aya(base.StageData, new PointF(x + 7f, y));
					new MyBullet_Aya(base.StageData, new PointF(x - 7f, y));
				}
				if (base.Time % 5 == 0)
				{
					for (int i = 0; i < base.PowerLevel; i++)
					{
						base.SubPlaneList[i].Shoot();
					}
				}
			}
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0006D37C File Offset: 0x0006B57C
		public override void Ctrl()
		{
			if (base.SpellList.Count <= 0)
			{
				base.Ctrl();
			}
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0006D3A9 File Offset: 0x0006B5A9
		public override void SpellShoot()
		{
			new Spell_AyaB(base.StageData);
			base.StageData.SoundPlay("se_cat00.wav");
			base.StageData.SoundPlay("se_nep00.wav");
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0006D3DC File Offset: 0x0006B5DC
		public override void SubPlaneCtrl()
		{
			if (base.KClass.Key_Shift)
			{
				switch (base.PowerLevel)
				{
				case 1:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 25f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					break;
				case 2:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 6f, base.OriginalPosition.Y - 30f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 6f, base.OriginalPosition.Y - 30f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[1].ShootDirection = -1.5707963267948966;
					break;
				case 3:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 28f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X - 10f, base.OriginalPosition.Y - 22f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X + 10f, base.OriginalPosition.Y - 22f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[1].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[2].ShootDirection = -1.5707963267948966;
					break;
				case 4:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 6f, base.OriginalPosition.Y - 30f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 6f, base.OriginalPosition.Y - 30f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X - 15f, base.OriginalPosition.Y - 22f);
					base.SubPlanePoint[3] = new PointF(base.OriginalPosition.X + 15f, base.OriginalPosition.Y - 22f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[1].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[2].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[3].ShootDirection = -1.5707963267948966;
					break;
				}
			}
			else
			{
				switch (base.PowerLevel)
				{
				case 1:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 35f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					break;
				case 2:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 30f, base.OriginalPosition.Y - 0f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 30f, base.OriginalPosition.Y - 0f);
					base.SubPlaneList[0].ShootDirection = -1.8207963267948966;
					base.SubPlaneList[1].ShootDirection = -1.3207963267948966;
					break;
				case 3:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y + 30f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X - 25f, base.OriginalPosition.Y + 20f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X + 25f, base.OriginalPosition.Y + 20f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[1].ShootDirection = -1.920796320834432;
					base.SubPlaneList[2].ShootDirection = -1.220796332755361;
					break;
				case 4:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 15f, base.OriginalPosition.Y + 26f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 15f, base.OriginalPosition.Y + 26f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X - 30f, base.OriginalPosition.Y + 15f);
					base.SubPlanePoint[3] = new PointF(base.OriginalPosition.X + 30f, base.OriginalPosition.Y + 15f);
					base.SubPlaneList[0].ShootDirection = -1.8207963267948966;
					base.SubPlaneList[1].ShootDirection = -1.3207963267948966;
					base.SubPlaneList[2].ShootDirection = -2.0707963267948966;
					base.SubPlaneList[3].ShootDirection = -1.0707963267948966;
					break;
				}
			}
			for (int i = 0; i < base.PowerLevel; i++)
			{
				base.SubPlaneList[i].Enabled = true;
				base.SubPlaneList[i].DestPoint = base.SubPlanePoint[i];
				base.SubPlaneList[i].Ctrl();
			}
			for (int i = base.PowerLevel; i < base.SubPlaneList.Count; i++)
			{
				base.SubPlaneList[i].Enabled = false;
				base.SubPlaneList[i].Position = base.Position;
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0006DC0C File Offset: 0x0006BE0C
		public override void ShowCenter()
		{
			if (base.SpellList.Count <= 0)
			{
				base.ShowCenter();
			}
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0006DC3C File Offset: 0x0006BE3C
		public override void Show()
		{
			if (base.SpellList.Count <= 0)
			{
				base.Show();
			}
		}
	}
}
