using System.Drawing;

namespace Shooting
{
	internal class Emitter : BaseEffect
	{
		public Emitter(StageDataPackage StageData, PointF Position)
			: base(StageData)
		{
			this.Position = Position;
			Velocity = 0f;
			Direction = 0.0;
		}

		public override void Shoot()
		{
			for (int i = 0; i < 10; i++)
			{
				string text = ((MyPlane.Name == "Aya") ? "MyParticle" : "MyParticle01");
				ParticleSmaller particleSmaller = new ParticleSmaller(StageData, text, Position, (float)(Ran.Next(20, 200) / 10), (double)((float)Ran.Next(360) / 180f) * 3.141592653589793);
				particleSmaller.LifeTime = 20;
				particleSmaller.Scale = (float)Ran.Next(100) / 100f;
				particleSmaller.TransparentValueF = 200f;
			}
		}

		public override bool OutBoundary()
		{
			return Time >= 4;
		}

		public override void Show()
		{
		}
	}
}
