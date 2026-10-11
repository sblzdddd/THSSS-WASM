using System;
using System.Drawing;

namespace Shooting
{
	public class EDPetal_Emitter : BaseEffect
	{
		public int Hold { get; set; }

		public EDPetal_Emitter(StageDataPackage StageData, PointF OriginalPosition, string EDText)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			base.OriginalPosition = OriginalPosition;
			base.LifeTime = 130;
			this.EDText = EDText;
		}

		public override void Shoot()
		{
			if (base.Time == 10)
			{
				BaseEffect baseEffect = new BaseEffect(base.StageData, this.EDText, base.OriginalPosition, 0f, 0.0);
				baseEffect.Scale = base.Scale;
				baseEffect.Direction = 1.5707963267948966;
				baseEffect.OriginalPosition = base.OriginalPosition;
				baseEffect.Active = false;
				baseEffect.TransparentValueF = 0f;
				baseEffect.TransparentVelocity = 10f;
				baseEffect.LifeTime = 322 + this.Hold;
				baseEffect.TransparentVelocityDictionary.Add(300 + this.Hold, -12f);
			}
			for (int i = 0; i < 5; i++)
			{
				if (base.Time == 1 + i * 3)
				{
					EDPetal edpetal = new EDPetal(base.StageData, "ED_Petal", base.OriginalPosition, 0f, 0.0);
					edpetal.DestAngleDegree = (double)(72 * i);
					edpetal.LifeTime = 300 + this.Hold;
				}
			}
		}

		private string EDText;
	}
}
