using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000FF RID: 255
	internal class SpellCard_SSS03_02 : BaseSpellCard
	{
		// Token: 0x06000711 RID: 1809 RVA: 0x0003F7F4 File Offset: 0x0003D9F4
		public SpellCard_SSS03_02(StageDataPackage StageData)
			: base(StageData)
		{
			if (base.Difficulty < DifficultLevel.Hard)
			{
				base.SC_Name = "火光「火精灵之灾」";
			}
			else
			{
				base.SC_Name = "火光「午夜的大火灾」";
			}
			base.BaseScore = 20000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
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
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			FullPic2 fullPic = new FullPic2(StageData, "FaceKoreirei_ct");
			fullPic.Scale = 0.8f;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0003F9F0 File Offset: 0x0003DBF0
		public override void Shoot()
		{
			base.Boss.Armon = 0.25f;
			if (base.Time > 100)
			{
				base.Boss.MoveUpDown();
			}
			if (base.Time == 150)
			{
				base.Boss.OnAction = 10000;
				string text;
				switch (base.Difficulty)
				{
				case DifficultLevel.Easy:
					text = ".\\CS\\St03\\道中Boss\\2符E.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St03\\道中Boss\\2符N.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St03\\道中Boss\\2符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St03\\道中Boss\\2符L.mbg";
					break;
				default:
					text = ".\\CS\\St03\\道中Boss\\2符U.mbg";
					break;
				}
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
