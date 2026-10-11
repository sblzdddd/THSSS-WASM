using System;
using System.Drawing;
using Shooting.Planes.Story;

namespace Shooting
{
	// Token: 0x0200001F RID: 31
	internal class EndBoss_Touhou04 : EndBoss_Touhou
	{
		// Token: 0x06000187 RID: 391 RVA: 0x000093E2 File Offset: 0x000075E2
		public EndBoss_Touhou04(StageDataPackage StageData, PointF OriginalPosition, Color Color1, Color Color2)
			: base(StageData, OriginalPosition, Color1, Color2)
		{
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000093F4 File Offset: 0x000075F4
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == base.LifeTime)
			{
				new Story_SSS04_02(base.StageData);
			}
		}
	}
}
