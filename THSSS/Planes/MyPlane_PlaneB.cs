using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	public class MyPlane_PlaneB : BaseMyPlane
	{
		public MyPlane_PlaneB(StageDataPackage StageData, Point OriginalPosition)
			: base(StageData, OriginalPosition)
		{
			base.WeaponType = "B";
			base.SubPlaneList = new List<BaseSubPlane>
			{
				new SubPlane_PlaneB(StageData, base.Position),
				new SubPlane_PlaneB(StageData, base.Position),
				new SubPlane_PlaneB(StageData, base.Position),
				new SubPlane_PlaneB(StageData, base.Position)
			};
			base.HighSpeed = 4.5f;
			base.LowSpeed = 2f;
		}

		public override void Shoot()
		{
			if (base.SpellList.Count <= 0)
			{
				if (base.Time % 3 == 0)
				{
					base.StageData.SoundPlay("se_plst00.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
					float x = base.Position.X;
					float y = base.Position.Y;
					new BaseMyBullet(base.StageData, "自机子弹", new PointF(x - 7f, y), 30f, -1.5707963267948966);
					new BaseMyBullet(base.StageData, "自机子弹", new PointF(x + 7f, y), 30f, -1.5707963267948966);
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

		public override void SpellShoot()
		{
			new Spell_PlaneB(base.StageData);
		}

		public override void SubPlaneCtrl()
		{
			if (base.KClass.Key_Shift)
			{
				switch (base.PowerLevel)
				{
				case 1:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 30f);
					break;
				case 2:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 12f, base.OriginalPosition.Y - 30f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 12f, base.OriginalPosition.Y - 30f);
					break;
				case 3:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 30f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 15f, base.OriginalPosition.Y - 20f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X - 15f, base.OriginalPosition.Y - 20f);
					break;
				case 4:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 8f, base.OriginalPosition.Y - 35f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 8f, base.OriginalPosition.Y - 35f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X - 22f, base.OriginalPosition.Y - 25f);
					base.SubPlanePoint[3] = new PointF(base.OriginalPosition.X + 22f, base.OriginalPosition.Y - 25f);
					break;
				}
			}
			else
			{
				switch (base.PowerLevel)
				{
				case 1:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 40f);
					break;
				case 2:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 25f, base.OriginalPosition.Y - 20f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 25f, base.OriginalPosition.Y - 20f);
					break;
				case 3:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 40f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 30f, base.OriginalPosition.Y - 20f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X - 30f, base.OriginalPosition.Y - 20f);
					break;
				case 4:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 25f, base.OriginalPosition.Y - 0f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 25f, base.OriginalPosition.Y - 0f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X - 50f, base.OriginalPosition.Y + 5f);
					base.SubPlanePoint[3] = new PointF(base.OriginalPosition.X + 50f, base.OriginalPosition.Y + 5f);
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
