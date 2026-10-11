using System;
using System.Drawing;

namespace Shooting
{
	// Token: 0x0200006B RID: 107
	internal class BeliefItem_Touhou : BaseItem_Touhou
	{
		// Token: 0x060003CA RID: 970 RVA: 0x0001A57C File Offset: 0x0001877C
		public BeliefItem_Touhou(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, "Item-2", OriginalPosition)
		{
			base.Region = 5;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0001A598 File Offset: 0x00018798
		public override void HitCheckAll()
		{
			base.HitCheckAll();
			if (this.HitCheck(base.MyPlane))
			{
				base.ItemList.Remove(this);
				base.MyPlane.HighItemScore += 300;
				base.MyPlane.StarPoint += 20;
				base.StageData.SoundPlay("se_item00.wav", base.OriginalPosition.X / (float)base.BoundRect.Width);
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0001A62C File Offset: 0x0001882C
		public override void Show()
		{
			base.Show();
			if (base.OriginalPosition.Y < -8f)
			{
				base.SpriteMain.Draw2D(base.TextureObjectDictionary["Item-10"], 1f, 0f, new PointF(base.Position.X, (float)base.BoundRect.Top), Color.FromArgb(base.TransparentValue, base.ColorValue));
			}
		}
	}
}
