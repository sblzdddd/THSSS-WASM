using System.Drawing;

namespace Shooting
{
	public class Spell_Shinmyoumaru : BaseSpell
	{
		public Spell_Shinmyoumaru(StageDataPackage StageData)
		{
			this.StageData = StageData;
			Position = MyPlane.Position;
			Damage = 32;
			Region = 4;
			LifeTime = 300;
			ScaleVelocity = 0.3f;
			MaxScale = 200f;
			new BackgroundBlack(StageData, Position);
			StageData.SoundPlay("se_cat00.wav");
			StageData.SoundPlay("se_nep00.wav");
			SpellList.Add(this);
		}

		public override void Move()
		{
			Position = MyPlane.Position;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (1 < Time && Time < 60)
			{
				var num = 7;
				var num2 = (double)(360f / (float)num);
				for (var i = 0; i < num; i++)
				{
					var baseEffect = new BaseEffect(StageData, "bullet23_0", Position, 1.5f, 0.0)
					{
						DirectionDegree = (double)(Time * 18) + num2 * (double)i,
						ScaleWidth = 0.3f,
						ScaleLength = 0.3f,
						LifeTime = 999
					};
					baseEffect.OutBound = false;
					baseEffect.DirectionVelocityDegree = -2.8f;
					baseEffect.VelocityDictionary.Add(40, 3f);
					baseEffect.AccelerateDictionary.Add(115, 2f);
					baseEffect.AccelerateDictionary.Add(123, 0f);
					baseEffect.DirectionVelocityDegreeDictionary.Add(115, (-80f + (float)Ran.NextPMDouble()) / 45f);
					baseEffect.DirectionVelocityDegreeDictionary.Add(160, 0f);
					baseEffect.ScaleLengthVelocityDictionary.Add(115, 0.29999998f);
					baseEffect.ScaleWidthVelocityDictionary.Add(115, 0.1f);
					baseEffect.ScaleLengthVelocityDictionary.Add(120, 0f);
					baseEffect.ScaleWidthVelocityDictionary.Add(120, 0f);
					baseEffect.SetBinding(this);
					baseEffect = new BaseEffect(StageData, "bullet23_0", Position, 1.5f, 0.0)
					{
						DirectionDegree = (double)(180 - Time * 18) + num2 * (double)i,
						ScaleWidth = 0.3f,
						ScaleLength = 0.3f,
						LifeTime = 999
					};
					baseEffect.OutBound = false;
					baseEffect.DirectionVelocityDegree = 2.8f;
					baseEffect.VelocityDictionary.Add(40, 3f);
					baseEffect.AccelerateDictionary.Add(115, 2f);
					baseEffect.AccelerateDictionary.Add(123, 0f);
					baseEffect.DirectionVelocityDegreeDictionary.Add(115, (80f + (float)Ran.NextPMDouble()) / 45f);
					baseEffect.DirectionVelocityDegreeDictionary.Add(160, 0f);
					baseEffect.ScaleLengthVelocityDictionary.Add(115, 0.29999998f);
					baseEffect.ScaleWidthVelocityDictionary.Add(115, 0.1f);
					baseEffect.ScaleLengthVelocityDictionary.Add(120, 0f);
					baseEffect.ScaleWidthVelocityDictionary.Add(120, 0f);
					baseEffect.SetBinding(this);
				}
			}
			if (130 < Time && Time < 220)
			{
				for (var i = 0; i < 3; i++)
				{
					var baseEffect_CS = new BaseEffect_CS(StageData);
					baseEffect_CS.TxtureObject = TextureObjectDictionary["bullet50_0"];
					baseEffect_CS.OriginalPosition = new PointF((float)(BoundRect.Width / 2) + 200f * (float)Ran.NextPMDouble(), (float)(BoundRect.Height / 2) + 280f * (float)Ran.NextPMDouble());
					baseEffect_CS.Velocity = (float)Ran.NextPMDouble();
					baseEffect_CS.Direction = 1.5707963267948966 * Ran.NextPMDouble();
					baseEffect_CS.Scale = 0.05f;
					baseEffect_CS.LifeTime = 40;
					var eventGroup = new EventGroup();
					eventGroup.index = 0;
					eventGroup.tag = "0";
					eventGroup.t = 15;
					eventGroup.addtime = 15;
					var @event = new Event();
					@event.EventString = "当前帧=1：高比变化到0，正弦，14";
					@event.String2BulletEvent();
					eventGroup.EventList.Add(@event);
					@event = new Event();
					@event.EventString = "当前帧=1：宽比变化到0，正弦，14";
					@event.String2BulletEvent();
					eventGroup.EventList.Add(@event);
					baseEffect_CS.EventGroupList.Add(eventGroup);
					EffectList.Add(baseEffect_CS);
				}
			}
			if (Time == 122)
			{
				ScaleVelocity = 10f;
				new EmitterGiveOutEnegy3D(StageData, OriginalPosition, Color.White);
				StageData.SoundPlay("se_cat00.wav");
				StageData.VibrateStart(LifeTime - Time);
			}
		}
	}
}
