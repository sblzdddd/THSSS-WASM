using System.Drawing;

namespace Shooting
{
	internal class BeliefItem_Touhou : BaseItem_Touhou
	{
		public BeliefItem_Touhou(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, "Item-2", OriginalPosition)
		{
			base.Region = 5;
		}

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
