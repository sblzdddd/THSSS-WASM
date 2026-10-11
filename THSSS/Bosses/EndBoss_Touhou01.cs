using System;
using System.Drawing;
using Shooting.Planes.Story;

namespace Shooting
{
	// Token: 0x02000093 RID: 147
	internal class EndBoss_Touhou01 : EndBoss_Touhou
	{
		// Token: 0x060004A1 RID: 1185 RVA: 0x00023538 File Offset: 0x00021738
		public EndBoss_Touhou01(StageDataPackage StageData, PointF OriginalPosition, Color Color1, Color Color2)
			: base(StageData, OriginalPosition, Color1, Color2)
		{
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00023548 File Offset: 0x00021748
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
						new Story_SSS01_02(base.StageData);
						goto IL_00CA;
					}
					if (name == "Reimu")
					{
						new Story_SSS01_02A(base.StageData);
						goto IL_00CA;
					}
					if (name == "Marisa")
					{
						new Story_SSS01_02B(base.StageData);
						goto IL_00CA;
					}
					if (name == "Sanae")
					{
						new Story_SSS01_02C(base.StageData);
						goto IL_00CA;
					}
					if (name == "Koishi")
					{
						new Story_SSS01_02D(base.StageData);
						goto IL_00CA;
					}
				}
				new Story_SSS01_02X(base.StageData);
				IL_00CA:;
			}
		}
	}
}
