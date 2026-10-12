using System.Drawing;

namespace Shooting
{
	internal class ClearBonus : BaseEffect
	{
		private char[] BonusCharArray { get; set; }

		public ClearBonus(StageDataPackage StageData, int Bonus)
			: base(StageData)
		{
			Layer = 1;
			LifeTime = 150;
			TxtureObject = TextureObjectDictionary["StageClear"];
			MyPlane.Score += (long)Bonus;
			TransparentVelocity = 6f;
			TransparentValueF = 0f;
			OriginalPosition = new PointF((float)(BoundRect.Width / 2), 150f);
			AngleDegree = 90.0;
			BonusCharArray = Bonus.ToString().ToCharArray();
			var num = 0;
			for (var i = 0; i < BonusCharArray.Length; i++)
			{
				num += TextureObjectDictionary["Num" + BonusCharArray[i].ToString()].Width;
			}
			ValuePoint = new PointF(OriginalPosition.X - (float)(num / 2), OriginalPosition.Y + 36f);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time == 100)
			{
				TransparentVelocity = -6f;
			}
			if (Time == 20)
			{
				while (Index < BonusCharArray.Length)
				{
					ValuePoint = new PointF(ValuePoint.X + (float)(TextureObjectDictionary["Num" + BonusCharArray[Index].ToString()].Width / 2), ValuePoint.Y);
					var baseEffect = new BaseEffect(StageData, "Num" + BonusCharArray[Index].ToString(), new PointF(0f, 0f), 0f, 1.5707963267948966)
					{
						Active = false,
						OriginalPosition = ValuePoint,
						ColorValue = Color.FromArgb(255 - Index * 10, 255 - Index * 5, 255 - Index * 10),
						LifeTime = 100,
						TransparentValueF = 0f
					};
					baseEffect.TransparentVelocityDictionary.Add(1, 13f);
					baseEffect.TransparentVelocityDictionary.Add(80, -13f);
					ValuePoint = new PointF(ValuePoint.X + (float)(TextureObjectDictionary["Num" + BonusCharArray[Index].ToString()].Width / 2), ValuePoint.Y);
					Index++;
				}
			}
		}

		private PointF ValuePoint;

		private int Index = 0;
	}
}
