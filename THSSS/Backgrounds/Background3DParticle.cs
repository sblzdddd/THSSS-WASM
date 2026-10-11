using System;
using System.Drawing;

namespace Shooting
{
	internal class Background3DParticle : Background3DObject
	{
		public Background3DParticle(StageDataPackage StageData, string modelName, PointF OriginalPosition, bool InsertAtFirst)
			: base(StageData, modelName, OriginalPosition, InsertAtFirst)
		{
		}

		public override void Show()
		{
			float transparentValueF = base.TransparentValueF;
			this.tr = 50.0 * Math.Sin((double)((float)base.Time / 2f));
			base.TransparentValueF += (float)((int)this.tr);
			base.Show();
			base.TransparentValueF = transparentValueF;
		}

		private double tr;
	}
}
