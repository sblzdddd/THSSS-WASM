using System;
using System.Drawing;
using Shooting.Planes.Story;

namespace Shooting
{
	// Token: 0x0200004D RID: 77
	internal class EndBoss_Touhou03 : EndBoss_Touhou
	{
		// Token: 0x06000282 RID: 642 RVA: 0x000107D5 File Offset: 0x0000E9D5
		public EndBoss_Touhou03(StageDataPackage StageData, PointF OriginalPosition, Color Color1, Color Color2)
			: base(StageData, OriginalPosition, Color1, Color2)
		{
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000107E8 File Offset: 0x0000E9E8
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == base.LifeTime)
			{
				string name = base.MyPlane.Name;
				if (name != null)
				{
					if (name == "Aya")
					{
						new Story_SSS03_02(base.StageData);
						goto IL_00CA;
					}
					if (name == "Reimu")
					{
						new Story_SSS03_02A(base.StageData);
						goto IL_00CA;
					}
					if (name == "Marisa")
					{
						new Story_SSS03_02B(base.StageData);
						goto IL_00CA;
					}
					if (name == "Sanae")
					{
						new Story_SSS03_02C(base.StageData);
						goto IL_00CA;
					}
					if (name == "Koishi")
					{
						new Story_SSS03_02D(base.StageData);
						goto IL_00CA;
					}
				}
				new Story_SSS03_02X(base.StageData);
				IL_00CA:;
			}
		}
	}
}
