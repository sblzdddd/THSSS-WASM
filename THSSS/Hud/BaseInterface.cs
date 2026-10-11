using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200000D RID: 13
	public class BaseInterface : BaseObject
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00005DCC File Offset: 0x00003FCC
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00005DE3 File Offset: 0x00003FE3
		public int Delay { get; set; }

		// Token: 0x06000102 RID: 258 RVA: 0x00005DEC File Offset: 0x00003FEC
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

		// Token: 0x06000103 RID: 259 RVA: 0x00005E3B File Offset: 0x0000403B
		public BaseInterface()
		{
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005E48 File Offset: 0x00004048
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time <= this.Delay)
			{
				base.TransparentValueF = 0f;
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005E80 File Offset: 0x00004080
		public override void Show()
		{
			base.SpriteMain.Draw2D(base.TxtureObject, 1f, 0f, base.OriginalPosition, base.TxtureObject.LeftTop, Color.FromArgb(base.TransparentValue, base.ColorValue));
		}
	}
}
