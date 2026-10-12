using System.Drawing;

namespace Shooting
{
	internal class SpellCardBonus : BaseEffect
	{
		private char[] BonusCharArray { get; set; }

		public SpellCardBonus(StageDataPackage StageData, long Bonus)
			: base(StageData)
		{
			Layer = 1;
			LifeTime = 150;
			TxtureObject = TextureObjectDictionary["GetSpellCardBonus"];
			MyPlane.Score += Bonus;
			TransparentVelocity = 100f;
			TransparentValueF = 0f;
			OriginalPosition = new PointF((float)(BoundRect.Width / 2), 120f);
			AngleDegree = 90.0;
			BonusCharArray = Bonus.ToString().ToCharArray();
			var num = 0;
			for (var i = 0; i < BonusCharArray.Length; i++)
			{
				num += TextureObjectDictionary["Num" + BonusCharArray[i].ToString()].Width;
			}
			ValuePoint = new PointF(OriginalPosition.X - (float)(num / 2), OriginalPosition.Y + 40f);
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (Time == 100)
			{
				TransparentVelocity = -10f;
			}
			if (Time >= 20)
			{
				if (Time % 4 == 0)
				{
					if (Index < BonusCharArray.Length)
					{
						ValuePoint = new PointF(ValuePoint.X + (float)(TextureObjectDictionary["Num" + BonusCharArray[Index].ToString()].Width / 2), ValuePoint.Y);
						var particleBonusNum = new ParticleBonusNum(StageData, "Num" + BonusCharArray[Index].ToString(), ValuePoint);
						particleBonusNum.ColorValue = Color.FromArgb(255 - Index * 10, 255 - Index * 5, 255 - Index * 10);
						ValuePoint = new PointF(ValuePoint.X + (float)(TextureObjectDictionary["Num" + BonusCharArray[Index].ToString()].Width / 2), ValuePoint.Y);
						Index++;
					}
				}
			}
		}

		private PointF ValuePoint;

		private int Index = 0;
	}
}
