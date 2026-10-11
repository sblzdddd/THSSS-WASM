using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000151 RID: 337
	internal class SpellItem_Touhou : BaseItem_Touhou
	{
		// Token: 0x060008AB RID: 2219 RVA: 0x00050AFA File Offset: 0x0004ECFA
		public SpellItem_Touhou(StageDataPackage StageData, PointF p)
			: base(StageData, "SpellItem", p)
		{
			base.Region = 10;
			base.Scale = 1f;
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00050B24 File Offset: 0x0004ED24
		public override void HitCheckAll()
		{
			base.HitCheckAll();
			if (this.HitCheck(base.MyPlane))
			{
				base.ItemList.Remove(this);
				base.MyPlane.SpellExtand();
			}
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00050B67 File Offset: 0x0004ED67
		public override void Show()
		{
			base.Show();
		}
	}
}
