using System.Drawing;

namespace Shooting
{
	internal class EmitterTitleStarFall : BaseEffect
	{
		public EmitterTitleStarFall(StageDataPackage StageData)
			: base(StageData, null, new PointF(0f, 0f), 0f, 0.0)
		{
			base.OriginalPosition = new PointF((float)(base.BoundRect.Width - 100), 0f);
		}

		public override void Shoot()
		{
			if (base.Time % 80 == 0)
			{
				new EmitterStarFall01(base.StageData, new PointF(base.OriginalPosition.X + 50f + (float)base.Ran.Next(-200, 200), (float)base.Ran.Next(30)));
			}
		}
	}
}
