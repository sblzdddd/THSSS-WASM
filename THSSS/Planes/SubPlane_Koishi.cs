using System;
using System.Drawing;

namespace Shooting
{
	public class SubPlane_Koishi : BaseSubPlane
	{
		public SubPlane_Koishi(StageDataPackage StageData, PointF Position)
			: base(StageData, "KoishiSubPlane01", Position)
		{
			base.AngleDegree = 90.0;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			base.Scale = 1f + 0.1f * (float)Math.Sin((double)((float)base.TimeMain / 40f * 2f) * 3.141592653589793);
			if (base.KClass.Key_Shift)
			{
				base.TxtureObject = base.TextureObjectDictionary["KoishiSubPlane00"];
			}
			else
			{
				base.TxtureObject = base.TextureObjectDictionary["KoishiSubPlane01"];
			}
		}

		public override void Shoot()
		{
			if (base.KClass.Key_Shift)
			{
				int num = 5;
				double num2 = 24.0;
				double num3 = num2 * 3.141592653589793 / 180.0 / (double)(num - 1);
				double num4 = base.ShootDirection - (double)(num - 1) * num3 / 2.0;
				double num5 = num4;
				for (int i = 0; i < num; i++)
				{
					BaseMyBullet_Touhou baseMyBullet_Touhou = new BaseMyBullet_Touhou(base.StageData, "KoishiSubBullet00", base.OriginalPosition, 15f, num5);
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
				double num4 = base.ShootDirection - (double)(num - 1) * num3 / 2.0;
				double num5 = num4;
				for (int i = 0; i < num; i++)
				{
					BaseMyBullet_Touhou baseMyBullet_Touhou2 = new BaseMyBullet_Touhou(base.StageData, "KoishiSubBullet01", base.OriginalPosition, 8f, num5);
					baseMyBullet_Touhou2.Angle = 1.5707963705062866;
					baseMyBullet_Touhou2.Damage = 15;
					num5 += num3;
				}
			}
		}

		public override void Show()
		{
			if (base.Enabled)
			{
				if (base.MyPlane.Power >= 400)
				{
					float scale = base.Scale;
					float transparentValueF = base.TransparentValueF;
					base.Scale = 1.3f;
					base.TransparentValueF = 150f;
					base.Show();
					base.TransparentValueF = transparentValueF;
					base.Scale = scale;
				}
				base.Show();
			}
		}
	}
}
