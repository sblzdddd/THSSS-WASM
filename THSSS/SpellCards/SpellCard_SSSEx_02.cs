using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSSEx_02 : BaseSpellCard
	{
		public SpellCard_SSSEx_02(StageDataPackage StageData)
			: base(StageData)
		{
			SC_Name = "柳符「新生的柳叶绿芽」";
			BaseScore = 20000000L;
			Boss.DestPoint = new Point(BoundRect.Width / 2, 120);
			Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			BackgroundRotate backgroundRotate = new BackgroundRotate(StageData, "Card03f");
			backgroundRotate.Scale = 1f;
			backgroundRotate.AngularVelocityDegree = 0f;
			backgroundRotate.AngleDegree = 90.0;
			backgroundRotate.AngleWithDirection = false;
			BackgroundMove2 backgroundMove = new BackgroundMove2(StageData, "Card03d")
			{
				Velocity = 1f,
				DirectionDegree = 0.0,
				TransparentValueF = 155f,
				Scale = 1f,
				Active = false,
				AngularVelocityDegree = 0f
			};
			backgroundMove.EventGroupList = new List<EventGroup>();
			backgroundMove.EventsExecutionList = new List<Execution>();
			EventGroup eventGroup = new EventGroup();
			eventGroup.index = 0;
			eventGroup.tag = "0";
			eventGroup.t = 200;
			eventGroup.addtime = 200;
			Event @event = new Event();
			@event.EventString = "当前帧=1：不透明度变化到60，正弦，400";
			@event.String2BulletEvent();
			eventGroup.EventList.Add(@event);
			backgroundMove.EventGroupList.Add(eventGroup);
			backgroundMove = new BackgroundMove2(StageData, "Card03e")
			{
				Velocity = 1f,
				DirectionDegree = 180.0,
				TransparentValueF = 60f,
				Scale = 1f,
				Active = false,
				AngularVelocityDegree = 0f
			};
			backgroundMove.EventGroupList = new List<EventGroup>();
			backgroundMove.EventsExecutionList = new List<Execution>();
			eventGroup = new EventGroup();
			eventGroup.index = 0;
			eventGroup.tag = "0";
			eventGroup.t = 200;
			eventGroup.addtime = 200;
			@event = new Event();
			@event.EventString = "当前帧=1：不透明度变化到155，正弦，400";
			@event.String2BulletEvent();
			eventGroup.EventList.Add(@event);
			backgroundMove.EventGroupList.Add(eventGroup);
			new BulletTitle(StageData, SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceSeiryuu_ct");
		}

		public override void Shoot()
		{
			Boss.Armon = Boss.ArmonArray[1];
			if (Time > 50)
			{
				Boss.RandomMove(150, 20, 2f, new Rectangle(BoundRect.Width / 2 - 45, 100, 90, 40));
			}
			if (Time == 100)
			{
				string text = ".\\CS\\StEx\\道中Boss\\2符.mbg";
				CS_Data cs_Data = StageData.LoadCS(text);
				CSEmitterController csemitterController = new CSEmitterController(StageData, cs_Data);
				csemitterController.BossBinding = true;
			}
		}
	}
}
