using System.Collections.Generic;

namespace Shooting
{
	public class BaseEffect_CS : BaseEffect
	{
		public BaseEffect_CS()
		{
		}

		public BaseEffect_CS(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.EventGroupList = new List<EventGroup>();
			base.EventsExecutionList = new List<Execution>();
		}

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
