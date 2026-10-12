using System.Drawing;

namespace Shooting
{
	public class EDPetal_Emitter : BaseEffect
	{
		public int Hold { get; set; }

		public EDPetal_Emitter(StageDataPackage StageData, PointF OriginalPosition, string EDText)
			: base(StageData, null, new Point(0, 0), 0f, 1.5707963267948966)
		{
			this.OriginalPosition = OriginalPosition;
			LifeTime = 130;
			this.EDText = EDText;
		}

		public override void Shoot()
		{
			if (Time == 10)
			{
				var baseEffect = new BaseEffect(StageData, EDText, OriginalPosition, 0f, 0.0);
				baseEffect.Scale = Scale;
				baseEffect.Direction = 1.5707963267948966;
				baseEffect.OriginalPosition = OriginalPosition;
				baseEffect.Active = false;
				baseEffect.TransparentValueF = 0f;
				baseEffect.TransparentVelocity = 10f;
				baseEffect.LifeTime = 322 + Hold;
				baseEffect.TransparentVelocityDictionary.Add(300 + Hold, -12f);
			}
			for (var i = 0; i < 5; i++)
			{
				if (Time == 1 + i * 3)
				{
					var edpetal = new EDPetal(StageData, "ED_Petal", OriginalPosition, 0f, 0.0);
					edpetal.DestAngleDegree = (double)(72 * i);
					edpetal.LifeTime = 300 + Hold;
				}
			}
		}

		private string EDText;
	}
}
