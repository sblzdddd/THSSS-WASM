using System.Drawing;

namespace Shooting
{
	internal class ParticleBack : BaseEffect
	{
		public float AngularVelocity { get; set; }

		public ParticleBack(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Diretion)
		{
			base.StageData = StageData;
			base.Position = Position;
			base.Velocity = Velocity;
			base.Direction = Diretion;
			base.TxtureObject = base.TextureObjectDictionary[textureName];
			base.LifeTime = 200;
			base.TransparentValueF = 0f;
			this.AngularVelocity = 0.01f;
			base.EffectList.Add(this);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			base.Angle += (double)this.AngularVelocity;
			if (0 < base.Time && base.Time < base.LifeTime / 3)
			{
				base.TransparentValueF += (float)(765 / base.LifeTime);
			}
			else if (base.LifeTime * 2 / 3 < base.Time && base.Time < base.LifeTime)
			{
				base.TransparentValueF -= (float)(255 / base.LifeTime * 3);
			}
		}
	}
}
