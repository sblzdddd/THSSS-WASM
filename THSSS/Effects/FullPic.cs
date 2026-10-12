using System.Drawing;

namespace Shooting
{
	internal class FullPic : BaseObject
	{
		public FullPic(StageDataPackage StageData, string textureName)
		{
			base.StageData = StageData;
			base.Position = new PointF((float)(base.BoundRect.Left + 250), (float)(base.BoundRect.Top + 150));
			base.Velocity = 2f;
			base.Direction = 1.5707963267948966;
			base.Active = false;
			base.LifeTime = 200;
			base.Accelerate = -0.025f;
			base.TxtureObject = base.TextureObjectDictionary[textureName];
			base.Background2.BackgroundList.Add(this);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Velocity < 0f)
			{
				base.Velocity = 0f;
			}
			if (base.Time > 40)
			{
				base.TransparentVelocity = -5f;
			}
		}
	}
}
