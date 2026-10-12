using System.Drawing;

namespace Shooting
{
	public class StarPointLineB : StarPointLineG
	{
		public StarPointLineB(StageDataPackage StageData, PointF OriginalPosition)
			: base(StageData, OriginalPosition)
		{
			this.type = "A";
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.MyPlane.LastColor == EnchantmentType.Blue && base.Story == null)
			{
				base.DestPoint = new PointF(66f, 404f);
			}
			else
			{
				base.DestPoint = new PointF(-6f, 404f);
			}
			base.Velocity = 3f;
		}
	}
}
