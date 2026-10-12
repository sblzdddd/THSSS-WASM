using System.Drawing;

namespace Shooting
{
	internal class SpellItem_Touhou : BaseItem_Touhou
	{
		public SpellItem_Touhou(StageDataPackage StageData, PointF p)
			: base(StageData, "SpellItem", p)
		{
			base.Region = 10;
			base.Scale = 1f;
		}

		public override void HitCheckAll()
		{
			base.HitCheckAll();
			if (this.HitCheck(base.MyPlane))
			{
				base.ItemList.Remove(this);
				base.MyPlane.SpellExtand();
			}
		}

		public override void Show()
		{
			base.Show();
		}
	}
}
