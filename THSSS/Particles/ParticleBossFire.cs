using System.Drawing;

namespace Shooting
{
	internal class ParticleBossFire : BaseObject
	{
		public ParticleBossFire(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int LifeTime)
		{
			this.StageData = StageData;
			this.Velocity = Velocity;
			this.Direction = Direction;
			this.OriginalPosition = OriginalPosition;
			this.LifeTime = LifeTime;
			if (textureName != null)
			{
				TxtureObject = TextureObjectDictionary[textureName];
			}
			Active = true;
			Background.BackgroundList.Add(this);
			ScaleLength = 0.5f;
			ScaleWidth = 0.3f;
			DirectionDegree = (double)(90 + Ran.Next(-4, 5));
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
			if (Time > 0)
			{
				ScaleLength += 0.07f;
				ScaleWidth += 0.03f;
				TransparentValueF -= (float)(MaxTransparent / LifeTime);
			}
			if (OutBoundary())
			{
				Background.BackgroundList.Remove(this);
			}
		}
	}
}
