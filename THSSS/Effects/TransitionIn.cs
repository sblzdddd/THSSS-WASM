using System.Drawing;

namespace Shooting
{
	public class TransitionIn : BaseObject
	{
		public int Delay { get; set; }

		public TransitionIn(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.TxtureObject = base.TextureObjectDictionary["MenuBackground"];
			base.OriginalPosition = new PointF(0f, 0f);
			base.TransparentValueF = 255f;
			StageData.InterfaceList.Add(this);
			this.Delay = 1;
			base.LifeTime = 50;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time > this.Delay)
			{
				base.TransparentValueF -= (float)(255 / (base.LifeTime - this.Delay));
			}
		}

		public override void Show()
		{
			base.SpriteMain.Draw2D(base.TxtureObject, 1f, 0f, base.OriginalPosition, base.TxtureObject.LeftTop, Color.FromArgb(base.TransparentValue, Color.Black));
		}
	}
}
