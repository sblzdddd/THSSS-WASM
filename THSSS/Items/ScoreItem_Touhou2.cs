using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000F5 RID: 245
	internal class ScoreItem_Touhou2 : BaseItem_Touhou
	{
		// Token: 0x06000699 RID: 1689 RVA: 0x0003AAB8 File Offset: 0x00038CB8
		public ScoreItem_Touhou2(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, "Item-7", OriginalPosition)
		{
			base.Region = 5;
			base.AngularVelocity = 0f;
			base.DirectionDegree += (double)((float)base.Ran.Next(-49, 50) / 10f);
			base.MaxTransparent = 80;
			base.TransparentValueF = 0f;
			base.TransparentVelocity = 16f;
			base.Active = true;
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0003AB38 File Offset: 0x00038D38
		public override void HitCheckAll()
		{
			if (this.HitCheck(base.MyPlane))
			{
				base.ItemList.Remove(this);
				base.StageData.SoundPlay("se_item00.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0003AB98 File Offset: 0x00038D98
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == 40)
			{
				base.Obtain = true;
			}
		}
	}
}
