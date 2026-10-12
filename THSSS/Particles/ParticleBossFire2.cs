using System.Drawing;

namespace Shooting
{
	internal class ParticleBossFire2 : BaseObject
	{
		public double AngularVelocity { get; set; }

		public ParticleBossFire2(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int LifeTime)
		{
			this.StageData = StageData;
			this.Velocity = Velocity;
			this.Direction = Direction;
			this.OriginalPosition = OriginalPosition;
			this.LifeTime = LifeTime;
			Scale = 0.01f;
			AngularVelocity = (double)Ran.Next(-2, 2) * 3.141592653589793 / 180.0;
			if (textureName != null)
			{
				TxtureObject = TextureObjectDictionary[textureName];
			}
			Active = true;
			Background.BackgroundList.Add(this);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time > 0)
			{
				TransparentValueF -= (float)(255 / LifeTime);
			}
			if (Boss != null)
			{
				OriginalPosition = Boss.OriginalPosition;
			}
			Direction += AngularVelocity;
			if (Time > 0)
			{
				Scale += 0.025f;
				TransparentValueF -= (float)(MaxTransparent / LifeTime);
			}
			if (OutBoundary())
			{
				Background.BackgroundList.Remove(this);
			}
		}
	}
}
