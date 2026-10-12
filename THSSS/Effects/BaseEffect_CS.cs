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
			this.StageData = StageData;
			EventGroupList = new List<EventGroup>();
			EventsExecutionList = new List<Execution>();
		}

		public override void EventCtrl()
		{
			EventGroupList.ForEach(delegate(EventGroup a)
			{
				a.Update(this);
			});
			EventsExecutionList.ForEach(delegate(Execution a)
			{
				a.Update(this);
			});
		}
	}
}
