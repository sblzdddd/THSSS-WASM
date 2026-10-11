using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	public class BackgroundBossEx : BaseObject
	{
		public BackgroundBossEx(StageDataPackage StageData)
			: base(StageData, null, new PointF(0f, 0f), 0f, 0.0)
		{
			BackgroundMove2 backgroundMove = new BackgroundMove2(StageData, "底面");
			backgroundMove.DirectionDegree = 90.0;
			new BackgroundSC(StageData, "中层");
			BackgroundRotate backgroundRotate = new BackgroundRotate(StageData, "最上")
			{
				DirectionDegree = 90.0,
				TransparentValueF = 128f,
				Scale = 1f,
				Active = false,
				AngularVelocityDegree = 0.5f
			};
			backgroundRotate.EventGroupList = new List<EventGroup>();
			backgroundRotate.EventsExecutionList = new List<Execution>();
			EventGroup eventGroup = new EventGroup();
			eventGroup.index = 0;
			eventGroup.tag = "0";
			eventGroup.t = 200;
			eventGroup.addtime = 200;
			Event @event = new Event();
			@event.EventString = "当前帧=1：不透明度变化到5，正弦，200";
			@event.String2BulletEvent();
			eventGroup.EventList.Add(@event);
			backgroundRotate.EventGroupList.Add(eventGroup);
		}
	}
}
