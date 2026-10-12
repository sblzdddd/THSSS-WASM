using System.Drawing;

namespace Shooting
{
	internal class SelectBox : BaseEffect
	{
		public SelectBox(StageDataPackage StageData)
			: base(StageData)
		{
			base.TxtureObject = base.TextureObjectDictionary["selectframe2"];
		}

		public override void Show()
		{
			base.SpriteMain.Draw2D(base.TxtureObject.TXTure, new Rectangle((int)base.OriginalPosition.X, (int)base.OriginalPosition.Y, 512, 22), new SizeF(512f, 22f), new PointF(0f, 0f), 0f, base.OriginalPosition, Color.White);
		}
	}
}
