using System.Drawing;

namespace Shooting
{
	public class ItemGetter : BaseEffect
	{
		public ItemGetter(StageDataPackage StageData)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			LifeTime = 2;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			ItemList.ForEach(delegate(BaseItem x)
			{
				x.Obtain = true;
				x.Doubled = true;
			});
		}

		public override void Show()
		{
		}
	}
}
