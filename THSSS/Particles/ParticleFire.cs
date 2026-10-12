using System.Drawing;

namespace Shooting
{
	internal class ParticleFire : BaseEffect
	{
		public int ColorType { get; set; }

		public ParticleFire(StageDataPackage StageData, PointF Position, float Velocity, double Direcity)
			: base(StageData, null, Position, Velocity, Direcity)
		{
			base.LifeTime = 31;
			base.Time = base.Ran.Next(32);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			switch (this.ColorType)
			{
			case 0:
				base.TxtureObject = base.TextureObjectDictionary["FY_Particle0_" + (base.Time % 32 / 4).ToString()];
				break;
			case 1:
				base.TxtureObject = base.TextureObjectDictionary["FY_Particle1_" + (base.Time % 32 / 4).ToString()];
				break;
			case 2:
				base.TxtureObject = base.TextureObjectDictionary["FY_Particle2_" + (base.Time % 32 / 4).ToString()];
				break;
			}
		}
	}
}
