using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000141 RID: 321
	internal class GameState_testStage : BaseStage, IGameState
	{
		// Token: 0x06000872 RID: 2162 RVA: 0x0004DA8B File Offset: 0x0004BC8B
		public GameState_testStage(GlobalDataPackage GlobalData)
			: base(GlobalData)
		{
			base.StageName = "Stage_test";
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0004DAA4 File Offset: 0x0004BCA4
		public override void Drama()
		{
			if (base.TimeMain == 1)
			{
				base.StageData.SetReplayInfo(base.StageName);
				if (base.MyPlane == null)
				{
					base.MyPlane = new BaseMyPlane(base.StageData, new Point(base.BoundRect.Width, base.BoundRect.Height - 50));
				}
				BackgroundRotate backgroundRotate = new BackgroundRotate(base.StageData, "Card02b");
				backgroundRotate.Scale = 1.3f;
				backgroundRotate.TransparentValueF = 255f;
				backgroundRotate.AngularVelocityDegree = -0.5f;
				BackgroundRotate backgroundRotate2 = new BackgroundRotate(base.StageData, "Card02a")
				{
					DirectionDegree = 90.0,
					TransparentValueF = 155f,
					Scale = 1f,
					Active = false,
					AngularVelocityDegree = 0f
				};
				backgroundRotate2.EventGroupList = new List<EventGroup>();
				backgroundRotate2.EventsExecutionList = new List<Execution>();
				EventGroup eventGroup = new EventGroup();
				eventGroup.index = 0;
				eventGroup.tag = "0";
				eventGroup.t = 200;
				eventGroup.addtime = 200;
				Event @event = new Event();
				@event.EventString = "当前帧=1：不透明度变化到60，正弦，400";
				@event.String2BulletEvent();
				eventGroup.EventList.Add(@event);
				backgroundRotate2.EventGroupList.Add(eventGroup);
				BackgroundRotate backgroundRotate3 = new BackgroundRotate(base.StageData, "Card02c");
				backgroundRotate3.Scale = 0.7f;
				backgroundRotate3.Active = true;
				backgroundRotate3.AngularVelocityDegree = 0.2f;
			}
			if (base.TimeMain == 50)
			{
			}
			if (base.TimeMain % 400 == 100)
			{
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0004DC94 File Offset: 0x0004BE94
		public override void BGM_ON()
		{
		}
	}
}
