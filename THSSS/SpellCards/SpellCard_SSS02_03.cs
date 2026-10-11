using System;
using System.Collections.Generic;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001D5 RID: 469
	internal class SpellCard_SSS02_03 : BaseSpellCard
	{
		// Token: 0x06000ABA RID: 2746 RVA: 0x00069668 File Offset: 0x00067868
		public SpellCard_SSS02_03(StageDataPackage StageData)
			: base(StageData)
		{
			if (base.Difficulty < DifficultLevel.Hard)
			{
				base.SC_Name = "使魔「自动狩猎装置」";
			}
			else
			{
				base.SC_Name = "魔狩「星夜的狩猎舞曲」";
			}
			base.BaseScore = 15000000L;
			base.Boss.DestPoint = new Point(base.BoundRect.Width / 2, 120);
			base.Boss.Velocity = 4f;
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
			new BulletTitle(StageData, base.SC_Name);
			new SpellCardAttack(StageData);
			new FullPic2(StageData, "FaceRakuki_ct");
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00069844 File Offset: 0x00067A44
		public override void Shoot()
		{
			base.Boss.Armon = 0.8f;
			if (base.Time > 100)
			{
				base.Boss.RandomMove1(180, 1.2f);
			}
			if (base.Time == 150)
			{
				string text;
				switch (base.Difficulty)
				{
				case DifficultLevel.Easy:
					text = ".\\CS\\St02\\关底Boss\\2符E.mbg";
					break;
				case DifficultLevel.Normal:
					text = ".\\CS\\St02\\关底Boss\\2符N.mbg";
					break;
				case DifficultLevel.Hard:
					text = ".\\CS\\St02\\关底Boss\\2符H.mbg";
					break;
				case DifficultLevel.Lunatic:
					text = ".\\CS\\St02\\关底Boss\\2符L.mbg";
					break;
				default:
					text = ".\\CS\\St02\\关底Boss\\2符L.mbg";
					break;
				}
				CS_Data cs_Data = base.StageData.LoadCS(text);
				new CSEmitterController(base.StageData, cs_Data);
			}
		}
	}
}
