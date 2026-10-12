using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	public class MyPlane_Sanae : BaseMyPlane_Touhou
	{
		public MyPlane_Sanae(StageDataPackage StageData, Point OriginalPosition)
			: base(StageData, OriginalPosition, "Sanae")
		{
			base.WeaponType = "A";
			base.SubPlaneList = new List<BaseSubPlane>
			{
				new SubPlane_Sanae(StageData, base.Position),
				new SubPlane_Sanae(StageData, base.Position),
				new SubPlane_Sanae(StageData, base.Position)
				{
					Mirrored = true
				},
				new SubPlane_Sanae(StageData, base.Position)
				{
					Mirrored = true
				}
			};
			base.HighSpeed = 4.5f;
			base.LowSpeed = 2f;
		}

		public override void Shoot()
		{
			if (base.Time % 3 == 0)
			{
				base.StageData.SoundPlay("se_plst00.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
				float x = base.OriginalPosition.X;
				float y = base.OriginalPosition.Y;
				new BaseMyBullet_Touhou(base.StageData, "SanaeBullet00", new PointF(x + 7f, y), 30f, -1.5707963267948966);
				new BaseMyBullet_Touhou(base.StageData, "SanaeBullet00", new PointF(x - 7f, y), 30f, -1.5707963267948966);
			}
			if (base.Time % 4 == 0)
			{
				for (int i = 0; i < base.PowerLevel; i++)
				{
					base.SubPlaneList[i].Shoot();
				}
			}
		}

		public override void SpellShoot()
		{
			new Spell_Sanae(base.StageData);
		}

		public override void SubPlaneCtrl()
		{
			if (base.KClass.Key_Shift)
			{
				switch (base.PowerLevel)
				{
				case 1:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 30f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					break;
				case 2:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 10f, base.OriginalPosition.Y - 30f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 10f, base.OriginalPosition.Y - 30f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[1].ShootDirection = -1.5707963267948966;
					break;
				case 3:
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y - 30f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X - 7f, base.OriginalPosition.Y - 30f);
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X + 7f, base.OriginalPosition.Y - 30f);
					base.SubPlaneList[2].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[1].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					break;
				case 4:
					base.SubPlanePoint[3] = new PointF(base.OriginalPosition.X - 6f, base.OriginalPosition.Y - 30f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X + 6f, base.OriginalPosition.Y - 30f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X - 12f, base.OriginalPosition.Y - 30f);
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X + 12f, base.OriginalPosition.Y - 30f);
					base.SubPlaneList[3].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[2].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[1].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					break;
				}
			}
			else
			{
				switch (base.PowerLevel)
				{
				case 1:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y + 30f);
					base.SubPlaneList[0].ShootDirection = -1.5707963267948966;
					break;
				case 2:
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X - 36f, base.OriginalPosition.Y + 0f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X + 36f, base.OriginalPosition.Y - 0f);
					base.SubPlaneList[0].ShootDirection = -1.6607963303711752;
					base.SubPlaneList[1].ShootDirection = -1.4807963232186179;
					break;
				case 3:
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X, base.OriginalPosition.Y + 30f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X - 36f, base.OriginalPosition.Y + 0f);
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X + 36f, base.OriginalPosition.Y + 0f);
					base.SubPlaneList[2].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[1].ShootDirection = -1.6607963303711752;
					base.SubPlaneList[0].ShootDirection = -1.4807963232186179;
					break;
				case 4:
					base.SubPlanePoint[3] = new PointF(base.OriginalPosition.X - 36f, base.OriginalPosition.Y + 0f);
					base.SubPlanePoint[2] = new PointF(base.OriginalPosition.X + 36f, base.OriginalPosition.Y + 0f);
					base.SubPlanePoint[1] = new PointF(base.OriginalPosition.X - 66f, base.OriginalPosition.Y + 0f);
					base.SubPlanePoint[0] = new PointF(base.OriginalPosition.X + 66f, base.OriginalPosition.Y + 0f);
					base.SubPlaneList[3].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[2].ShootDirection = -1.5707963267948966;
					base.SubPlaneList[1].ShootDirection = -1.6607963303711752;
					base.SubPlaneList[0].ShootDirection = -1.4807963232186179;
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
