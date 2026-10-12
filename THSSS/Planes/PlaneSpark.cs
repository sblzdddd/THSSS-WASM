using System.Drawing;

namespace Shooting
{
	internal class PlaneSpark : BaseEffect
	{
		public PlaneSpark(StageDataPackage StageData, string textureName, PointF OriginalPosition, float Velocity, double Direction)
			: base(StageData, textureName, OriginalPosition, Velocity, Direction)
		{
			this.OriginalPosition = OriginalPosition;
			ScaleWidth = 0.05f;
			Active = true;
			LifeTime = 300;
			ColorValue = Color.FromArgb(85, 140, 255);
			UnRemoveable = true;
			Angle = 0.0;
			ScaleVL = 0.1f;
			ScaleVW = 0.1f;
		}

		public override void Ctrl()
		{
			base.Ctrl();
			Region = (int)((float)TxtureObject.Width / 2f * 0.7f);
			if (Time < LifeTime - 50)
			{
				TransparentValueF = (float)(150 + Ran.Next(40));
			}
			else
			{
				TransparentValueF = (float)((LifeTime - Time) * 2 - 20 + Ran.Next(40));
			}
		}

		public override void Show()
		{
			if (TxtureObject != null)
			{
				var sizeF = new SizeF((float)TxtureObject.Width * ScaleWidth, (float)TxtureObject.Height * ScaleLength);
				SpriteMain.Draw2D(TxtureObject.TXTure, TxtureObject.PosRect, sizeF, new PointF((float)(TxtureObject.Width / 2), 0f), (float)(Direction - 1.5707963267948966 + Angle), Position, Color.FromArgb(TransparentValue, ColorValue));
			}
		}
	}
}
