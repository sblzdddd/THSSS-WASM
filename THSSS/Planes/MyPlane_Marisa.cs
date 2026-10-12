using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	public class MyPlane_Marisa : BaseMyPlane_Touhou
	{
		public MyPlane_Marisa(StageDataPackage StageData, Point OriginalPosition)
			: base(StageData, OriginalPosition, "Marisa")
		{
			WeaponType = "A";
			SubPlaneList = new List<BaseSubPlane>
			{
				new SubPlane_Marisa(StageData, Position),
				new SubPlane_Marisa(StageData, Position),
				new SubPlane_Marisa(StageData, Position),
				new SubPlane_Marisa(StageData, Position)
			};
			HighSpeed = 5f;
			LowSpeed = 2f;
		}

		public override void Shoot()
		{
			if (SpellList.Count <= 0)
			{
				if (Time % 3 == 0)
				{
					StageData.SoundPlay("se_plst00.wav", OriginalPosition.X / (float)BoundRect.Width);
					float x = OriginalPosition.X;
					float y = OriginalPosition.Y;
					BaseMyBullet_Touhou baseMyBullet_Touhou = new BaseMyBullet_Touhou(StageData, "MarisaBullet00", new PointF(x + 7f, y), 30f, -1.5707963267948966);
					baseMyBullet_Touhou.Damage = 15;
					BaseMyBullet_Touhou baseMyBullet_Touhou2 = new BaseMyBullet_Touhou(StageData, "MarisaBullet00", new PointF(x - 7f, y), 30f, -1.5707963267948966);
					baseMyBullet_Touhou2.Damage = 15;
				}
				for (int i = 0; i < PowerLevel; i++)
				{
					SubPlaneList[i].Shoot();
				}
			}
		}

		public override void SpellShoot()
		{
			new Spell_PlaneB(StageData);
		}

		public override void SubPlaneCtrl()
		{
			if (KClass.Key_Shift)
			{
				switch (PowerLevel)
				{
				case 1:
					SubPlanePoint[0] = new PointF(OriginalPosition.X, OriginalPosition.Y - 40f);
					SubPlaneList[0].ShootDirection = -1.5707963267948966;
					break;
				case 2:
					SubPlanePoint[0] = new PointF(OriginalPosition.X - 31f, OriginalPosition.Y - 10f);
					SubPlanePoint[1] = new PointF(OriginalPosition.X + 31f, OriginalPosition.Y - 10f);
					SubPlaneList[0].ShootDirection = 4.834562028024293;
					SubPlaneList[1].ShootDirection = 4.590215932745087;
					break;
				case 3:
					SubPlanePoint[0] = new PointF(OriginalPosition.X, OriginalPosition.Y - 40f);
					SubPlanePoint[1] = new PointF(OriginalPosition.X - 31f, OriginalPosition.Y - 10f);
					SubPlanePoint[2] = new PointF(OriginalPosition.X + 31f, OriginalPosition.Y - 10f);
					SubPlaneList[0].ShootDirection = -1.5707963267948966;
					SubPlaneList[1].ShootDirection = 4.834562028024293;
					SubPlaneList[2].ShootDirection = 4.590215932745087;
					break;
				case 4:
					SubPlanePoint[0] = new PointF(OriginalPosition.X - 31f, OriginalPosition.Y - 10f);
					SubPlanePoint[1] = new PointF(OriginalPosition.X + 31f, OriginalPosition.Y - 10f);
					SubPlanePoint[2] = new PointF(OriginalPosition.X - 14f, OriginalPosition.Y - 35f);
					SubPlanePoint[3] = new PointF(OriginalPosition.X + 14f, OriginalPosition.Y - 35f);
					SubPlaneList[0].ShootDirection = 4.834562028024293;
					SubPlaneList[1].ShootDirection = 4.590215932745087;
					SubPlaneList[2].ShootDirection = 4.76474885794452;
					SubPlaneList[3].ShootDirection = 4.6600291028248595;
					break;
				}
			}
			else
			{
				switch (PowerLevel)
				{
				case 1:
					SubPlanePoint[0] = new PointF(OriginalPosition.X, OriginalPosition.Y - 50f);
					SubPlaneList[0].ShootDirection = -1.5707963267948966;
					break;
				case 2:
					SubPlanePoint[0] = new PointF(OriginalPosition.X - 31f, OriginalPosition.Y - 0f);
					SubPlanePoint[1] = new PointF(OriginalPosition.X + 31f, OriginalPosition.Y - 0f);
					SubPlaneList[0].ShootDirection = 4.677482395344803;
					SubPlaneList[1].ShootDirection = 4.747295565424577;
					break;
				case 3:
					SubPlanePoint[0] = new PointF(OriginalPosition.X, OriginalPosition.Y - 50f);
					SubPlanePoint[1] = new PointF(OriginalPosition.X - 31f, OriginalPosition.Y + 0f);
					SubPlanePoint[2] = new PointF(OriginalPosition.X + 31f, OriginalPosition.Y + 0f);
					SubPlaneList[0].ShootDirection = -1.5707963267948966;
					SubPlaneList[1].ShootDirection = 4.677482395344803;
					SubPlaneList[2].ShootDirection = 4.747295565424577;
					break;
				case 4:
					SubPlanePoint[0] = new PointF(OriginalPosition.X - 31f, OriginalPosition.Y - 0f);
					SubPlanePoint[1] = new PointF(OriginalPosition.X + 31f, OriginalPosition.Y - 0f);
					SubPlanePoint[2] = new PointF(OriginalPosition.X - 14f, OriginalPosition.Y - 50f);
					SubPlanePoint[3] = new PointF(OriginalPosition.X + 14f, OriginalPosition.Y - 50f);
					SubPlaneList[0].ShootDirection = 4.677482395344803;
					SubPlaneList[1].ShootDirection = 4.747295565424577;
					SubPlaneList[2].ShootDirection = 4.71238898038469;
					SubPlaneList[3].ShootDirection = 4.71238898038469;
					break;
				}
			}
			for (int i = 0; i < PowerLevel; i++)
			{
				SubPlaneList[i].Enabled = true;
				SubPlaneList[i].DestPoint = SubPlanePoint[i];
				SubPlaneList[i].Ctrl();
			}
			for (int i = PowerLevel; i < SubPlaneList.Count; i++)
			{
				SubPlaneList[i].Enabled = false;
				SubPlaneList[i].Position = Position;
			}
		}
	}
}
