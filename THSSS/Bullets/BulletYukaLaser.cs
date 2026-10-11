using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000D9 RID: 217
	internal class BulletYukaLaser : BulletLaser
	{
		// Token: 0x06000603 RID: 1539 RVA: 0x000354A0 File Offset: 0x000336A0
		public BulletYukaLaser(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			base.OriginalPosition = OriginalPosition;
			base.ScaleWidth = 0.05f;
			base.Active = true;
			base.LifeTime = 500;
			base.ColorValue = Color.FromArgb(255, 120, 80);
			base.UnRemoveable = true;
			base.Angle = 0.0;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00035514 File Offset: 0x00033714
		public override void Ctrl()
		{
			base.Ctrl();
			base.Region = (int)((float)base.TxtureObject.Width / 2f * 0.7f);
			if (base.ScaleLength < base.DesScaleLength)
			{
				base.ScaleLength += 0.05f;
			}
			else if (base.ScaleWidth < base.DesScaleWidth)
			{
				base.ScaleWidth += 0.05f;
			}
			if (base.Time < base.LifeTime - 50)
			{
				base.TransparentValueF = (float)(150 + base.Ran.Next(40));
			}
			else
			{
				base.TransparentValueF = (float)((base.LifeTime - base.Time) * 2 - 20 + base.Ran.Next(40));
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00035600 File Offset: 0x00033800
		public override void HitCheckAll()
		{
			if (base.MyPlane.HitEnabled && this.HitCheck(base.MyPlane) && base.Time < base.LifeTime - 50)
			{
				base.MyPlane.PreMiss();
			}
		}
	}
}
