using System.Drawing;

namespace Shooting
{
	internal class CharacterR : BaseStoryItem
	{
		public CharacterR(StageDataPackage StageData, string textureName)
			: base(StageData)
		{
			base.Position = new PointF((float)(base.BoundRect.Right - 100), (float)(base.BoundRect.Top + 200));
			base.Velocity = 0f;
			base.Direction = 0.0;
			base.TxtureObject = base.TextureObjectDictionary[textureName];
			base.Active = false;
			base.TransparentValueF = 0f;
		}

		public override void Move()
		{
			if (base.Active)
			{
				if (base.Position.X > (float)(base.BoundRect.Right - 130))
				{
					base.OriginalPosition = new PointF(base.OriginalPosition.X - 2f, base.OriginalPosition.Y);
				}
			}
			else if (base.Position.X < (float)(base.BoundRect.Right - 100))
			{
				base.OriginalPosition = new PointF(base.OriginalPosition.X + 2f, base.OriginalPosition.Y);
			}
		}

		public override void Ctrl()
		{
			base.Ctrl();
			if (base.Time <= 1)
			{
				base.TransparentValueF = 0f;
			}
			else if (base.Time < 26)
			{
				base.TransparentValueF += 10f;
			}
		}

		public override void Show()
		{
			SizeF sizeF = new SizeF((float)base.TxtureObject.Width * base.Scale, (float)base.TxtureObject.Height * base.Scale);
			if (base.Active)
			{
				base.SpriteMain.Draw2D(base.TxtureObject.TXTure, base.TxtureObject.PosRect, sizeF, base.TxtureObject.RotatingCenter, 0f, base.Position, Color.FromArgb(base.TransparentValue, Color.White));
			}
			else
			{
				base.SpriteMain.Draw2D(base.TxtureObject.TXTure, base.TxtureObject.PosRect, sizeF, base.TxtureObject.RotatingCenter, 0f, base.Position, Color.FromArgb(base.TransparentValue, Color.Gray));
			}
		}
	}
}
