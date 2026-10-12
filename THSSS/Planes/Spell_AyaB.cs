using System;
using System.Drawing;

namespace Shooting
{
	public class Spell_AyaB : BaseSpell
	{
		public Spell_AyaB(StageDataPackage StageData)
		{
			this.StageData = StageData;
			OriginalPosition = MyPlane.OriginalPosition;
			tmpPoint = MyPlane.OriginalPosition;
			LifeTime = 270;
			SpellList.Add(this);
			BaseEffect baseEffect = new BaseEffect(StageData, null, Position, 0f, 1.5707963267948966);
			baseEffect.TxtureObject = MyPlane.TxtureObject;
			baseEffect.TransparentVelocity = -20f;
			baseEffect.ScaleVelocity = 0.1f;
			new BackgroundBlack(StageData, Position);
			BackgroundMySC backgroundMySC = new BackgroundMySC(StageData, "76-4");
			backgroundMySC.LifeTime = 350;
			backgroundMySC.MaxTransparent = 180;
			backgroundMySC.TransparentValueF = 0f;
			backgroundMySC.TransparentVelocity = 4f;
			backgroundMySC.ColorValue = Color.Green;
			BackgroundMySC backgroundMySC2 = new BackgroundMySC(StageData, "76-4");
			backgroundMySC2.LifeTime = 350;
			backgroundMySC2.MaxTransparent = 150;
			backgroundMySC2.TransparentValueF = 0f;
			backgroundMySC2.TransparentVelocity = 4f;
			backgroundMySC2.ColorValue = Color.Green;
			backgroundMySC2.AngularVelocityDegree = -0.5f;
			backgroundMySC2.Active = true;
			tmpRegion = MyPlane.Region;
			MyPlane.Region = -100;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time > 64 && Time <= 96)
			{
				Circle = 16;
			}
			else if (Time > 96 && Time <= 112)
			{
				Circle = 8;
			}
			if (Time % Circle == 0)
			{
				Spell_AyaB_Laser spell_AyaB_Laser = new Spell_AyaB_Laser(StageData, OriginalPosition, Left);
				if (Time <= 96)
				{
					BaseEffect baseEffect = new BaseEffect(StageData, "Ayaya", new PointF(spell_AyaB_Laser.Position.X + 360f * (float)Math.Cos(spell_AyaB_Laser.Direction), spell_AyaB_Laser.Position.Y + 360f * (float)Math.Sin(spell_AyaB_Laser.Direction)), spell_AyaB_Laser.Velocity, spell_AyaB_Laser.Direction);
					baseEffect.Active = false;
					baseEffect.Mirrored = !Left;
					baseEffect.Scale = 1.5f;
					baseEffect.GhostingCount = 5;
				}
				Left = !Left;
				StageData.SoundPlay("se_tan01.wav");
				StageData.VibrateStart(8);
			}
			ItemList.ForEach(delegate(BaseItem x)
			{
				x.Region = 1000;
				x.Doubled = true;
			});
			if (Time == LifeTime)
			{
				ItemList.ForEach(delegate(BaseItem x)
				{
					x.Obtain = true;
				});
				BaseEffect baseEffect2 = new BaseEffect(StageData, null, MyPlane.Position, 0f, 1.5707963267948966);
				baseEffect2.TxtureObject = MyPlane.TxtureObject;
				baseEffect2.Scale = 3f;
				baseEffect2.ScaleVelocity = -0.1f;
				baseEffect2.LifeTime = 20;
				baseEffect2.TransparentValueF = 0f;
				baseEffect2.TransparentVelocity = 10f;
				MyPlane.Region = tmpRegion;
			}
		}

		public override void HitCheckAll()
		{
		}

		public override void Show()
		{
		}

		private PointF tmpPoint;

		private int tmpRegion;

		private int Circle = 32;

		private bool Left = true;
	}
}
