using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000102 RID: 258
	public class BackgroundBoss06 : BaseObject
	{
		// Token: 0x06000717 RID: 1815 RVA: 0x00040054 File Offset: 0x0003E254
		public BackgroundBoss06(StageDataPackage StageData)
			: base(StageData, null, new PointF(0f, 0f), 0f, 0.0)
		{
			new BackgroundSC(StageData, "底色");
			BackgroundSC bsc1 = new BackgroundSC(StageData, "绿光");
			bsc1.CtrlCall = delegate(string x)
			{
				if (bsc1.Time % 300 == 0)
				{
					bsc1.TransparentVelocity = -2.55f;
				}
				else if (bsc1.Time % 300 == 100)
				{
					bsc1.TransparentVelocity = 2.55f;
				}
			};
			BackgroundSC bsc2 = new BackgroundSC(StageData, "蓝光");
			bsc2.CtrlCall = delegate(string x)
			{
				if (bsc2.Time % 300 == 100)
				{
					bsc2.TransparentVelocity = -2.55f;
				}
				else if (bsc2.Time % 300 == 200)
				{
					bsc2.TransparentVelocity = 2.55f;
				}
			};
			BackgroundSC bsc = new BackgroundSC(StageData, "红光");
			bsc.CtrlCall = delegate(string x)
			{
				if (bsc.Time % 300 == 200)
				{
					bsc.TransparentVelocity = -2.55f;
				}
				else if (bsc.Time % 300 == 0)
				{
					bsc.TransparentVelocity = 2.55f;
				}
			};
		}
	}
}
