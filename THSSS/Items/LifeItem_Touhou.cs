using System;
using System.Drawing;

namespace Shooting
{
	internal class LifeItem_Touhou : BaseItem_Touhou
	{
		public LifeItem_Touhou(StageDataPackage StageData, PointF p)
			: base(StageData, "Item-6", p)
		{
			base.Region = 10;
		}

		public override void HitCheckAll()
		{
			base.HitCheckAll();
			if (this.HitCheck(base.MyPlane))
			{
				base.ItemList.Remove(this);
				base.MyPlane.Extend();
			}
		}

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
