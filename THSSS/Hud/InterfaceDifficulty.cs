using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020001C8 RID: 456
	public class InterfaceDifficulty : BaseInterface
	{
		// Token: 0x06000A81 RID: 2689 RVA: 0x00066128 File Offset: 0x00064328
		public InterfaceDifficulty(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, null, OriginalPosition)
		{
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x00066138 File Offset: 0x00064338
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

		// Token: 0x06000A83 RID: 2691 RVA: 0x000661F8 File Offset: 0x000643F8
		public override void Show()
		{
			if (base.TxtureObject != null)
			{
				base.SpriteMain.Draw2D(base.TxtureObject, 1f, 0f, base.OriginalPosition, Color.FromArgb(base.TransparentValue, Color.White));
			}
		}
	}
}
