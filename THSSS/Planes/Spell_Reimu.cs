using System;
using System.Drawing;

namespace Shooting
{
	public class Spell_Reimu : BaseSpell
	{
		public Spell_Reimu(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.Position = base.MyPlane.Position;
			base.Damage = 40;
			base.Region = 64;
			base.LifeTime = 260;
			base.Scale = 0f;
			new BackgroundBlack(StageData, base.Position);
			new EmitterGiveOutEnegy3D(StageData, base.OriginalPosition, Color.FromArgb(255, 100, 100));
			StageData.SoundPlay("se_cat00.wav");
			StageData.SoundPlay("se_nep00.wav");
			base.TxtureObject = base.TextureObjectDictionary["YINYANGYU"];
			base.Velocity = 2.2f;
			base.DirectionDegree = -90.0;
			base.AngularVelocityDegree = -3f;
			base.Accelerate = -base.Velocity / 210f;
			base.SpellList.Add(this);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (1 < base.Time && base.Time < 50)
			{
				base.ScaleVelocity += 0.0004f;
			}
			else if (150 < base.Time && base.Time < 240)
			{
				base.ScaleVelocity -= 0.0004f;
			}
			else if (base.Time == 240)
			{
				base.ScaleVelocity = -base.Scale / 20f;
			}
			else if (base.Time == 260)
			{
				base.ScaleVelocity = 0f;
			}
			if (base.Time == 22)
			{
				base.StageData.VibrateStart(base.LifeTime - base.Time);
			}
			if (base.Time == 260)
			{
				new EmitterGiveOutEnegy3D(base.StageData, base.OriginalPosition, Color.FromArgb(150, 150, 255));
				new EmitterGiveOutEnegy3D(base.StageData, base.OriginalPosition, Color.FromArgb(150, 150, 255));
				new EmitterGiveOutEnegy3D(base.StageData, base.OriginalPosition, Color.FromArgb(150, 150, 255));
				base.StageData.SoundPlay("se_cat00.wav");
			}
			if (base.Time % 12 == 0)
			{
				int num = 60;
				double num2 = (double)(360f / (float)num);
				float num3 = ((base.Time < 150) ? (2f + (float)base.Time * 4f / 150f) : 6f);
				for (int i = 0; i < num; i++)
				{
					BaseEffect baseEffect = new BaseEffect(base.StageData, "yuan_9", base.Position, num3, 0.0)
					{
						DirectionDegree = num2 * (double)i,
						ScaleWidth = 0.4f,
						ScaleLength = 0.13f,
						LifeTime = 60,
						TransparentValueF = 0f,
						ColorValue = Color.FromArgb(100, 100, 200)
					};
					baseEffect.AccelerateDictionary.Add(1, -(num3 - 0.5f) / 50f);
					baseEffect.AccelerateDictionary.Add(50, 0f);
					baseEffect.ScaleWidthVelocityDictionary.Add(1, 0.01f);
					baseEffect.ScaleWidthVelocityDictionary.Add(50, 0f);
					baseEffect.ScaleLengthVelocityDictionary.Add(1, -baseEffect.ScaleLength / 50f);
					baseEffect.ScaleLengthVelocityDictionary.Add(50, 0f);
					baseEffect.TransparentVelocityDictionary.Add(1, 1f);
					baseEffect.TransparentVelocityDictionary.Add(60, 0f);
				}
			}
		}

		public override void Show()
		{
			base.Show();
			base.SpriteMain.Draw2D(base.TextureObjectDictionary["bullet50_3"], base.ScaleWidth * 0.13f / 0.04f, base.ScaleLength * 0.13f / 0.04f, 0f, base.Position, Color.FromArgb(76, base.ColorValue), base.Mirrored);
		}
	}
}
