using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS02_01 : BaseSpellCard
	{
		public SpellCard_SSS02_01(StageDataPackage StageData)
			: base(StageData)
		{
			if (Difficulty < DifficultLevel.Hard)
			{
				SC_Name = "魔矢「无限之箭」";
			}
			else
			{
				SC_Name = "魔矢「轮回的万箭」";
			}
			BaseScore = 15000000L;
			Boss.DestPoint = new Point(BoundRect.Width / 2, 120);
			Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			BackgroundRotate backgroundRotate = new BackgroundRotate(StageData, "Card02b");
			backgroundRotate.Scale = 1.3f;
			backgroundRotate.TransparentValueF = 255f;
			backgroundRotate.AngularVelocityDegree = -0.5f;
			BackgroundMove2 backgroundMove = new BackgroundMove2(StageData, "Card02a")
			{
				DirectionDegree = -80.0,
				TransparentValueF = 155f,
				Scale = 1f,
				Active = false
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
			BackgroundRotate backgroundRotate2 = new BackgroundRotate(StageData, "Card02c");
			backgroundRotate2.Scale = 0.7f;
			backgroundRotate2.Active = true;
			backgroundRotate2.AngularVelocityDegree = 0.2f;
			new BulletTitle(StageData, SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRakuki_ct");
		}

		public override void Shoot()
		{
			Boss.Armon = 0.7f;
			if (Time > 100)
			{
				Boss.MoveUpDown();
			}
			if (Time == 150)
			{
				string text;
				switch (Difficulty)
				{
				case DifficultLevel.Easy:
					text = ".\\CS\\St02\\道中Boss\\1符E.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St02\\道中Boss\\1符N.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St02\\道中Boss\\1符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St02\\道中Boss\\1符L.mbg";
					break;
				default:
					text = ".\\CS\\St02\\道中Boss\\1符L.mbg";
					break;
				}
				CS_Data cs_Data = StageData.LoadCS(text);
				new CSEmitterController(StageData, cs_Data);
			}
		}
	}
}
