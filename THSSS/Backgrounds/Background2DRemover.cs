using System.Drawing;

namespace Shooting
{
	public class Background2DRemover : BaseEffect
	{
		public Background2DRemover(StageDataPackage StageData)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			LifeTime = 20;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			Background.BackgroundList.ForEach(delegate(BaseObject x)
			{
				x.TransparentValueF -= (float)(MaxTransparent / LifeTime);
			});
			if (Time >= LifeTime)
			{
				Background.BackgroundList.Clear();
			}
		}

		public override void Show()
		{
		}
	}
}
