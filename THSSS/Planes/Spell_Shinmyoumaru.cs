using System;
using System.Drawing;

namespace Shooting
{
	public class Spell_Shinmyoumaru : BaseSpell
	{
		public Spell_Shinmyoumaru(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.Position = base.MyPlane.Position;
			base.Damage = 32;
			base.Region = 4;
			base.LifeTime = 300;
			base.ScaleVelocity = 0.3f;
			base.MaxScale = 200f;
			new BackgroundBlack(StageData, base.Position);
			StageData.SoundPlay("se_cat00.wav");
			StageData.SoundPlay("se_nep00.wav");
			base.SpellList.Add(this);
		}

		public override void Move()
		{
			base.Position = base.MyPlane.Position;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (1 < base.Time && base.Time < 60)
			{
				int num = 7;
				double num2 = (double)(360f / (float)num);
				for (int i = 0; i < num; i++)
				{
					BaseEffect baseEffect = new BaseEffect(base.StageData, "bullet23_0", base.Position, 1.5f, 0.0)
					{
						DirectionDegree = (double)(base.Time * 18) + num2 * (double)i,
						ScaleWidth = 0.3f,
						ScaleLength = 0.3f,
						LifeTime = 999
					};
					baseEffect.OutBound = false;
					baseEffect.DirectionVelocityDegree = -2.8f;
					baseEffect.VelocityDictionary.Add(40, 3f);
					baseEffect.AccelerateDictionary.Add(115, 2f);
					baseEffect.AccelerateDictionary.Add(123, 0f);
					baseEffect.DirectionVelocityDegreeDictionary.Add(115, (-80f + (float)base.Ran.NextPMDouble()) / 45f);
					baseEffect.DirectionVelocityDegreeDictionary.Add(160, 0f);
					baseEffect.ScaleLengthVelocityDictionary.Add(115, 0.29999998f);
					baseEffect.ScaleWidthVelocityDictionary.Add(115, 0.1f);
					baseEffect.ScaleLengthVelocityDictionary.Add(120, 0f);
					baseEffect.ScaleWidthVelocityDictionary.Add(120, 0f);
					baseEffect.SetBinding(this);
					baseEffect = new BaseEffect(base.StageData, "bullet23_0", base.Position, 1.5f, 0.0)
					{
						DirectionDegree = (double)(180 - base.Time * 18) + num2 * (double)i,
						ScaleWidth = 0.3f,
						ScaleLength = 0.3f,
						LifeTime = 999
					};
					baseEffect.OutBound = false;
					baseEffect.DirectionVelocityDegree = 2.8f;
					baseEffect.VelocityDictionary.Add(40, 3f);
					baseEffect.AccelerateDictionary.Add(115, 2f);
					baseEffect.AccelerateDictionary.Add(123, 0f);
					baseEffect.DirectionVelocityDegreeDictionary.Add(115, (80f + (float)base.Ran.NextPMDouble()) / 45f);
					baseEffect.DirectionVelocityDegreeDictionary.Add(160, 0f);
					baseEffect.ScaleLengthVelocityDictionary.Add(115, 0.29999998f);
					baseEffect.ScaleWidthVelocityDictionary.Add(115, 0.1f);
					baseEffect.ScaleLengthVelocityDictionary.Add(120, 0f);
					baseEffect.ScaleWidthVelocityDictionary.Add(120, 0f);
					baseEffect.SetBinding(this);
				}
			}
			if (130 < base.Time && base.Time < 220)
			{
				for (int i = 0; i < 3; i++)
				{
					BaseEffect_CS baseEffect_CS = new BaseEffect_CS(base.StageData);
					baseEffect_CS.TxtureObject = base.TextureObjectDictionary["bullet50_0"];
					baseEffect_CS.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2) + 200f * (float)base.Ran.NextPMDouble(), (float)(base.BoundRect.Height / 2) + 280f * (float)base.Ran.NextPMDouble());
					baseEffect_CS.Velocity = (float)base.Ran.NextPMDouble();
					baseEffect_CS.Direction = 1.5707963267948966 * base.Ran.NextPMDouble();
					baseEffect_CS.Scale = 0.05f;
					baseEffect_CS.LifeTime = 40;
					EventGroup eventGroup = new EventGroup();
					eventGroup.index = 0;
					eventGroup.tag = "0";
					eventGroup.t = 15;
					eventGroup.addtime = 15;
					Event @event = new Event();
					@event.EventString = "当前帧=1：高比变化到0，正弦，14";
					@event.String2BulletEvent();
					eventGroup.EventList.Add(@event);
					@event = new Event();
					@event.EventString = "当前帧=1：宽比变化到0，正弦，14";
					@event.String2BulletEvent();
					eventGroup.EventList.Add(@event);
					baseEffect_CS.EventGroupList.Add(eventGroup);
					base.EffectList.Add(baseEffect_CS);
				}
			}
			if (base.Time == 122)
			{
				base.ScaleVelocity = 10f;
				new EmitterGiveOutEnegy3D(base.StageData, base.OriginalPosition, Color.White);
				base.StageData.SoundPlay("se_cat00.wav");
				base.StageData.VibrateStart(base.LifeTime - base.Time);
			}
		}
	}
}
