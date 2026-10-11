using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000132 RID: 306
	public class MyPlane_Aya : BaseMyPlane_Touhou
	{
		// Token: 0x0600082C RID: 2092 RVA: 0x0004AC54 File Offset: 0x00048E54
		public MyPlane_Aya(StageDataPackage StageData, Point OriginalPosition)
			: base(StageData, OriginalPosition, "Aya")
		{
			base.WeaponType = "A";
			base.SubPlaneList = new List<BaseSubPlane>
			{
				new SubPlane_Aya(StageData, base.Position),
				new SubPlane_Aya(StageData, base.Position),
				new SubPlane_Aya(StageData, base.Position),
				new SubPlane_Aya(StageData, base.Position)
			};
			base.HighSpeed = 5f;
			base.LowSpeed = 2f;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x0004ACF0 File Offset: 0x00048EF0
		public override void Shoot()
		{
			if (base.Time % 3 == 0)
			{
				base.StageData.SoundPlay("se_plst00.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
				float x = base.OriginalPosition.X;
				float y = base.OriginalPosition.Y;
				new MyBullet_Aya(base.StageData, new PointF(x + 7f, y));
				new MyBullet_Aya(base.StageData, new PointF(x - 7f, y));
			}
			if (!base.KClass.Key_Shift)
			{
				if (base.Time % 4 == 0)
				{
					for (int i = 0; i < base.PowerLevel; i++)
					{
						base.SubPlaneList[i].Shoot();
					}
				}
			}
			else if (base.PowerLevel > 0)
			{
				if (base.Time % (13 - (base.PowerLevel - 1) * 2) == 0)
				{
					new MyBullet_AyaSub(base.StageData, base.OriginalPosition);
				}
			}
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0004AE2E File Offset: 0x0004902E
		public override void SpellShoot()
		{
			new Spell_AyaA(base.StageData);
			base.StageData.SoundPlay("se_nep00.wav");
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0004AE50 File Offset: 0x00049050
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
					base.SubPlaneList[0].ShootDirection = -1.7707963297751288;
					base.SubPlaneList[1].ShootDirection = -1.3707963238146643;
					break;
				case 3:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 28f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X - 10f, base.OriginalPosition.Y - 22f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X + 10f, base.OriginalPosition.Y - 22f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[1].ShootDirection = -1.7707963297751288;
					base.SubPlaneList[2].ShootDirection = -1.3707963238146643;
					break;
				case 4:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 6f, base.OriginalPosition.Y - 30f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 6f, base.OriginalPosition.Y - 30f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X - 15f, base.OriginalPosition.Y - 22f);
					base.SubPlanePoint[3] = new PointF(base.OriginalPosition.X + 15f, base.OriginalPosition.Y - 22f);
					base.SubPlaneList[0].ShootDirection = -1.7707963297751288;
					base.SubPlaneList[1].ShootDirection = -1.3707963238146643;
					base.SubPlaneList[2].ShootDirection = -1.7707963297751288;
					base.SubPlaneList[3].ShootDirection = -1.3707963238146643;
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
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 25f, base.OriginalPosition.Y - 0f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 25f, base.OriginalPosition.Y - 0f);
					base.SubPlaneList[0].ShootDirection = -1.7707963297751288;
					base.SubPlaneList[1].ShootDirection = -1.3707963238146643;
					break;
				case 3:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 35f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X - 30f, base.OriginalPosition.Y + 5f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X + 30f, base.OriginalPosition.Y + 5f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[1].ShootDirection = -1.920796320834432;
					base.SubPlaneList[2].ShootDirection = -1.220796332755361;
					break;
				case 4:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 25f, base.OriginalPosition.Y + 5f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 25f, base.OriginalPosition.Y + 5f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X - 40f, base.OriginalPosition.Y + 10f);
					base.SubPlanePoint[3] = new PointF(base.OriginalPosition.X + 40f, base.OriginalPosition.Y + 10f);
					base.SubPlaneList[0].ShootDirection = -1.720796332755361;
					base.SubPlaneList[1].ShootDirection = -1.420796320834432;
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

		// Token: 0x06000830 RID: 2096 RVA: 0x0004B680 File Offset: 0x00049880
		public override void TextureCtrl()
		{
			base.TextureCtrl();
			if (base.SpellList.Count > 0)
			{
				base.TxtureObject = base.TextureObjectDictionary["Aya36"];
			}
		}
	}
}
