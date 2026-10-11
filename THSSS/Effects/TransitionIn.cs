using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200008A RID: 138
	public class TransitionIn : BaseObject
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00020A24 File Offset: 0x0001EC24
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x00020A3B File Offset: 0x0001EC3B
		public int Delay { get; set; }

		// Token: 0x06000475 RID: 1141 RVA: 0x00020A44 File Offset: 0x0001EC44
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

		// Token: 0x06000476 RID: 1142 RVA: 0x00020ABC File Offset: 0x0001ECBC
		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time > this.Delay)
			{
				base.TransparentValueF -= (float)(255 / (base.LifeTime - this.Delay));
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00020B0A File Offset: 0x0001ED0A
		public override void Show()
		{
			base.SpriteMain.Draw2D(base.TxtureObject, 1f, 0f, base.OriginalPosition, base.TxtureObject.LeftTop, Color.FromArgb(base.TransparentValue, Color.Black));
		}
	}
}
