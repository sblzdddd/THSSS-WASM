using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000110 RID: 272
	public class SubPlane_Koishi : BaseSubPlane
	{
		// Token: 0x0600076A RID: 1898 RVA: 0x000437EB File Offset: 0x000419EB
		public SubPlane_Koishi(StageDataPackage StageData, PointF Position)
			: base(StageData, "KoishiSubPlane01", Position)
		{
			base.AngleDegree = 90.0;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00043810 File Offset: 0x00041A10
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

		// Token: 0x0600076C RID: 1900 RVA: 0x000438A4 File Offset: 0x00041AA4
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

		// Token: 0x0600076D RID: 1901 RVA: 0x00043A0C File Offset: 0x00041C0C
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
