using System;
using System.Drawing;
using Shooting.Planes.Story;

namespace Shooting
{
	// Token: 0x020001FF RID: 511
	internal class EndBoss_Touhou05 : EndBoss_Touhou
	{
		// Token: 0x06000B7D RID: 2941 RVA: 0x000722A9 File Offset: 0x000704A9
		public EndBoss_Touhou05(StageDataPackage StageData, PointF OriginalPosition, Color Color1, Color Color2)
			: base(StageData, OriginalPosition, Color1, Color2)
		{
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x000722BC File Offset: 0x000704BC
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == base.LifeTime)
			{
				new Story_SSS05_03(base.StageData);
			}
		}
	}
}
