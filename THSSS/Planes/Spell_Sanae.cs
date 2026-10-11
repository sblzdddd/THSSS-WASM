using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200012D RID: 301
	public class Spell_Sanae : BaseSpell
	{
		// Token: 0x06000809 RID: 2057 RVA: 0x00049274 File Offset: 0x00047474
		public Spell_Sanae(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.Position = base.MyPlane.Position;
			base.Damage = 36;
			base.Region = 4;
			base.LifeTime = 300;
			base.ScaleVelocity = 0.1f;
			base.MaxScale = 200f;
			new BackgroundBlack(StageData, base.Position);
			StageData.SoundPlay("se_cat00.wav");
			StageData.SoundPlay("se_nep00.wav");
			base.SpellList.Add(this);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0004930C File Offset: 0x0004750C
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == 1)
			{
				BaseEffect baseEffect = new BaseEffect(base.StageData, "球", base.Position, 0f, 0.0)
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
			else if (base.Time == 120)
			{
				base.StageData.SoundPlay("se_cat00.wav");
				new EmitterGiveOutEnegy3D(base.StageData, base.OriginalPosition, Color.Green);
				base.StageData.VibrateStart(base.LifeTime - base.Time);
				base.ScaleVelocity = 10f;
			}
			else if (120 < base.Time && base.Time <= 170)
			{
				base.AngularVelocityDegree = 2.5f;
				BaseEffect baseEffect2 = new BaseEffect(base.StageData, "bullet50_5", base.Position, 7f, -1.5707963267948966 + base.Angle);
				baseEffect2.ScaleLength = 15f;
				baseEffect2.ScaleWidth = 0.3f;
				BaseEffect baseEffect3 = new BaseEffect(base.StageData, "bullet50_5", base.Position, 7f, -1.5707963267948966 - base.Angle);
				baseEffect3.ScaleLength = 15f;
				baseEffect3.ScaleWidth = 0.3f;
			}
			else if (base.Time == 240)
			{
				base.Damage = 0;
			}
		}
	}
}
