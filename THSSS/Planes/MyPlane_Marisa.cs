using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001DF RID: 479
	public class MyPlane_Marisa : BaseMyPlane_Touhou
	{
		// Token: 0x06000B08 RID: 2824 RVA: 0x0006C7D0 File Offset: 0x0006A9D0
		public MyPlane_Marisa(StageDataPackage StageData, Point OriginalPosition)
			: base(StageData, OriginalPosition, "Marisa")
		{
			base.WeaponType = "A";
			base.SubPlaneList = new List<BaseSubPlane>
			{
				new SubPlane_Marisa(StageData, base.Position),
				new SubPlane_Marisa(StageData, base.Position),
				new SubPlane_Marisa(StageData, base.Position),
				new SubPlane_Marisa(StageData, base.Position)
			};
			base.HighSpeed = 5f;
			base.LowSpeed = 2f;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0006C86C File Offset: 0x0006AA6C
		public override void Shoot()
		{
			if (base.SpellList.Count <= 0)
			{
				if (base.Time % 3 == 0)
				{
					base.StageData.SoundPlay("se_plst00.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
					float x = base.OriginalPosition.X;
					float y = base.OriginalPosition.Y;
					BaseMyBullet_Touhou baseMyBullet_Touhou = new BaseMyBullet_Touhou(base.StageData, "MarisaBullet00", new PointF(x + 7f, y), 30f, -1.5707963267948966);
					baseMyBullet_Touhou.Damage = 15;
					BaseMyBullet_Touhou baseMyBullet_Touhou2 = new BaseMyBullet_Touhou(base.StageData, "MarisaBullet00", new PointF(x - 7f, y), 30f, -1.5707963267948966);
					baseMyBullet_Touhou2.Damage = 15;
				}
				for (int i = 0; i < base.PowerLevel; i++)
				{
					base.SubPlaneList[i].Shoot();
				}
			}
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0006C9A1 File Offset: 0x0006ABA1
		public override void SpellShoot()
		{
			new Spell_PlaneB(base.StageData);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0006C9B0 File Offset: 0x0006ABB0
		public override void SubPlaneCtrl()
		{
			if (base.KClass.Key_Shift)
			{
				switch (base.PowerLevel)
				{
				case 1:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 40f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					break;
				case 2:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 31f, base.OriginalPosition.Y - 10f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 31f, base.OriginalPosition.Y - 10f);
					base.SubPlaneList[0].ShootDirection = 4.834562028024293;
					base.SubPlaneList[1].ShootDirection = 4.590215932745087;
					break;
				case 3:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 40f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X - 31f, base.OriginalPosition.Y - 10f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X + 31f, base.OriginalPosition.Y - 10f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[1].ShootDirection = 4.834562028024293;
					base.SubPlaneList[2].ShootDirection = 4.590215932745087;
					break;
				case 4:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 31f, base.OriginalPosition.Y - 10f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 31f, base.OriginalPosition.Y - 10f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X - 14f, base.OriginalPosition.Y - 35f);
					base.SubPlanePoint[3] = new PointF(base.OriginalPosition.X + 14f, base.OriginalPosition.Y - 35f);
					base.SubPlaneList[0].ShootDirection = 4.834562028024293;
					base.SubPlaneList[1].ShootDirection = 4.590215932745087;
					base.SubPlaneList[2].ShootDirection = 4.76474885794452;
					base.SubPlaneList[3].ShootDirection = 4.6600291028248595;
					break;
				}
			}
			else
			{
				switch (base.PowerLevel)
				{
				case 1:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 50f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					break;
				case 2:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 31f, base.OriginalPosition.Y - 0f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 31f, base.OriginalPosition.Y - 0f);
					base.SubPlaneList[0].ShootDirection = 4.677482395344803;
					base.SubPlaneList[1].ShootDirection = 4.747295565424577;
					break;
				case 3:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 50f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X - 31f, base.OriginalPosition.Y + 0f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X + 31f, base.OriginalPosition.Y + 0f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[1].ShootDirection = 4.677482395344803;
					base.SubPlaneList[2].ShootDirection = 4.747295565424577;
					break;
				case 4:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 31f, base.OriginalPosition.Y - 0f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 31f, base.OriginalPosition.Y - 0f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X - 14f, base.OriginalPosition.Y - 50f);
					base.SubPlanePoint[3] = new PointF(base.OriginalPosition.X + 14f, base.OriginalPosition.Y - 50f);
					base.SubPlaneList[0].ShootDirection = 4.677482395344803;
					base.SubPlaneList[1].ShootDirection = 4.747295565424577;
					base.SubPlaneList[2].ShootDirection = 4.71238898038469;
					base.SubPlaneList[3].ShootDirection = 4.71238898038469;
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
