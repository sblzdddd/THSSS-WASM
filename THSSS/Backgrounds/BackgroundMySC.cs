using System.Drawing;

namespace Shooting
{
	public class BackgroundMySC : BaseObject
	{
		public BackgroundMySC(StageDataPackage StageData, string textureName)
			: base(StageData, textureName, new PointF(0f, 0f), 0f, 0.0)
		{
			base.Background.BackgroundList.Add(this);
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width / 2), (float)(base.BoundRect.Height / 2));
			base.Scale = 2.5f;
			base.AngularVelocityDegree = 0.5f;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time >= base.LifeTime - 50)
			{
				base.TransparentVelocity = -5f;
			}
		}
	}
}
