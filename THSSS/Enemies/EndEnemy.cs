using System.Drawing;

namespace Shooting
{
	internal class EndEnemy : BaseEffect
	{
		public EndEnemy(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData)
		{
			this.Position = Position;
			this.Velocity = Velocity;
			this.Direction = Direction;
			Active = true;
			TransparentVelocity = -15f;
			ScaleVelocity = 0.1f;
			TxtureObject = TextureObjectDictionary[textureName];
		}

		public override bool OutBoundary()
		{
			return TransparentValueF <= 0f;
		}
	}
}
