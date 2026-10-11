using System;
using System.Collections.Generic;

namespace Shooting
{
	// Token: 0x0200020D RID: 525
	public class CompareDepth : IComparer<BaseParticle3D>
	{
		// Token: 0x06000BA5 RID: 2981 RVA: 0x000739EC File Offset: 0x00071BEC
		public int Compare(BaseParticle3D p1, BaseParticle3D p2)
		{
			return (int)(p1.Depth * 10f - p2.Depth * 10f);
		}
	}
}
