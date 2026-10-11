using System;
using System.Drawing;

namespace Shooting
{
	internal class EndEnemy : BaseEffect
	{
		public EndEnemy(StageDataPackage StageData, string textureName, PointF Position, float Velocity, double Direction)
			: base(StageData)
		{
			base.Position = Position;
			base.Velocity = Velocity;
			base.Direction = Direction;
			base.Active = true;
			base.TransparentVelocity = -15f;
			base.ScaleVelocity = 0.1f;
			base.TxtureObject = base.TextureObjectDictionary[textureName];
		}

		public override bool OutBoundary()
		{
			return base.TransparentValueF <= 0f;
		}
	}
}
