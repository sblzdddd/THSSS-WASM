using System;
using System.Drawing;
using Shooting.Planes.Story;

namespace Shooting
{
	// Token: 0x02000077 RID: 119
	internal class EndBoss_Touhou02 : EndBoss_Touhou
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x0001BB7E File Offset: 0x00019D7E
		public EndBoss_Touhou02(StageDataPackage StageData, PointF OriginalPosition, Color Color1, Color Color2)
			: base(StageData, OriginalPosition, Color1, Color2)
		{
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0001BB90 File Offset: 0x00019D90
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
						new Story_SSS02_02(base.StageData);
						goto IL_00CA;
					}
					if (name == "Reimu")
					{
						new Story_SSS02_02A(base.StageData);
						goto IL_00CA;
					}
					if (name == "Marisa")
					{
						new Story_SSS02_02B(base.StageData);
						goto IL_00CA;
					}
					if (name == "Sanae")
					{
						new Story_SSS02_02C(base.StageData);
						goto IL_00CA;
					}
					if (name == "Koishi")
					{
						new Story_SSS02_02D(base.StageData);
						goto IL_00CA;
					}
				}
				new Story_SSS02_02X(base.StageData);
				IL_00CA:;
			}
		}
	}
}
