using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000F2 RID: 242
	internal class GameTitle : BaseEffect
	{
		// Token: 0x06000684 RID: 1668 RVA: 0x0003A410 File Offset: 0x00038610
		public GameTitle(StageDataPackage StageData, string textureName, Point Position, Point DestPoint)
			: base(StageData)
		{
			base.TxtureObject = base.TextureObjectDictionary[textureName];
			base.Position = Position;
			base.DestPoint = DestPoint;
			base.Velocity = 5f;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0003A461 File Offset: 0x00038661
		public override void Ctrl()
		{
			base.Ctrl();
			base.Angle = (double)(-(double)((float)(base.Direction - 1.5707963267948966)));
			base.TransparentValueF += 2f;
		}
	}
}
