using System;
using System.Drawing;
using Shooting.Planes.Story;

namespace Shooting
{
	// Token: 0x02000071 RID: 113
	internal class EndBoss_TouhouEx : EndBoss_Touhou
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x0001B693 File Offset: 0x00019893
		public EndBoss_TouhouEx(StageDataPackage StageData, PointF OriginalPosition, Color Color1, Color Color2)
			: base(StageData, OriginalPosition, Color1, Color2)
		{
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001B6A4 File Offset: 0x000198A4
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == base.LifeTime)
			{
				new Story_SSSEx_02(base.StageData);
			}
		}
	}
}
