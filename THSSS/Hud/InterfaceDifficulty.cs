using System.Drawing;

namespace Shooting
{
	public class InterfaceDifficulty : BaseInterface
	{
		public InterfaceDifficulty(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, OriginalPosition)
		{
		}

		public override void Ctrl()
		{
			base.Ctrl();
			switch (base.StageData.Difficulty)
			{
			case DifficultLevel.Easy:
				base.TxtureObject = base.TextureObjectDictionary["Easy"];
				break;
			case DifficultLevel.Normal:
				base.TxtureObject = base.TextureObjectDictionary["Normal"];
				break;
			case DifficultLevel.Hard:
				base.TxtureObject = base.TextureObjectDictionary["Hard"];
				break;
			case DifficultLevel.Lunatic:
				base.TxtureObject = base.TextureObjectDictionary["Lunatic"];
				break;
			case DifficultLevel.Extra:
				base.TxtureObject = base.TextureObjectDictionary["Extra"];
				break;
			}
		}

		public override void Show()
		{
			if (base.TxtureObject != null)
			{
				base.SpriteMain.Draw2D(base.TxtureObject, 1f, 0f, base.OriginalPosition, Color.FromArgb(base.TransparentValue, Color.White));
			}
		}
	}
}
