using System.Drawing;

namespace Shooting
{
	internal class CharacterL_Touhou : CharacterR_Touhou
	{
		public CharacterL_Touhou(StageDataPackage StageData, string textureName)
			: base(StageData, textureName)
		{
			base.OriginalPosition = new PointF(70f, 128f);
		}

		public override void Move()
		{
			if (base.Active)
			{
				if (base.OriginalPosition.X < 100f)
				{
					base.OriginalPosition = new PointF(base.OriginalPosition.X + 2f, base.OriginalPosition.Y);
				}
			}
			else if (base.OriginalPosition.X > 70f)
			{
				base.OriginalPosition = new PointF(base.OriginalPosition.X - 2f, base.OriginalPosition.Y);
			}
		}
	}
}
