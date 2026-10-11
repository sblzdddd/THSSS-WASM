using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x020000EE RID: 238
	internal class BaseNum : BaseObject
	{
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x00038860 File Offset: 0x00036A60
		// (set) Token: 0x0600066E RID: 1646 RVA: 0x00038877 File Offset: 0x00036A77
		public int Num { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00038880 File Offset: 0x00036A80
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x00038897 File Offset: 0x00036A97
		public int Count { get; set; }

		// Token: 0x06000671 RID: 1649 RVA: 0x000388A0 File Offset: 0x00036AA0
		public BaseNum(StageDataPackage StageData, PointF Position)
			: base(StageData, null, Position, 0f, 0.0)
		{
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x000388BC File Offset: 0x00036ABC
		public override void Show()
		{
			int num = this.Num;
			for (int i = 0; i < this.Count; i++)
			{
				int num2 = num % 10;
				num /= 10;
				TextureObject textureObject = base.TextureObjectDictionary[num2.ToString()];
				base.SpriteMain.Draw2D(textureObject.TXTure, textureObject.PosRect, new SizeF((float)textureObject.Width, (float)textureObject.Height), textureObject.RotatingCenter, 0f, new PointF(base.Position.X + (float)(16 * (this.Count - i - 1)), base.Position.Y), Color.White);
			}
		}

		// Token: 0x0400022E RID: 558
		private const int NumWidth = 16;
	}
}
