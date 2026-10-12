using System.Drawing;

namespace Shooting
{
	internal class ParticleBack : BaseEffect
	{
		public float AngularVelocity { get; set; }

		public ParticleBack(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Diretion)
		{
			this.StageData = StageData;
			this.Position = Position;
			this.Velocity = Velocity;
			Direction = Diretion;
			TxtureObject = TextureObjectDictionary[textureName];
			LifeTime = 200;
			TransparentValueF = 0f;
			AngularVelocity = 0.01f;
			EffectList.Add(this);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			Angle += (double)AngularVelocity;
			if (0 < Time && Time < LifeTime / 3)
			{
				TransparentValueF += (float)(765 / LifeTime);
			}
			else if (LifeTime * 2 / 3 < Time && Time < LifeTime)
			{
				TransparentValueF -= (float)(255 / LifeTime * 3);
			}
		}
	}
}
