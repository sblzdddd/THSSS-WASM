using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	public class BackgroundBoss04 : BaseObject
	{
		public BackgroundBoss04(StageDataPackage StageData)
			: base(StageData, null, new PointF(0f, 0f), 0f, 0.0)
		{
			new BackgroundSC(StageData, "Card04c");
			BackgroundRotate backgroundRotate = new BackgroundRotate(StageData, "Card04a")
			{
				DirectionDegree = 90.0,
				TransparentValueF = 128f,
				Scale = 1f,
				Active = false,
				AngularVelocityDegree = 0f
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
			backgroundRotate = new BackgroundRotate(StageData, "Card04b")
			{
				DirectionDegree = 90.0,
				TransparentValueF = 128f,
				Scale = 1f,
				Active = false,
				AngularVelocityDegree = 0f
			};
			backgroundRotate.EventGroupList = new List<EventGroup>();
			backgroundRotate.EventsExecutionList = new List<Execution>();
			eventGroup = new EventGroup();
			eventGroup.index = 0;
			eventGroup.tag = "0";
			eventGroup.t = 200;
			eventGroup.addtime = 200;
			@event = new Event();
			@event.EventString = "当前帧=1：不透明度变化到255，正弦，200";
			@event.String2BulletEvent();
			eventGroup.EventList.Add(@event);
			backgroundRotate.EventGroupList.Add(eventGroup);
		}
	}
}
