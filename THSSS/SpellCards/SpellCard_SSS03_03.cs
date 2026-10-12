using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS03_03 : BaseSpellCard
	{
		public SpellCard_SSS03_03(StageDataPackage StageData)
			: base(StageData)
		{
			if (Difficulty < DifficultLevel.Hard)
			{
				SC_Name = "木灵「大屋津姬命」";
			}
			else
			{
				SC_Name = "木魅「枛津姬命」";
			}
			BaseScore = 20000000L;
			Boss.DestPoint = new Point(BoundRect.Width / 2, 220);
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
			Boss.Armon = 0.86f;
			if (Time == 150)
			{
				string text;
				switch (Difficulty)
				{
				case DifficultLevel.Easy:
					text = ".\\CS\\St03\\关底Boss\\1符E.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St03\\关底Boss\\1符N.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St03\\关底Boss\\1符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St03\\关底Boss\\1符L.mbg";
					break;
				default:
					text = ".\\CS\\St03\\关底Boss\\1符U.mbg";
					break;
				}
				CS_Data cs_Data = StageData.LoadCS(text);
				new CSEmitterController(StageData, cs_Data);
			}
			if ((Time - 160) % 363 == 0)
			{
				Boss.OnAction = 250;
			}
		}
	}
}
