using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x02000018 RID: 24
	internal class LifeItem_Touhou : BaseItem_Touhou
	{
		// Token: 0x0600013F RID: 319 RVA: 0x00008383 File Offset: 0x00006583
		public LifeItem_Touhou(StageDataPackage StageData, PointF p)
			: base(StageData, "Item-6", p)
		{
			base.Region = 10;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000083A0 File Offset: 0x000065A0
		public override void HitCheckAll()
		{
			base.HitCheckAll();
			if (this.HitCheck(base.MyPlane))
			{
				base.ItemList.Remove(this);
				base.MyPlane.Extend();
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000083E4 File Offset: 0x000065E4
		public override void Show()
		{
			base.Show();
			if (base.OriginalPosition.Y < -8f)
			{
				base.SpriteMain.Draw2D(base.TextureObjectDictionary["Item-14"], 1f, 0f, new PointF(base.Position.X, (float)base.BoundRect.Top), Color.FromArgb(base.TransparentValue, base.ColorValue));
			}
		}
	}
}
