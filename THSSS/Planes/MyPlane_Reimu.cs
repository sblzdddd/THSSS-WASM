using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000068 RID: 104
	public class MyPlane_Reimu : BaseMyPlane_Touhou
	{
		// Token: 0x060003BF RID: 959 RVA: 0x000198A0 File Offset: 0x00017AA0
		public MyPlane_Reimu(StageDataPackage StageData, Point OriginalPosition)
			: base(StageData, OriginalPosition, "Reimu")
		{
			base.WeaponType = "A";
			base.SubPlaneList = new List<BaseSubPlane>
			{
				new Shinmyoumaru(StageData, base.Position),
				new SubPlane_Reimu(StageData, base.Position),
				new SubPlane_Reimu(StageData, base.Position),
				new SubPlane_Reimu(StageData, base.Position)
			};
			base.HighSpeed = 4.5f;
			base.LowSpeed = 2f;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001993C File Offset: 0x00017B3C
		public override void Shoot()
		{
			if (base.Time % 3 == 0)
			{
				base.StageData.SoundPlay("se_plst00.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
				float x = base.OriginalPosition.X;
				float y = base.OriginalPosition.Y;
				BaseMyBullet_Touhou baseMyBullet_Touhou = new BaseMyBullet_Touhou(base.StageData, "ReimuBullet", new PointF(x - 7f, y), 30f, -1.5707963267948966);
				baseMyBullet_Touhou.Damage = 14;
				BaseMyBullet_Touhou baseMyBullet_Touhou2 = new BaseMyBullet_Touhou(base.StageData, "ReimuBullet", new PointF(x + 7f, y), 30f, -1.5707963267948966);
				baseMyBullet_Touhou2.Damage = 14;
			}
			if (base.Time % 4 == 0)
			{
				for (int i = 0; i < base.PowerLevel; i++)
				{
					base.SubPlaneList[i].Shoot();
				}
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00019A68 File Offset: 0x00017C68
		public override void SpellShoot()
		{
			if (base.KClass.Key_Shift)
			{
				new Spell_Reimu(base.StageData);
			}
			else
			{
				new Spell_Shinmyoumaru(base.StageData);
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00019AA8 File Offset: 0x00017CA8
		public override void SubPlaneCtrl()
		{
			if (base.KClass.Key_Shift)
			{
				switch (base.PowerLevel)
				{
				case 1:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 70f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					break;
				case 2:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 0f, base.OriginalPosition.Y - 70f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 0f, base.OriginalPosition.Y - 70f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[1].ShootDirection = -1.5707963267948966;
					break;
				case 3:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 70f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X - 24f, base.OriginalPosition.Y - 0f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X + 24f, base.OriginalPosition.Y - 0f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[1].ShootDirection = 4.756022211684548;
					base.SubPlaneList[2].ShootDirection = 4.6687557490848315;
					break;
				case 4:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 0f, base.OriginalPosition.Y - 70f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 0f, base.OriginalPosition.Y - 70f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X - 24f, base.OriginalPosition.Y - 0f);
					base.SubPlanePoint[3] = new PointF(base.OriginalPosition.X + 24f, base.OriginalPosition.Y - 0f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[1].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[2].ShootDirection = 4.756022211684548;
					base.SubPlaneList[3].ShootDirection = 4.6687557490848315;
					break;
				}
			}
			else
			{
				switch (base.PowerLevel)
				{
				case 1:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 48f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					break;
				case 2:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 0f, base.OriginalPosition.Y - 48f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 0f, base.OriginalPosition.Y + 48f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[1].ShootDirection = -1.5707963267948966;
					break;
				case 3:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 48f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X - 48f, base.OriginalPosition.Y + 0f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X + 48f, base.OriginalPosition.Y + 0f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[1].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[2].ShootDirection = -1.5707963267948966;
					break;
				case 4:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 48f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y + 48f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X - 48f, base.OriginalPosition.Y + 0f);
					base.SubPlanePoint[3] = new PointF(base.OriginalPosition.X + 48f, base.OriginalPosition.Y + 0f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[1].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[2].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[3].ShootDirection = -1.5707963267948966;
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
	}
}
