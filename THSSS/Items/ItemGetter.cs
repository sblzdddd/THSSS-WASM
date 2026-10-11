using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000076 RID: 118
	public class ItemGetter : BaseEffect
	{
		// Token: 0x06000409 RID: 1033 RVA: 0x0001BB04 File Offset: 0x00019D04
		public ItemGetter(StageDataPackage StageData)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			base.LifeTime = 2;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001BB46 File Offset: 0x00019D46
		public override void Ctrl()
		{
			base.Ctrl();
			base.ItemList.ForEach(delegate(BaseItem x)
			{
				x.Obtain = true;
				x.Doubled = true;
			});
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0001BB7B File Offset: 0x00019D7B
		public override void Show()
		{
		}
	}
}
