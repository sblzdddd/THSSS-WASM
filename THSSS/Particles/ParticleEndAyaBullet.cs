using System;
using System.Drawing;

namespace Shooting
{
	public class ParticleEndAyaBullet : BaseEffect
	{
		public TextureObject[] TxtureObjects { get; set; }

		public int IndexX { get; set; }

		public ParticleEndAyaBullet(StageDataPackage StageData, string Name, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, null, OriginalPosition, Velocity, Direction)
		{
			base.OriginalPosition = OriginalPosition;
			this.TxtureObjects = new TextureObject[]
			{
				base.TextureObjectDictionary[Name + "00"],
				base.TextureObjectDictionary[Name + "01"],
				base.TextureObjectDictionary[Name + "02"],
				base.TextureObjectDictionary[Name + "03"]
			};
			base.TxtureObject = this.TxtureObjects[0];
			base.LifeTime = 15;
			base.Angle = 1.5707963705062866;
			base.MaxTransparent = 80;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			this.IndexX = base.Time % 16 / 4;
			base.TxtureObject = this.TxtureObjects[this.IndexX];
			base.TransparentValueF -= (float)(base.MaxTransparent / base.LifeTime);
			base.Scale += 0.1f;
		}
	}
}
