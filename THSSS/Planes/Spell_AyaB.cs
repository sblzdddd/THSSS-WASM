using System;
using System.Drawing;

namespace Shooting
{
	public class Spell_AyaB : BaseSpell
	{
		public Spell_AyaB(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.OriginalPosition = base.MyPlane.OriginalPosition;
			this.tmpPoint = base.MyPlane.OriginalPosition;
			base.LifeTime = 270;
			base.SpellList.Add(this);
			BaseEffect baseEffect = new BaseEffect(StageData, null, base.Position, 0f, 1.5707963267948966);
			baseEffect.TxtureObject = base.MyPlane.TxtureObject;
			baseEffect.TransparentVelocity = -20f;
			baseEffect.ScaleVelocity = 0.1f;
			new BackgroundBlack(StageData, base.Position);
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
			this.tmpRegion = base.MyPlane.Region;
			base.MyPlane.Region = -100;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time > 64 && base.Time <= 96)
			{
				this.Circle = 16;
			}
			else if (base.Time > 96 && base.Time <= 112)
			{
				this.Circle = 8;
			}
			if (base.Time % this.Circle == 0)
			{
				Spell_AyaB_Laser spell_AyaB_Laser = new Spell_AyaB_Laser(base.StageData, base.OriginalPosition, this.Left);
				if (base.Time <= 96)
				{
					BaseEffect baseEffect = new BaseEffect(base.StageData, "Ayaya", new PointF(spell_AyaB_Laser.Position.X + 360f * (float)Math.Cos(spell_AyaB_Laser.Direction), spell_AyaB_Laser.Position.Y + 360f * (float)Math.Sin(spell_AyaB_Laser.Direction)), spell_AyaB_Laser.Velocity, spell_AyaB_Laser.Direction);
					baseEffect.Active = false;
					baseEffect.Mirrored = !this.Left;
					baseEffect.Scale = 1.5f;
					baseEffect.GhostingCount = 5;
				}
				this.Left = !this.Left;
				base.StageData.SoundPlay("se_tan01.wav");
				base.StageData.VibrateStart(8);
			}
			base.ItemList.ForEach(delegate(BaseItem x)
			{
				x.Region = 1000;
				x.Doubled = true;
			});
			if (base.Time == base.LifeTime)
			{
				base.ItemList.ForEach(delegate(BaseItem x)
				{
					x.Obtain = true;
				});
				BaseEffect baseEffect2 = new BaseEffect(base.StageData, null, base.MyPlane.Position, 0f, 1.5707963267948966);
				baseEffect2.TxtureObject = base.MyPlane.TxtureObject;
				baseEffect2.Scale = 3f;
				baseEffect2.ScaleVelocity = -0.1f;
				baseEffect2.LifeTime = 20;
				baseEffect2.TransparentValueF = 0f;
				baseEffect2.TransparentVelocity = 10f;
				base.MyPlane.Region = this.tmpRegion;
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
