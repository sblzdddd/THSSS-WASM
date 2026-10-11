using System;
using System.Drawing;

namespace Shooting
{
	internal class EnemyPlane_TouhouYYY : BaseEnemyPlane_Touhou
	{
		public bool BackFire { get; set; }

		public Color BackFireColor { get; set; }

		public EnemyPlane_TouhouYYY(StageDataPackage StageData, PointF OriginalPosition, float Velocity, double Direction, byte ColorType)
			: base(StageData, null, OriginalPosition, Velocity, Direction, ColorType)
		{
			base.HealthPoint = 30f;
			base.Region = 8;
			base.TxtureObject = base.TextureObjectDictionary["EnemyYYY" + ((int)(ColorType + 4)).ToString() + "_1"];
			base.AngularVelocityDegree = 5f;
		}

		public override void Shoot()
		{
		}

		public override void TextureCtrl()
		{
		}

		public override void GiveEndEffect()
		{
			base.GiveEndEffect();
			base.EffectList.Remove(this.circle0);
			base.EffectList.Remove(this.circle1);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time == 1)
			{
				this.circle0 = new BaseEffect(base.StageData, "EnemyYYY" + ((int)(base.ColorType + 4)).ToString() + "_0", base.Position, 0f, 0.0)
				{
					Active = false,
					AngularVelocityDegree = -5f,
					AngleDegree = 90.0
				};
				this.circle1 = new BaseEffect(base.StageData, "EnemyYYY" + ((int)(base.ColorType + 4)).ToString() + "_1", base.Position, 0f, 0.0)
				{
					Scale = 1.5f,
					AngularVelocityDegree = -10f
				};
				this.circle0.SetBinding(this);
				this.circle1.SetBinding(this);
			}
			if (this.OutBoundary())
			{
				base.EffectList.Remove(this.circle0);
				base.EffectList.Remove(this.circle1);
			}
		}

		private BaseEffect circle0;

		private BaseEffect circle1;
	}
}
