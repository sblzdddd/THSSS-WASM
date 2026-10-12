using System.Drawing;

namespace Shooting
{
	public class Spell_Sanae : BaseSpell
	{
		public Spell_Sanae(StageDataPackage StageData)
		{
			this.StageData = StageData;
			Position = MyPlane.Position;
			Damage = 36;
			Region = 4;
			LifeTime = 300;
			ScaleVelocity = 0.1f;
			MaxScale = 200f;
			new BackgroundBlack(StageData, Position);
			StageData.SoundPlay("se_cat00.wav");
			StageData.SoundPlay("se_nep00.wav");
			SpellList.Add(this);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time == 1)
			{
				BaseEffect baseEffect = new BaseEffect(StageData, "球", Position, 0f, 0.0)
				{
					ScaleWidth = 0.05f,
					ScaleLength = 0.05f
				};
				baseEffect.ScaleLengthVelocityDictionary.Add(1, 0.016666668f);
				baseEffect.ScaleWidthVelocityDictionary.Add(1, 0.016666668f);
				baseEffect.ScaleLengthVelocityDictionary.Add(120, 1.25f);
				baseEffect.ScaleWidthVelocityDictionary.Add(120, 1.25f);
				baseEffect.TransparentVelocityDictionary.Add(120, -10f);
				baseEffect.AngularVelocityDegree = 10f;
			}
			else if (Time == 120)
			{
				StageData.SoundPlay("se_cat00.wav");
				new EmitterGiveOutEnegy3D(StageData, OriginalPosition, Color.Green);
				StageData.VibrateStart(LifeTime - Time);
				ScaleVelocity = 10f;
			}
			else if (120 < Time && Time <= 170)
			{
				AngularVelocityDegree = 2.5f;
				BaseEffect baseEffect2 = new BaseEffect(StageData, "bullet50_5", Position, 7f, -1.5707963267948966 + Angle);
				baseEffect2.ScaleLength = 15f;
				baseEffect2.ScaleWidth = 0.3f;
				BaseEffect baseEffect3 = new BaseEffect(StageData, "bullet50_5", Position, 7f, -1.5707963267948966 - Angle);
				baseEffect3.ScaleLength = 15f;
				baseEffect3.ScaleWidth = 0.3f;
			}
			else if (Time == 240)
			{
				Damage = 0;
			}
		}
	}
}
