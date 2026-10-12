using System.Drawing;

namespace Shooting
{
	internal class FullPic : BaseObject
	{
		public FullPic(StageDataPackage StageData, string textureName)
		{
			this.StageData = StageData;
			Position = new PointF((float)(BoundRect.Left + 250), (float)(BoundRect.Top + 150));
			Velocity = 2f;
			Direction = 1.5707963267948966;
			Active = false;
			LifeTime = 200;
			Accelerate = -0.025f;
			TxtureObject = TextureObjectDictionary[textureName];
			Background2.BackgroundList.Add(this);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Velocity < 0f)
			{
				Velocity = 0f;
			}
			if (Time > 40)
			{
				TransparentVelocity = -5f;
			}
		}
	}
}
