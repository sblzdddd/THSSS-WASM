using System.Drawing;

namespace Shooting
{
	public class BaseInterface : BaseObject
	{
		public int Delay { get; set; }

		public BaseInterface(StageDataPackage StageData, string textureName, PointF OriginalPosition)
		{
			base.StageData = StageData;
			if (textureName != null)
			{
				base.TxtureObject = base.TextureObjectDictionary[textureName];
			}
			base.OriginalPosition = OriginalPosition;
			base.TransparentVelocity = 10f;
		}

		public BaseInterface()
		{
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time <= this.Delay)
			{
				base.TransparentValueF = 0f;
			}
		}

		public override void Show()
		{
			base.SpriteMain.Draw2D(base.TxtureObject, 1f, 0f, base.OriginalPosition, base.TxtureObject.LeftTop, Color.FromArgb(base.TransparentValue, base.ColorValue));
		}
	}
}
