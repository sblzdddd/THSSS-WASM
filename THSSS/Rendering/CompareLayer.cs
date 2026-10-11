using System;
using System.Collections.Generic;

namespace Shooting
{
	// Token: 0x02000025 RID: 37
	public class CompareLayer : IComparer<BaseBullet_Touhou>
	{
		// Token: 0x060001AF RID: 431 RVA: 0x0000A1F4 File Offset: 0x000083F4
		public int Compare(BaseBullet_Touhou b1, BaseBullet_Touhou b2)
		{
			return b1.Layer - b2.Layer;
		}
	}
}
