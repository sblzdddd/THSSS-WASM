using System.Drawing;

namespace Shooting
{
	public class TransitionOut : BaseObject
	{
		public TransitionOut(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.TxtureObject = base.TextureObjectDictionary["MenuBackground"];
			base.OriginalPosition = new PointF(0f, 0f);
			base.TransparentValueF = 0f;
			StageData.InterfaceList.Add(this);
			base.LifeTime = 255;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			base.TransparentValueF += (float)(255 / base.LifeTime);
		}

		public override void Show()
		{
			base.SpriteMain.Draw2D(base.TxtureObject, 1f, 0f, base.OriginalPosition, base.TxtureObject.LeftTop, Color.FromArgb(base.TransparentValue, Color.Black));
		}
	}
}
