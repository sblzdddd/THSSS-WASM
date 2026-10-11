using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000C2 RID: 194
	internal class SpellCard_SSS03_04 : BaseSpellCard
	{
		// Token: 0x06000567 RID: 1383 RVA: 0x0002FDB0 File Offset: 0x0002DFB0
		public SpellCard_SSS03_04(StageDataPackage StageData)
			: base(StageData)
		{
			if (base.Difficulty < DifficultLevel.Hard)
			{
				base.SC_Name = "妖符「柳下妖蝉」";
			}
			else
			{
				base.SC_Name = "妖符「柳下妖蝉」";
			}
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

		// Token: 0x06000568 RID: 1384 RVA: 0x00030054 File Offset: 0x0002E254
		public override void Shoot()
		{
			base.Boss.Armon = 0.7f;
			if (base.Time > 100)
			{
				base.Boss.MoveUpDown();
			}
			if (base.Time == 150)
			{
				string text;
				switch (base.Difficulty)
				{
				case DifficultLevel.Easy:
					text = ".\\CS\\St03\\关底Boss\\2符E.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St03\\关底Boss\\2符N.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St03\\关底Boss\\2符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St03\\关底Boss\\2符L.mbg";
					break;
				default:
					text = ".\\CS\\St03\\关底Boss\\2符U.mbg";
					break;
				}
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
			if ((base.Time - 100) % 200 == 0)
			{
				base.Boss.OnAction = 150;
			}
		}
	}
}
