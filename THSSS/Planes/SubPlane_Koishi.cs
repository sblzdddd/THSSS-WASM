using System;
using System.Drawing;

namespace Shooting
{
	public class SubPlane_Koishi : BaseSubPlane
	{
		public SubPlane_Koishi(StageDataPackage StageData, PointF Position)
			: base(StageData, "KoishiSubPlane01", Position)
		{
			AngleDegree = 90.0;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			Scale = 1f + 0.1f * (float)Math.Sin((double)((float)TimeMain / 40f * 2f) * 3.141592653589793);
			if (KClass.Key_Shift)
			{
				TxtureObject = TextureObjectDictionary["KoishiSubPlane00"];
			}
			else
			{
				TxtureObject = TextureObjectDictionary["KoishiSubPlane01"];
			}
		}

		public override void Shoot()
		{
			if (KClass.Key_Shift)
			{
				int num = 5;
				double num2 = 24.0;
				double num3 = num2 * 3.141592653589793 / 180.0 / (double)(num - 1);
				double num4 = ShootDirection - (double)(num - 1) * num3 / 2.0;
				double num5 = num4;
				for (int i = 0; i < num; i++)
				{
					BaseMyBullet_Touhou baseMyBullet_Touhou = new BaseMyBullet_Touhou(StageData, "KoishiSubBullet00", OriginalPosition, 15f, num5);
					baseMyBullet_Touhou.Angle = 1.5707963705062866;
					baseMyBullet_Touhou.Damage = 6;
					num5 += num3;
				}
			}
			else
			{
				int num = 15;
				double num2 = 120.0;
				double num3 = num2 * 3.141592653589793 / 180.0 / (double)(num - 1);
				double num4 = ShootDirection - (double)(num - 1) * num3 / 2.0;
				double num5 = num4;
				for (int i = 0; i < num; i++)
				{
					BaseMyBullet_Touhou baseMyBullet_Touhou2 = new BaseMyBullet_Touhou(StageData, "KoishiSubBullet01", OriginalPosition, 8f, num5);
					baseMyBullet_Touhou2.Angle = 1.5707963705062866;
					baseMyBullet_Touhou2.Damage = 15;
					num5 += num3;
				}
			}
		}

		public override void Show()
		{
			if (Enabled)
			{
				if (MyPlane.Power >= 400)
				{
					float scale = Scale;
					float transparentValueF = TransparentValueF;
					Scale = 1.3f;
					TransparentValueF = 150f;
					base.Show();
					TransparentValueF = transparentValueF;
					Scale = scale;
				}
				base.Show();
			}
		}
	}
}
