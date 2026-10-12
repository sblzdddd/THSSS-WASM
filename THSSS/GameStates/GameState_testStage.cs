using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	internal class GameState_testStage : BaseStage, IGameState
	{
		public GameState_testStage(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			StageName = "Stage_test";
		}

		public override void Drama()
		{
			if (TimeMain == 1)
			{
				StageData.SetReplayInfo(StageName);
				if (MyPlane == null)
				{
					MyPlane = new BaseMyPlane(StageData, new Point(BoundRect.Width, BoundRect.Height - 50));
				}
				var backgroundRotate = new BackgroundRotate(StageData, "Card02b");
				backgroundRotate.Scale = 1.3f;
				backgroundRotate.TransparentValueF = 255f;
				backgroundRotate.AngularVelocityDegree = -0.5f;
				var backgroundRotate2 = new BackgroundRotate(StageData, "Card02a")
				{
					DirectionDegree = 90.0,
					TransparentValueF = 155f,
					Scale = 1f,
					Active = false,
					AngularVelocityDegree = 0f
				};
				backgroundRotate2.EventGroupList = new List<EventGroup>();
				backgroundRotate2.EventsExecutionList = new List<Execution>();
				var eventGroup = new EventGroup();
				eventGroup.index = 0;
				eventGroup.tag = "0";
				eventGroup.t = 200;
				eventGroup.addtime = 200;
				var @event = new Event();
				@event.EventString = "当前帧=1：不透明度变化到60，正弦，400";
				@event.String2BulletEvent();
				eventGroup.EventList.Add(@event);
				backgroundRotate2.EventGroupList.Add(eventGroup);
				var backgroundRotate3 = new BackgroundRotate(StageData, "Card02c");
				backgroundRotate3.Scale = 0.7f;
				backgroundRotate3.Active = true;
				backgroundRotate3.AngularVelocityDegree = 0.2f;
			}
			if (TimeMain == 50)
			{
			}
			if (TimeMain % 400 == 100)
			{
			}
		}

		public override void BGM_ON()
		{
		}
	}
}
