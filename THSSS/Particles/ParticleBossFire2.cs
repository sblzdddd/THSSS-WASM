using System;
using System.Drawing;

namespace Shooting
{
	internal class ParticleBossFire2 : BaseObject
	{
		public double AngularVelocity { get; set; }

		public ParticleBossFire2(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction, int LifeTime)
		{
			base.StageData = StageData;
			base.Velocity = Velocity;
			base.Direction = Direction;
			base.OriginalPosition = OriginalPosition;
			base.LifeTime = LifeTime;
			base.Scale = 0.01f;
			this.AngularVelocity = (double)base.Ran.Next(-2, 2) * 3.141592653589793 / 180.0;
			if (textureName != null)
			{
				base.TxtureObject = base.TextureObjectDictionary[textureName];
			}
			base.Active = true;
			base.Background.BackgroundList.Add(this);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time > 0)
			{
				base.TransparentValueF -= (float)(255 / base.LifeTime);
			}
			if (base.Boss != null)
			{
				base.OriginalPosition = base.Boss.OriginalPosition;
			}
			base.Direction += this.AngularVelocity;
			if (base.Time > 0)
			{
				base.Scale += 0.025f;
				base.TransparentValueF -= (float)(base.MaxTransparent / base.LifeTime);
			}
			if (this.OutBoundary())
			{
				base.Background.BackgroundList.Remove(this);
			}
		}
	}
}
