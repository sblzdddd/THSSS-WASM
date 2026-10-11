using System;
using System.Collections.Generic;

namespace Shooting
{
	// Token: 0x02000079 RID: 121
	public class BaseEffect_CS : BaseEffect
	{
		// Token: 0x06000413 RID: 1043 RVA: 0x0001BF5E File Offset: 0x0001A15E
		public BaseEffect_CS()
		{
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001BF69 File Offset: 0x0001A169
		public BaseEffect_CS(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.EventGroupList = new List<EventGroup>();
			base.EventsExecutionList = new List<Execution>();
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001BFA8 File Offset: 0x0001A1A8
		public override void EventCtrl()
		{
			base.EventGroupList.ForEach(delegate(EventGroup a)
			{
				a.Update(this);
			});
			base.EventsExecutionList.ForEach(delegate(Execution a)
			{
				a.Update(this);
			});
		}
	}
}
