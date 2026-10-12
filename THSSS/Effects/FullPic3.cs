using System.Drawing;

namespace Shooting
{
	internal class FullPic3 : BaseObject
	{
		public FullPic3(StageDataPackage StageData, string textureName)
		{
			base.StageData = StageData;
			base.Position = new PointF((float)(base.BoundRect.Left + base.BoundRect.Width / 2), (float)(base.BoundRect.Bottom + 100));
			base.Velocity = 9f;
			base.DirectionDegree = -90.0;
			base.Active = false;
			base.LifeTime = 200;
			base.Accelerate = -0.2f;
			this.AccelerateDictionary.Add(90, 0.1f);
			this.TransparentVelocityDictionary.Add(90, -10f);
			base.AngleWithDirection = false;
			base.AngleDegree = 90.0;
			base.TxtureObject = base.TextureObjectDictionary[textureName];
			base.Background2.BackgroundList.Add(this);
			base.OutsideRegion = 500;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Velocity < 1.2f)
			{
				base.Velocity = 1.2f;
			}
		}
	}
}
