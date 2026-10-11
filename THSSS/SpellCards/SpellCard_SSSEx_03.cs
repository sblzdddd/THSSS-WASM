using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000FB RID: 251
	internal class SpellCard_SSSEx_03 : BaseSpellCard
	{
		// Token: 0x06000709 RID: 1801 RVA: 0x0003EFCC File Offset: 0x0003D1CC
		public SpellCard_SSSEx_03(StageDataPackage StageData)
			: base(StageData)
		{
			base.SC_Name = "蝶恋「千年之愿」";
			base.BaseScore = 20000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
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
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceSeiryuu_ct");
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0003F24C File Offset: 0x0003D44C
		public override void Shoot()
		{
			base.Boss.Armon = base.Boss.ArmonArray[2];
			if (base.Time > 50)
			{
				base.Boss.RandomMove(150, 20, 2f, new Rectangle(base.BoundRect.Width / 2 - 45, 100, 90, 40));
			}
			if (base.Time == 100)
			{
				string text = ".\\CS\\StEx\\道中Boss\\3符.mbg";
				CS_Data cs_Data = base.StageData.LoadCS(text);
				CSEmitterController csemitterController = new CSEmitterController(base.StageData, cs_Data);
				csemitterController.BossBinding = true;
			}
		}
	}
}
