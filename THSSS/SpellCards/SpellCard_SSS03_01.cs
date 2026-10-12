using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	internal class SpellCard_SSS03_01 : BaseSpellCard
	{
		public SpellCard_SSS03_01(StageDataPackage StageData)
			: base(StageData)
		{
			if (Difficulty < DifficultLevel.Hard)
			{
				SC_Name = "火符「逼近的火焰」";
			}
			else
			{
				SC_Name = "火符「燃烧地狱火海」";
			}
			BaseScore = 20000000L;
			Boss.DestPoint = new Point(BoundRect.Width / 2, 120);
			Boss.Velocity = 4f;
			StageData.SoundPlay("se_cat00.wav");
			BackgroundRotate backgroundRotate = new BackgroundRotate(StageData, "Card03b");
			backgroundRotate.Scale = 1.3f;
			backgroundRotate.TransparentValueF = 255f;
			backgroundRotate.AngularVelocityDegree = -0.5f;
			BackgroundRotate backgroundRotate2 = new BackgroundRotate(StageData, "Card03a")
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
			BackgroundRotate backgroundRotate3 = new BackgroundRotate(StageData, "Card03c");
			backgroundRotate3.Scale = 0.7f;
			backgroundRotate3.Active = true;
			backgroundRotate3.AngularVelocityDegree = 0.2f;
			new BulletTitle(StageData, SC_Name);
			new SpellCardAttack(StageData);
			FullPic2 fullPic = new FullPic2(StageData, "FaceKoreirei_ct");
			fullPic.Scale = 0.8f;
		}

		public override void Shoot()
		{
			Boss.Armon = 0.15f;
			if (Time > 50)
			{
				Boss.RandomMove(150, 20, 2f, new Rectangle(BoundRect.Width / 2 - 45, 100, 90, 40));
			}
			if (Time == 100)
			{
				string text;
				switch (Difficulty)
				{
				case DifficultLevel.Easy:
					text = ".\\CS\\St03\\道中Boss\\1符E.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St03\\道中Boss\\1符N.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St03\\道中Boss\\1符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St03\\道中Boss\\1符L.mbg";
					break;
				default:
					text = ".\\CS\\St03\\道中Boss\\1符U.mbg";
					break;
				}
				CS_Data cs_Data = StageData.LoadCS(text);
				CSEmitterController csemitterController = new CSEmitterController(StageData, cs_Data);
				csemitterController.BossBinding = true;
			}
		}
	}
}
