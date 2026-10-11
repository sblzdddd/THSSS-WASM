using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001E2 RID: 482
	internal class StarItem_Touhou : BaseItem_Touhou
	{
		// Token: 0x06000B15 RID: 2837 RVA: 0x0006DCEC File Offset: 0x0006BEEC
		public StarItem_Touhou(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, "SmallStar", OriginalPosition)
		{
			base.Region = 5;
			base.AngularVelocity = 0f;
			base.DirectionDegree += (double)((float)base.Ran.Next(-49, 50) / 10f);
			base.MaxTransparent = 80;
			base.TransparentValueF = 0f;
			base.TransparentVelocity = 16f;
			base.Active = true;
			base.AngleWithDirection = false;
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0006DD74 File Offset: 0x0006BF74
		public override void HitCheckAll()
		{
			if (this.HitCheck(base.MyPlane))
			{
				base.ItemList.Remove(this);
				base.MyPlane.StarPoint++;
				base.MyPlane.Score += 100L;
				if (++base.MyPlane.GreenPoint % 2 == 0)
				{
					base.MyPlane.HighItemScore += 10;
				}
				base.StageData.SoundPlay("se_item00.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
			}
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0006DE3C File Offset: 0x0006C03C
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == 40)
			{
				base.Obtain = true;
			}
			base.Angle = 0.0;
		}
	}
}
