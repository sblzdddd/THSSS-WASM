using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000111 RID: 273
	public class TransitionOut : BaseObject
	{
		// Token: 0x0600076E RID: 1902 RVA: 0x00043A84 File Offset: 0x00041C84
		public TransitionOut(StageDataPackage StageData)
		{
			base.StageData = StageData;
			base.TxtureObject = base.TextureObjectDictionary["MenuBackground"];
			base.OriginalPosition = new PointF(0f, 0f);
			base.TransparentValueF = 0f;
			StageData.InterfaceList.Add(this);
			base.LifeTime = 255;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00043AF4 File Offset: 0x00041CF4
		public override void Ctrl()
		{
			base.Ctrl();
			base.TransparentValueF += (float)(255 / base.LifeTime);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00043B19 File Offset: 0x00041D19
		public override void Show()
		{
			base.SpriteMain.Draw2D(base.TxtureObject, 1f, 0f, base.OriginalPosition, base.TxtureObject.LeftTop, Color.FromArgb(base.TransparentValue, Color.Black));
		}
	}
}
